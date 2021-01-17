using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class JacobPresleyAbility : MonoBehaviour
{
  public struct TempWall
  {
    public Vector3Int position;
    public TileBase wall;
  }
  
  public GameObject gameMap;
  public int tempWallMax;
  public TileBase wallTile;
  public TempWall[] tempWalls = new TempWall[3]; //public to test in editor
  public GameObject wallUI;

  private Tilemap tileGameMap;
  private int oldestTile = 0;

  private void PlaceWall()
  {
    Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    Vector3Int hoveredTile = tileGameMap.WorldToCell(mousePos);
    Text wallText = wallUI.GetComponent<Text>();

    //dont place walls on preexisting walls
    if (tileGameMap.GetTile(hoveredTile) != null)
    {
      return;
    }
    
    tileGameMap.SetTile(hoveredTile, wallTile);
    TileBase newTempWall = tileGameMap.GetTile(hoveredTile);
    
    if (tempWalls[0].wall == null)
    {
      tempWalls[0].wall = newTempWall;
      tempWalls[0].position = hoveredTile;
      wallText.text = "WALLS: 1 / 3";
    }
    else if (tempWalls[1].wall == null)
    {
      tempWalls[1].wall = newTempWall;
      tempWalls[1].position = hoveredTile;
      wallText.text = "WALLS: 2 / 3";
    }
    else if (tempWalls[2].wall == null)
    {
      tempWalls[2].wall = newTempWall;
      tempWalls[2].position = hoveredTile;
      wallText.text = "WALLS: 3 / 3";
    }
    else
    {
      tileGameMap.SetTile(tempWalls[oldestTile].position, null);
      tempWalls[oldestTile].position = hoveredTile;
      tempWalls[oldestTile].wall = newTempWall;
      oldestTile++;
      if (oldestTile > 2)
        oldestTile = 0;
    }
  }

  // Start is called before the first frame update
  void Start()
  {
    tileGameMap = gameMap.GetComponent<Tilemap>();
  }

  // Update is called once per frame
  void Update()
  {
    if (Input.GetMouseButtonDown(0) == true)
    {
      PlaceWall();
    }
  }
}
