using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum OnList { OPENLIST, CLOSEDLIST, NOLIST };

public class Node
{
    public float totalCost; // f
    public float gCost; // g
    public OnList onList; // whether on openlist, closedlist, or not on any list
    public Vector2Int parentPos; // grid row and col of parent node
    public bool isObstacle;

    public Node()
    {
        totalCost = 0.0f;
        gCost = 0.0f;
        onList = OnList.NOLIST;
        parentPos = Vector2Int.zero;
        isObstacle = false;
    }

}

public class AStarPather
{
    public Grid grid;
    public GameObject testObj;

    List<List<Node>> nodeGrid;
    List<Vector2Int> openList;
    public void init(Grid grid)
    {
        nodeGrid = new List<List<Node>>(); // creates the list
        

         // gets the dimensions of the populated grid spaces
        Vector2Int gridDimensions = getGridDimensions(grid);

          // sets rows and cols accordingly 
        int rowCount = gridDimensions.y;
        int colCount = gridDimensions.x;

         // fills the 2D List with the nodes in the grid
        for(int row = 0; row < rowCount; ++row)
        {
            nodeGrid.Add(new List<Node>());

            for(int col = 0; col < colCount; ++col)
            {
                nodeGrid[row].Add(new Node());
            }
        }

        setObstacles(grid, rowCount, colCount);
    }

     // gets the dimensions of the grid that's in use
    private Vector2Int getGridDimensions(Grid grid)
    {
        int rowMin = 0, rowMax = 0, colMin = 0, colMax = 0; // the max row and cols for the tilemaps
        int rowCount = 0, colCount = 0; // the total row and column count of the grid used
        
         // loops through each tilemap in the grid and gets the bounds
        foreach(Tilemap tilemap in grid.GetComponentsInChildren<Tilemap>())
        {
            rowMin = Mathf.Min(tilemap.cellBounds.yMin, rowMin); // note: rows are technically y (height)
            rowMax = Mathf.Max(tilemap.cellBounds.yMax, rowMax);
            colMin = Mathf.Min(tilemap.cellBounds.xMin, colMin); // note: cols are technically x (width)
            colMax = Mathf.Max(tilemap.cellBounds.xMax, colMax);
        }

         // gets number of rows and cols being occupied by tiles
        rowCount = rowMax - rowMin;
        colCount = colMax - colMin;

       // Debug.Log("Row count: " + rowCount.ToString());
        //Debug.Log("Col count: " + colCount.ToString());

         // returning regarding x and y 
        return new Vector2Int(colCount, rowCount);
    }

     // sets whether the node is an obstacle tile (one that the enemy can't walk on)
    private void setObstacles(Grid grid, int rowCount, int colCount)
    {
        int rowMin = 0, colMin = 0; // the max row and cols for the tilemaps

        foreach (Tilemap tilemap in grid.GetComponentsInChildren<Tilemap>())
        {
            rowMin = Mathf.Min(tilemap.cellBounds.yMin, rowMin); // note: rows are technically y (height)
            colMin = Mathf.Min(tilemap.cellBounds.xMin, colMin); // note: cols are technically x (width)
        }
        int i = 0;
        foreach(Tilemap tilemap in grid.GetComponentsInChildren<Tilemap>())
        {
            TilemapCollider2D collider = tilemap.GetComponent<TilemapCollider2D>();

            if(collider != null)
            {
                for(int row = 0; row < rowCount; ++row)
                {
                    for(int col = 0; col < colCount; ++col)
                    {
                        if (tilemap.HasTile(new Vector3Int(col + colMin, row + rowMin, 0)))
                        {
                            nodeGrid[row][col].isObstacle = true;
                            ++i;
                        }
                    }
                }
            }
        }
        //Debug.Log("Number of obstacles: " + i);

    }

    public void setGrid(Grid grid_)
    {
        grid = grid_;
    }

