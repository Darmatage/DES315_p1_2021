using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WallSwitch : MonoBehaviour
{
	public GameObject SwitchOffArt;
	public GameObject SwitchOnArt;

	public Tilemap WallMap;
	public TileBase WallTileBase;
	public List<Vector3Int> WallTiles;

	// Start is called before the first frame update
	void Start()
	{
		SwitchOffArt.SetActive(true);
		SwitchOnArt.SetActive(false);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (SwitchOffArt.activeSelf == true && other.gameObject.tag == "Player")
		{
			SwitchOffArt.SetActive(false);
			SwitchOnArt.SetActive(true);

			foreach (Vector3Int pos in WallTiles)
            {
				// if a tile exists in pos, set it to null, spawn wall otherwise
				if (WallMap.GetTile(pos) != null)
					WallMap.SetTile(pos, null);
				else
					WallMap.SetTile(pos, WallTileBase);
			}
		}
	}

}