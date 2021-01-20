﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewDoorSwitch_DRC : MonoBehaviour
{
	public GameObject SwitchOffArt;
	public GameObject SwitchOnArt;
	public GameObject DoorObj;
	public GameObject ChangeableGridOn;
	public GameObject ChangeableGridOff;

	public bool isActive;

    // Start is called before the first frame update
    void Start()
    {
		SwitchOffArt.SetActive(true);
		SwitchOnArt.SetActive(false);
		isActive = false;
		//DoorObj = GameObject.FindGameObjectWithTag("Door");
		ChangeableGridOff.SetActive(false);
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "Player"){
			if (!SwitchOffArt.activeSelf)
			{
				GameObject[] a = GameObject.FindGameObjectsWithTag("togglebutton");
				for(int i = 0; i < a.Length; ++i)
                {
					a[i].GetComponent<NewDoorSwitch_DRC>().SwitchOffArt.SetActive(true);
					a[i].GetComponent<NewDoorSwitch_DRC>().SwitchOnArt.SetActive(false);
					a[i].GetComponent<NewDoorSwitch_DRC>().isActive = false;
				}
				if(DoorObj)
					DoorObj.GetComponent<NewDoor_DRC>().DoorClose();
				if(ChangeableGridOn)
					ChangeableGridOn.SetActive(true);
				if (ChangeableGridOff)
					ChangeableGridOff.SetActive(false);
			}
			else
            {
				GameObject[] a = GameObject.FindGameObjectsWithTag("togglebutton");
				for (int i = 0; i < a.Length; ++i)
				{
					a[i].GetComponent<NewDoorSwitch_DRC>().SwitchOffArt.SetActive(false);
					a[i].GetComponent<NewDoorSwitch_DRC>().SwitchOnArt.SetActive(true);
					a[i].GetComponent<NewDoorSwitch_DRC>().isActive = true;
				}
				if (DoorObj)
					DoorObj.GetComponent<NewDoor_DRC>().DoorOpen();
				if (ChangeableGridOn)
					ChangeableGridOn.SetActive(false);
				if (ChangeableGridOff)
					ChangeableGridOff.SetActive(true);
			}
		}
	}

}
