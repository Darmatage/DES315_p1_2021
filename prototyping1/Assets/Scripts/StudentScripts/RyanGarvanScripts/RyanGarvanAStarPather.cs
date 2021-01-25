using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

// Generic A* pathfinding script
public class RyanGarvanAStarPather : MonoBehaviour
{
    // Pathfinding node structure that represents a single grid cell
    public class Node
    {
        public bool isOpen;       // Whether the node is on the open list
        public bool isClosed;     // Whether the node is on the closed list
        public bool isWall;       // Whether the node is impassable
        public bool isDynamic;    // Whether the node requires collision checks to determine whether it is a wall
        public float given;       // The lowest found given cost to the node
        public float heuristic;   // The heuristic cost of the node
        public bool hasParent;    // Whether the node has a parent
        public Vector2Int parent; // The coordinates of the node's parent nodes

        public Node()
        {
            isOpen = false;
            isClosed = false;
            isWall = false;
            isDynamic = false;
            given = 0;
            heuristic = 0;
            hasParent = false;
            parent = Vector2Int.zero;
        }
    }

    class PriorityQueue
    {
        List<Vector2Int> m_elements; // List of elements in the queue. Higher priority items are further to the right.
        Node[,] m_map;

        public PriorityQueue(ref Node[,] map)
        {
            m_elements = new List<Vector2Int>();
            m_map = map;
        }

        public void Insert(Vector2Int new_elem)
        {
            m_elements.Add(new_elem);
        }

        public Vector2Int Top()
        {
            int highestPriorityIndex = 0;

            for (int i = 1; i < m_elements.Count; ++i)
            {
                if (GetPriority(highestPriorityIndex) < GetPriority(i))
                {
                    highestPriorityIndex = i;
                }
            }
            return m_elements[highestPriorityIndex];
        }

        public Vector2Int Pop()
        {
            Vector2Int element = Top();
            m_elements.Remove(element);
            return element;
        }

        public int GetCount()
        {
            return m_elements.Count;
        }

        float GetPriority(int index)
        {
            Node node = m_map[m_elements[index].x, m_elements[index].y];
            return -(node.given + node.heuristic);
        }
    }

    int m_mapWidth;  // Width of the map, in cells
    int m_mapHeight; // Height of the map, in cells
    int m_xMin;      // Minimum cell x index in Unity grid (used when converting between internal and external coords)
    int m_yMin;      // Minimum cell y index in Unity grid (used when converting between internal and external coords)
    int m_xMax;      // Maximum cell x index in Unity grid (used when converting between internal and external coords)
    int m_yMax;      // Maximum cell y index in Unity grid (used when converting between internal and external coords)

    List<Grid> m_mapGrids; // Infinite grid (Unity component) containing all tilemaps

    Node[,] m_map; // Internal grid of pathfinding nodes

    PriorityQueue openList; // Open list for pathfinding
    
