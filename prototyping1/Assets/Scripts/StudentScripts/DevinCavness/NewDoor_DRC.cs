using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewDoor_DRC : MonoBehaviour
{
	public GameObject DoorClosedArt;
	public GameObject DoorOpenArt;
	public string NextScene = "MainMenu";

    void Start(){
		DoorClosedArt.SetActive(true);
		DoorOpenArt.SetActive(false);
		gameObject.GetComponent<Collider2D>().enabled = false;
    }

	public void DoorOpen(){
		DoorClosedArt.SetActive(false);
		DoorOpenArt.SetActive(true);
		gameObject.GetComponent<Collider2D>().enabled = true;
	}

	public void DoorClose()
	{
		DoorClosedArt.SetActive(true);
		DoorOpenArt.SetActive(false);
		gameObject.GetComponent<Collider2D>().enabled = false;
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "Player"){
			SceneManager.LoadScene(NextScene);
		}
    }

}