    public void DrawDebug()
    {
        Vector2Int dim = getGridDimensions(grid);


        int rowCount = dim.y;
        int colCount = dim.x;

        if (grid)
        {
            for (int row = 0; row < rowCount; ++row)
            {
                for (int col = 0; col < colCount; ++col)
                {
                    if (nodeGrid[row][col].isObstacle)
                    {
                        Vector3 cell_world_pos = gridToWorld(new Vector2Int(col, row)) + new Vector3(0, 0, 10);
                       // Debug.Log("Obstacle Pos: " + cell_world_pos.ToString());

                        //testObj.transform.position = cell_world_pos;

                        //GameObject.Instantiate(testObj, cell_world_pos, Quaternion.identity);
                    }
                }
            }

            //Debug.Log("Im here");
        }
        else
        {
            //Debug.Log("I didn't make it.");
        }
    }

    public Vector3 gridToWorld(Vector2Int pos)
    {
        int rowMin = 0, colMin = 0; 

        foreach (Tilemap tilemap in grid.GetComponentsInChildren<Tilemap>())
        {
            rowMin = Mathf.Min(tilemap.cellBounds.yMin, rowMin); // note: rows are technically y (height)
            colMin = Mathf.Min(tilemap.cellBounds.xMin, colMin); // note: cols are technically x (width)
        }

        return grid.CellToWorld((Vector3Int)(pos + new Vector2Int(colMin, rowMin))) + new Vector3(0.5f, 0.5f, 0);
    }

    public Vector2Int worldToGrid(Vector3 pos)
    {
        int rowMin = 0, colMin = 0;

        foreach (Tilemap tilemap in grid.GetComponentsInChildren<Tilemap>())
        {
            rowMin = Mathf.Min(tilemap.cellBounds.yMin, rowMin); // note: rows are technically y (height)
            colMin = Mathf.Min(tilemap.cellBounds.xMin, colMin); // note: cols are technically x (width)
        }
        return (Vector2Int)grid.WorldToCell(pos) - new Vector2Int(colMin, rowMin);
    }

    public void setObject(GameObject object_)
    {
        testObj = object_;
    }

