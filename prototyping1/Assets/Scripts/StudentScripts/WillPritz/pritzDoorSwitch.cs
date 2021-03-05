﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pritzDoorSwitch : MonoBehaviour
{
	public GameObject SwitchOffArt;
	public GameObject SwitchOnArt;
	public GameObject DoorObj;
    public GameObject ActivateTilemap;


    // Start is called before the first frame update
    void Start()
    {
		SwitchOffArt.SetActive(true);
		SwitchOnArt.SetActive(false);
		DoorObj = GameObject.FindGameObjectWithTag("Door");
    }

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "Player"){
			SwitchOffArt.SetActive(false);
			SwitchOnArt.SetActive(true);
			DoorObj.GetComponent<Door>().DoorOpen();
            ActivateTilemap.SetActive(true);
		}
	}

}