    // Start is called before the first frame update
    void Start()
    {
        m_mapGrids = new List<Grid>(FindObjectsOfType<Grid>()); // Get tile grid

        foreach (Grid grid in m_mapGrids)
        {
            // Go through every tilemap to determine the furthest extents of the map
            foreach (Tilemap tilemap in grid.GetComponentsInChildren<Tilemap>())
            {
                m_xMin = Mathf.Min(m_xMin, tilemap.cellBounds.xMin);
                m_yMin = Mathf.Min(m_yMin, tilemap.cellBounds.yMin);
                m_xMax = Mathf.Max(m_xMax, tilemap.cellBounds.xMax);
                m_yMax = Mathf.Max(m_yMax, tilemap.cellBounds.yMax);
            }
        }

        // Set map dimensions
        m_mapWidth = m_xMax - m_xMin;
        m_mapHeight = m_yMax - m_yMin;

        InitMap(); // Initialize map nodes

        // Set wall flags
        foreach (Grid grid in m_mapGrids)
        {
            if (grid.name.Contains("Changing"))
            {
                continue;
            }

            foreach (Tilemap tilemap in grid.GetComponentsInChildren<Tilemap>())
            {
                if (tilemap.name.Contains("Changeable"))
                {
                    continue;
                }

                // Current tilemap's collider, if any
                CompositeCollider2D tilemapCollider = tilemap.GetComponent<CompositeCollider2D>();

                // If current tilemap has a solid collider, use it to set wall flags
                if ((tilemapCollider != null && !tilemapCollider.isTrigger) || tilemap.gameObject.name.Contains("MimicWall"))
                {
                    // Mark all tiles on the current tilemap as walls on the internal node map
                    for (int x = 0; x < m_mapWidth; ++x)
                    {
                        for (int y = 0; y < m_mapHeight; ++y)
                        {
                            if (tilemap.HasTile(new Vector3Int(x + m_xMin, y + m_yMin, 0)))
                            {
                                m_map[x, y].isWall = true;
                            }
                        }
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // (DEBUG) Draw Xes on top of all identified wall tiles
        for (int x = 0; x < m_mapWidth; ++x)
        {
            for (int y = 0; y < m_mapHeight; ++y)
            {
                if (m_map[x, y].isWall)
                {
                    Vector3 cell_world_pos = MapToWorld(new Vector2Int(x, y)) + new Vector3(0, 0, -10);
                    Debug.DrawLine(cell_world_pos, cell_world_pos + new Vector3(1.0f, 1.0f, 0), Color.red, 0f);
                    Debug.DrawLine(cell_world_pos + new Vector3(1, 0, 0), cell_world_pos + new Vector3(0, 1, 0), Color.red, 0f);
                }
            }
        }
    }

    ////////////////////
    // Public functions
    ////////////////////

    // Returns a path from one point to another
    public List<Vector3> GetPath(Vector3 startPos, Vector3 goalPos)
    {
        ResetMap();

        List<Vector3> result = new List<Vector3>();
        Vector2Int startCellPos = WorldToMap(startPos);
        Vector2Int goalCellPos = WorldToMap(goalPos);

        if (!IsCellWalkable(goalCellPos))
        {
            return result;
        }

        openList = new PriorityQueue(ref m_map);
        openList.Insert(startCellPos);

        while (openList.GetCount() > 0)
        {
            Vector2Int nextCellPos = openList.Pop();

            if (nextCellPos == goalCellPos)
            {
                while (m_map[nextCellPos.x, nextCellPos.y].hasParent)
                {
                    result.Insert(0, MapToWorld(nextCellPos));
                    nextCellPos = m_map[nextCellPos.x, nextCellPos.y].parent;
                }

                return result;
            }

            for (int x = -1; x <= 1; ++x)
            {
                for (int y = -1; y <= 1; ++y)
                {
                    Vector2Int adjacentCellPos = new Vector2Int(nextCellPos.x + x, nextCellPos.y + y);

                    if ((x != 0 || y != 0) && IsCellWalkable(adjacentCellPos))
                    {
                        if (x != 0 && y != 0 && (!IsCellWalkable(nextCellPos + new Vector2Int(x, 0)) || !IsCellWalkable(nextCellPos + new Vector2Int(0, y))))
                            continue;

                        AddNodeToOpenList(adjacentCellPos, nextCellPos, Mathf.Sqrt(x * x + y * y), openList);
                    }
                }
            }

            m_map[nextCellPos.x, nextCellPos.y].isOpen = false;
            m_map[nextCellPos.x, nextCellPos.y].isClosed = true;
        }

        return result; // A path could not be found
    }

    public List<Vector3> GetFleePath(Vector3 startPos, int iterations, Vector3 playerPos)
    {
        ResetMap();

        List<Vector3> result = new List<Vector3>();
        Vector2Int startCellPos = WorldToMap(startPos);
        Vector2Int playerCellPos = WorldToMap(playerPos);

        openList = new PriorityQueue(ref m_map);
        openList.Insert(startCellPos);
        m_map[startCellPos.x, startCellPos.y].heuristic = -(playerCellPos - startCellPos).sqrMagnitude;

        PriorityQueue closedList = new PriorityQueue(ref m_map);

        int iterations_left = iterations;

        while (openList.GetCount() > 0)
        {
            Vector2Int nextCellPos = openList.Pop();

            for (int x = -1; x <= 1; ++x)
            {
                for (int y = -1; y <= 1; ++y)
                {
                    Vector2Int adjacentCellPos = new Vector2Int(nextCellPos.x + x, nextCellPos.y + y);

                    if ((x != 0 || y != 0) && IsCellWalkable(adjacentCellPos))
                    {
                        if ((startCellPos - adjacentCellPos).magnitude >= 15)
                            continue;

                        if ((playerCellPos - adjacentCellPos).magnitude <= 3 && (playerCellPos - nextCellPos).magnitude > 3)
                            continue;

                        if ((playerCellPos - adjacentCellPos).magnitude <= 2.0f)
                            continue;

                        if (x != 0 && y != 0 && (!IsCellWalkable(nextCellPos + new Vector2Int(x, 0)) || !IsCellWalkable(nextCellPos + new Vector2Int(0, y))))
                            continue;

                        AddNodeToOpenList(adjacentCellPos, nextCellPos, Mathf.Sqrt(x * x + y * y), openList, -(adjacentCellPos - playerCellPos).sqrMagnitude);
                    }
                }
            }

            m_map[nextCellPos.x, nextCellPos.y].isOpen = false;
            m_map[nextCellPos.x, nextCellPos.y].isClosed = true;
            closedList.Insert(nextCellPos);

            --iterations_left;
        }

        Vector2Int targetPos = closedList.Pop();

        while (m_map[targetPos.x, targetPos.y].hasParent)
        {
            result.Insert(0, MapToWorld(targetPos));
            targetPos = m_map[targetPos.x, targetPos.y].parent;
        }

        return result;
    }

    // Returns the pather's internal map data. Use this if you want to set a custom heuristic.
    public Node[,] GetMap()
    {
        return m_map;
    }

    ////////////////////
    // Helper functions
    ////////////////////

    // Initialize all Node objects in the map
    void InitMap()
    {
        m_map = new Node[m_mapWidth, m_mapHeight];

        for (int x = 0; x < m_mapWidth; ++x)
        {
            for (int y = 0; y < m_mapHeight; ++y)
            {
                m_map[x, y] = new Node();
            }
        }

        Collider2D[] colliders = FindObjectsOfType<Collider2D>();

        foreach (Collider2D collider in colliders)
        {
            if (!collider.isTrigger)
            {
                Rigidbody2D rb = collider.GetComponent<Rigidbody2D>();

                if (rb == null || rb.bodyType == RigidbodyType2D.Static)
                {
                    Tilemap tilemap = collider.GetComponent<Tilemap>();

                    if (tilemap == null)
                    {
                        Vector2Int cellPos = WorldToMap(collider.transform.position);
                        m_map[cellPos.x, cellPos.y].isDynamic = true;
                    }
                    else if (tilemap.gameObject.name.Contains("Changeable"))
                    {
                        for (int x = tilemap.cellBounds.xMin; x <= tilemap.cellBounds.xMax; ++x)
                        {
                            for (int y = tilemap.cellBounds.yMin; y <= tilemap.cellBounds.yMax; ++y)
                            {
                                if (tilemap.HasTile(new Vector3Int(x, y, 0)))
                                {
                                    Vector2Int cellPos = new Vector2Int(x - m_xMin, y - m_yMin);
                                    m_map[cellPos.x, cellPos.y].isDynamic = true;
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    // Reset open and closed list flags on all nodes
    public void ResetMap()
    {
        for (int x = 0; x < m_mapWidth; ++x)
        {
            for (int y = 0; y < m_mapHeight; ++y)
            {
                m_map[x, y].isOpen = false;
                m_map[x, y].isClosed = false;
                m_map[x, y].hasParent = false;
            }
        }
    }

    // Convert m_map (internal) coordinates to world coordinates
    public Vector3 MapToWorld(Vector2Int cellPos)
    {
        return m_mapGrids[0].CellToWorld((Vector3Int)(cellPos + new Vector2Int(m_xMin, m_yMin))) + new Vector3(0.5f, 0.5f, 0);
    }

    // Convert world coordinates to m_map (internal) coordinates
    public Vector2Int WorldToMap(Vector3 worldPos)
    {
        return (Vector2Int)m_mapGrids[0].WorldToCell(worldPos) - new Vector2Int(m_xMin, m_yMin);
    }

    public bool IsCellWalkable(Vector2Int cellPos)
    {
        if (cellPos.x < 0 || cellPos.y < 0 || cellPos.x >= m_mapWidth || cellPos.y >= m_mapHeight)
        {
            return false;
        }

        if (m_map[cellPos.x, cellPos.y].isWall)
        {
            return false;
        }

        if (m_map[cellPos.x, cellPos.y].isDynamic)
        {
            List<Collider2D> results = new List<Collider2D>(Physics2D.OverlapPointAll(MapToWorld(cellPos)));

            foreach (Collider2D collider in results)
            {
                if (!collider.isTrigger || collider.name.Contains("MimicWall"))
                {
                    Rigidbody2D rb = collider.GetComponent<Rigidbody2D>();

                    if (rb == null || rb.bodyType == RigidbodyType2D.Static)
                    {
                        return false;
                    }
                }
            }
        }

        return true;
    }

    void AddNodeToOpenList(Vector2Int cellPos, Vector2Int parentCellPos, float givenCost, PriorityQueue openList, float heuristic = 0)
    {
        Node node = m_map[cellPos.x, cellPos.y];
        Node parentNode = m_map[parentCellPos.x, parentCellPos.y];

        if ((!node.isOpen && !node.isClosed) || node.given > parentNode.given + givenCost)
        {
            if (!node.isOpen)
            {
                openList.Insert(cellPos);
            }

            node.isOpen = true;
            node.isClosed = false;
            node.given = parentNode.given + givenCost;
            node.hasParent = true;
            node.parent = parentCellPos;
            node.heuristic = heuristic;
        }
    }
}