    // computes the path
    public List<Vector3> computePath(Vector3 start, Vector3 goal)
    {

        //Debug.Log("I get in here PATHER");
        Vector2Int startPos = worldToGrid(start);
        Vector2Int goalPos = worldToGrid(goal);

        //Vector3 cell_world_pos1 = gridToWorld(new Vector2Int(startPos.x, startPos.y)) + new Vector3(0, 0, 10);
        //testObj.transform.position = cell_world_pos1;
        //GameObject.Instantiate(testObj, cell_world_pos1, Quaternion.identity);
        
        //Vector3 cell_world_pos2 = gridToWorld(new Vector2Int(goalPos.x, goalPos.y)) + new Vector3(0, 0, 10);
        //testObj.transform.position = cell_world_pos2;
        //GameObject.Instantiate(testObj, cell_world_pos2, Quaternion.identity);


        // will hold the final path
        List<Vector3> path = new List<Vector3>();

        // sets parent location to "null"
        nodeGrid[startPos.y][startPos.x].parentPos.y = -1;
        nodeGrid[startPos.y][startPos.x].parentPos.x = -1;
        nodeGrid[startPos.y][startPos.x].gCost = 0.0f; // since it is start, given cost is 0

        // calculates the total cost
        nodeGrid[startPos.y][startPos.x].totalCost = nodeGrid[startPos.y][startPos.x].gCost + calculateHeuristic(startPos, goalPos);

        // push the start Node on the open list
        openList = new List<Vector2Int> { startPos };
        //openList.Add(startPos);
        nodeGrid[startPos.y][startPos.x].onList = OnList.OPENLIST;

        float gNew, totalNew;
        // while there are still nodes to search
        while (openList.Count > 0)
        {
             // pop the cheapest node off the openList
            Vector2Int poppedPos = getCheapestNodePos(openList);

             // if we have made it to the goal
            if(poppedPos == goalPos)
            {
                //makePath(goalPos, ref path);

                Vector2Int pos = new Vector2Int(goalPos.x, goalPos.y);

                while (nodeGrid[pos.y][pos.x].parentPos.y != -1 && nodeGrid[pos.y][pos.x].parentPos.x != -1)
                {
                    path.Insert(0, gridToWorld(pos));

                    Vector2Int temp = new Vector2Int(nodeGrid[pos.y][pos.x].parentPos.x, nodeGrid[pos.y][pos.x].parentPos.y);

                    pos = temp;
                }

                //result.Insert(0, start);

                //Debug.Log("Found path");

                resetGrid();

                // return the path
                return path;
            }

            // go through each valid neighbor of the popped node
            foreach(Vector2Int neighborPos in getNeighbors(poppedPos))
            {
                gNew = nodeGrid[poppedPos.y][poppedPos.x].gCost + getMoveCost(poppedPos, neighborPos);
                totalNew = gNew + calculateHeuristic(neighborPos, goalPos);

                 // if the popped node isn't on any list then put it on open list
                if(nodeGrid[neighborPos.y][neighborPos.x].onList == OnList.NOLIST)
                {
                    nodeGrid[neighborPos.y][neighborPos.x].onList = OnList.OPENLIST;
                    nodeGrid[neighborPos.y][neighborPos.x].parentPos = poppedPos;
                    nodeGrid[neighborPos.y][neighborPos.x].gCost = gNew;
                    nodeGrid[neighborPos.y][neighborPos.x].totalCost = totalNew;

                    // puts it on open list
                    openList.Add(neighborPos);
                }
                 // if it is on a list
                else
                {
                    if (totalNew < nodeGrid[neighborPos.y][neighborPos.x].totalCost)
                    {
                        // if it is on the open list then just update the node
                        if (nodeGrid[neighborPos.y][neighborPos.x].onList == OnList.OPENLIST)
                        {
                            nodeGrid[neighborPos.y][neighborPos.x].parentPos = poppedPos;
                            nodeGrid[neighborPos.y][neighborPos.x].gCost = gNew;
                            nodeGrid[neighborPos.y][neighborPos.x].totalCost = totalNew;
                        }
                        // if it is on the closed list then update node and put it back on open list
                        else
                        {
                            nodeGrid[neighborPos.y][neighborPos.x].onList = OnList.OPENLIST;
                            nodeGrid[neighborPos.y][neighborPos.x].parentPos = poppedPos;
                            nodeGrid[neighborPos.y][neighborPos.x].gCost = gNew;
                            nodeGrid[neighborPos.y][neighborPos.x].totalCost = totalNew;

                            openList.Add(neighborPos);
                        }
                    }
                }
            }

            nodeGrid[poppedPos.y][poppedPos.x].onList = OnList.CLOSEDLIST;
        }

        openList.Clear();

        resetGrid();

         // a path couldn't be found
        return null;
    }

    private void makePath(Vector2Int goalPos, ref List<Vector3>  path)
    {
        Vector2Int pos = new Vector2Int(goalPos.x, goalPos.y);

        while(nodeGrid[pos.y][pos.x].parentPos.y != -1 && nodeGrid[pos.y][pos.x].parentPos.x != -1)
        {
            path.Insert(0, gridToWorld(pos));

            Vector2Int temp = new Vector2Int(nodeGrid[pos.y][pos.x].parentPos.x, nodeGrid[pos.y][pos.x].parentPos.y);

            pos = temp;
        }
    }

    private void resetGrid()
    {
        Vector2Int dim = getGridDimensions(grid);

        for (int row = 0; row < dim.y; ++row)
        {
            for (int col = 0; col < dim.x; ++col)
            {
                nodeGrid[row][col].gCost = 0.0f;
                nodeGrid[row][col].onList = OnList.NOLIST;
                nodeGrid[row][col].parentPos.y = 0;
                nodeGrid[row][col].parentPos.x = 0;
                nodeGrid[row][col].totalCost = 0.0f;
            }
        }
    }

