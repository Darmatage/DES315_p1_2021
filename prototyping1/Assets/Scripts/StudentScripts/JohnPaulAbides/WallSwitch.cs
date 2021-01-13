using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WallSwitch : MonoBehaviour
{
	public GameObject SwitchOffArt;
	public GameObject SwitchOnArt;

	public Tilemap WallMap;
	public List<Tile> WallTiles;

	// Start is called before the first frame update
	void Start()
	{
		SwitchOffArt.SetActive(true);
		SwitchOnArt.SetActive(false);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			SwitchOffArt.SetActive(false);
			SwitchOnArt.SetActive(true);
			
		}
	}

}