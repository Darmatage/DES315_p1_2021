using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorLogic : MonoBehaviour
{
	public GameObject DoorClosedArt;
	public GameObject DoorOpenArt;
	public GameObject Enemy1;
	public GameObject Enemy2;
	public GameObject Enemy3;
	public GameObject Enemy4;
	public GameObject Enemy5;
	public string NextScene = "MainMenu";

	void Start()
	{
		DoorClosedArt.SetActive(true);
		DoorOpenArt.SetActive(false);
		gameObject.GetComponent<Collider2D>().enabled = false;
	}

	public void DoorOpen()
	{
		DoorClosedArt.SetActive(false);
		DoorOpenArt.SetActive(true);
		gameObject.GetComponent<Collider2D>().enabled = true;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			SceneManager.LoadScene(NextScene);
		}
	}

    private void Update()
    {
        if(Enemy1 == null && Enemy2 == null && Enemy3 == null && Enemy4 == null && Enemy5 == null)
        {
			DoorOpen();
        }
    }
}