    private float getMoveCost(Vector2Int parentPos, Vector2Int neighbor)
    {
        if (neighbor.y - parentPos.y == 0 || neighbor.x - parentPos.x == 0)
            return 1.0f;

        return Mathf.Sqrt(2.0f);
    }

    private List<Vector2Int> getNeighbors(Vector2Int current)
    {
        Vector2Int neighborPos = new Vector2Int();

        List<Vector2Int> neighbors = new List<Vector2Int>();

         // goes through all adjacent grid spaces from current node pos
        for(int dRow = -1; dRow <= 1; ++dRow)
        {
            for(int dCol = -1; dCol <= 1; ++dCol)
            {
                 // same node continue
                if (dRow == 0 && dCol == 0)
                    continue;

                 // calcs the neighbors position
                neighborPos.y = current.y + dRow;
                neighborPos.x = current.x + dCol;

                 // if the neighbor is valid, then push it to the list
                if (isValidNeighbor(neighborPos, current))
                    neighbors.Add(neighborPos);
            }
        }

        return neighbors;
    }

    private bool isValidNeighbor(Vector2Int neighbor, Vector2Int parent)
    {
        Vector2Int dim = getGridDimensions(grid);

         // if it is out of bounds then it isn't valid
        if (neighbor.x < 0 || neighbor.y < 0 || neighbor.x >= dim.x || neighbor.y >= dim.y)
            return false;

         // if the neighbor is a wall it isn't valid
        if (nodeGrid[neighbor.y][neighbor.x].isObstacle)
            return false;

         // doesn't cut corners
        int dRow = neighbor.y - parent.y;
        int dCol = neighbor.x - parent.x;

        Vector2Int pos1 = new Vector2Int();
        Vector2Int pos2 = new Vector2Int();

        pos1.y = parent.y + dRow;
        pos1.x = parent.x;

        pos2.y = parent.y;
        pos2.x = parent.x + dCol;

         // if either of those are walls then they aren't valid
        if (nodeGrid[pos1.y][pos1.x].isObstacle || nodeGrid[pos2.y][pos2.x].isObstacle)
            return false;

        return true;
    }

    public Vector2Int getCheapestNodePos(List<Vector2Int> openList)
    {
       // Debug.Log("I get in cheapest node");

        float minCost = nodeGrid[openList[0].y][openList[0].x].totalCost;
        Vector2Int cheapest = openList[0];

         // finds the node with cheapest total cost
        foreach(Vector2Int pos in openList)
        {
             // if there is a node that cost less or equal than current cheapest
            if(nodeGrid[pos.y][pos.x].totalCost <= minCost)
            {
                 // if the total Costs are the same then choose one with lower given cost
                if(nodeGrid[pos.y][pos.x].totalCost == minCost)
                {
                    if(nodeGrid[pos.y][pos.x].gCost < nodeGrid[cheapest.y][cheapest.x].gCost)
                    {
                        minCost = nodeGrid[pos.y][pos.x].totalCost;
                        cheapest = pos;
                    }
                }
                 // else just set cheapest to new node
                else
                {
                    minCost = nodeGrid[pos.y][pos.x].totalCost;
                    cheapest = pos;
                }
            }
        }

         // pops value from the list
        openList.Remove(cheapest);

         // makes sure to say that node is not on any list right now
        nodeGrid[cheapest.y][cheapest.x].onList = OnList.NOLIST;

        return cheapest;
    }

    public float calculateHeuristic(Vector2Int startPos, Vector2Int goalPos)
    {
        float cost = 0.0f;

         // gets x,y coordinate differences
        float xDiff = (float)Mathf.Abs(startPos.x - goalPos.x);
        float yDiff = (float)Mathf.Abs(startPos.y - goalPos.y);

         // computes octile distance
        float firstPart = Mathf.Min(xDiff, yDiff) * Mathf.Sqrt(2.0f);
        float secondPart = Mathf.Max(xDiff, yDiff) - Mathf.Min(xDiff, yDiff);
        cost = firstPart + secondPart;

         // returns octile heuristic
        return cost;
    }
}
