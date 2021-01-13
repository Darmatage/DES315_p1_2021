using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class JacobPresleyAbility : MonoBehaviour
{
  public GameObject gameMap;

  public int tempWallMax;
  private TileBase[] tempWalls;
  private Tilemap tileGameMap;

  private void SelectTile()
  {
    Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    Vector3Int hoveredTile = tileGameMap.WorldToCell(mousePos);
    
    tileGameMap.SetTile(hoveredTile, null);
  }

  private void ReplaceTile()
  {
    Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    Vector3Int hoveredTile = tileGameMap.WorldToCell(mousePos);
    Vector3Int standingTile = tileGameMap.WorldToCell(GetComponentInParent<Transform>().position);

    TileBase standing = tileGameMap.GetTile(standingTile);

    tileGameMap.SetTile(hoveredTile, standing);
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
      SelectTile();
    }

    if (Input.GetMouseButtonDown(1) == true)
    {
      ReplaceTile();
    }
  }
}
