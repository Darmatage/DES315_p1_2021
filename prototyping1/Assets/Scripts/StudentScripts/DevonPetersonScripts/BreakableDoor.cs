using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BreakableDoor : MonoBehaviour
{
	// Start is called before the first frame update
	public GameObject DoorClosedArt;
	public GameObject DoorOpenArt;
	public GameObject DoorBrokenArt;
	public string NextScene = "MainMenu";

	public bool open = false;
	public bool destroyed = false;

	void Start()
	{
		DoorClosedArt.SetActive(true);
		DoorOpenArt.SetActive(false);
		DoorBrokenArt.SetActive(false);
		gameObject.GetComponent<Collider2D>().enabled = true;
	}

	public void BrokenDoorOpen()
	{
		DoorClosedArt.SetActive(false);
		DoorOpenArt.SetActive(true);
		open = true;
		//gameObject.GetComponent<Collider2D>().enabled = true;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		
		if (other.gameObject.tag == "Player")
		{
			if (open && !destroyed) 
			{
				SceneManager.LoadScene(NextScene);
			}
		}

		if (other.gameObject.tag == "CrabWalkBoss")
		{
			//Debug.Log("collide");
			DoorBrokenArt.SetActive(true);
			DoorOpenArt.SetActive(false);
			DoorClosedArt.SetActive(false);
			gameObject.GetComponent<Collider2D>().enabled = false;
			open = false;
			destroyed = true;
		}
	}
}
