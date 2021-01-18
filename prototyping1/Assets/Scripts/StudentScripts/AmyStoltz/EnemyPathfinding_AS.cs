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
        onList = OnList.CLOSEDLIST;
        parentPos = Vector2Int.zero;
        isObstacle = false;
    }

}

public class AStarPather
{
    public Grid grid;
    public GameObject testObj;

    List<List<Node>> nodeGrid;
    List<Node> openList;
    public void init(Grid grid)
    {
        nodeGrid = new List<List<Node>>(); // creates the list

         // is this necessary?

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

        Debug.Log("Row count: " + rowCount.ToString());
        Debug.Log("Col count: " + colCount.ToString());

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
        Debug.Log("Number of obstacles: " + i);

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
                        Vector3 cell_world_pos = MapToWorld(new Vector2Int(col, row)) + new Vector3(0, 0, 10);
                       // Debug.Log("Obstacle Pos: " + cell_world_pos.ToString());

                        //testObj.transform.position = cell_world_pos;

                        //GameObject.Instantiate(testObj, cell_world_pos, Quaternion.identity);
                    }
                }
            }

            Debug.Log("Im here");
        }
        else
        {
            Debug.Log("I didn't make it.");
        }
    }

    public Vector3 MapToWorld(Vector2Int cellPos)
    {
        int rowMin = 0, colMin = 0; 

        foreach (Tilemap tilemap in grid.GetComponentsInChildren<Tilemap>())
        {
            rowMin = Mathf.Min(tilemap.cellBounds.yMin, rowMin); // note: rows are technically y (height)
            colMin = Mathf.Min(tilemap.cellBounds.xMin, colMin); // note: cols are technically x (width)
        }

        return grid.CellToWorld((Vector3Int)(cellPos + new Vector2Int(colMin, rowMin))) + new Vector3(0.5f, 0.5f, 0);
    }

    public void setObject(GameObject object_)
    {
        testObj = object_;
    }

    public void Update()
    {

    }
}
