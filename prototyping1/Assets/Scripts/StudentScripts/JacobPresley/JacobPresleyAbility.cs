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
    public float timer;
  }
  
  public GameObject gameMap;
  public TileBase wallTile;
  public TempWall[] tempWalls = new TempWall[3]; //public to test in editor
  public GameObject wallUI;

  private Tilemap tileGameMap;
  private int oldestTile = 0;
  private int placedTiles = 0; 
  private Text wallText;
  
  
  
  
  
  private void PlaceWall()
  {
    Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    Vector3Int hoveredTile = tileGameMap.WorldToCell(mousePos);

    //dont place walls on preexisting walls
    if (tileGameMap.GetTile(hoveredTile) != null)
    {
      return;
    }

    Vector3Int playerPosition =
      tileGameMap.WorldToCell(GameObject.FindWithTag("Player").GetComponent<RectTransform>().position);
    //dont place walls on top of player
    if (hoveredTile == playerPosition)
    {
      return;
    }
    
    tileGameMap.SetTile(hoveredTile, wallTile);
    TileBase newTempWall = tileGameMap.GetTile(hoveredTile);
    
    if (tempWalls[0].wall == null)
    {
      replaceTile(0, newTempWall, hoveredTile);
    }
    else if (tempWalls[1].wall == null)
    {
      replaceTile(1, newTempWall, hoveredTile);
    }
    else if (tempWalls[2].wall == null)
    {
      replaceTile(2, newTempWall, hoveredTile);
    }
    else
    {
      float temp = 10.0f;
      for (int i = 0; i < tempWalls.Length; ++i)
      {
        if (tempWalls[i].timer < temp)
        {
          oldestTile = i;
          temp = tempWalls[i].timer;
        }
      }
      
      tileGameMap.SetTile(tempWalls[oldestTile].position, null);
      tempWalls[oldestTile].wall = null;
      replaceTile(oldestTile, newTempWall, hoveredTile);
    }
  }

  private void replaceTile(int tileNumber, TileBase newWall, Vector3Int newWallPosition)
  {
    tempWalls[tileNumber].wall = newWall;
    tempWalls[tileNumber].position = newWallPosition;
    tempWalls[tileNumber].timer = 4.0f;
    if (placedTiles < 3)
    {
      placedTiles++;
    }
  }

  // Start is called before the first frame update
  void Start()
  {
    tileGameMap = gameMap.GetComponent<Tilemap>();
    wallText = wallUI.GetComponent<Text>();
    for (int i = 0; i < tempWalls.Length; ++i)
    {
      tempWalls[i].timer = 4.0f;
    }
  }

  // Update is called once per frame
  void Update()
  {
    if (Input.GetMouseButtonDown(0) == true)
    {
      PlaceWall();
    }
    
    if (placedTiles < 0)
    {
      placedTiles = 0;
    }
    
    for (int i = 0; i < tempWalls.Length; ++i)
    {
      if (tempWalls[i].wall != null)
      {
        wallText.text = "WALLS: ";
        wallText.text += placedTiles.ToString();
        wallText.text += " / 3";
        tempWalls[i].timer -= Time.deltaTime;
        
        if (tempWalls[i].timer <= 0.0f)
        {
          tileGameMap.SetTile(tempWalls[i].position, null);
          tempWalls[i].timer = 4.0f;
          placedTiles--;
        }
      }
    }
  }
}
