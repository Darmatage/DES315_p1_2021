using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InventoryHandlerJW : MonoBehaviour
{
	private GameObject playerObj;

	public static bool InventoryIsOpen = false;
	public GameObject pauseMenuUI;
	public KeyCode key = KeyCode.Tab;
	public bool pauseOnOpen = true;

	public int coinCount = 0;
	public Text[] coinCounters;

    void Start()
	{	
		if (GameObject.FindGameObjectWithTag ("Player") != null) {
			playerObj = GameObject.FindGameObjectWithTag ("Player");
		}

		
		pauseMenuUI.SetActive(false);
    }
		
	//pause menu
	void Update (){
		if (Input.GetKeyDown(key)){
			if (InventoryIsOpen){ Close(); }
			else{ Open(); }
		}

		foreach(Text counter in coinCounters)
			counter.text = coinCount.ToString();
	}

	void Open(){
		pauseMenuUI.SetActive(true);
		Time.timeScale = 0f;
		InventoryIsOpen = true;
	}

	public void Close(){
		pauseMenuUI.SetActive(false);
		Time.timeScale = 1f;
		InventoryIsOpen = false;
	}

	public bool Store(CollectibleJW collectible) // returns whether storage was successful
	{
		switch (collectible.type)
		{
			case "Coin":
				coinCount++;
				return true;
		}

		return false;
	}
}
