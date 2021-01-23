using System.Collections;
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
	public bool isolate;

    // Start is called before the first frame update
    void Start()
    {
		SwitchOffArt.SetActive(true);
		SwitchOnArt.SetActive(false);
		isActive = false;
		//DoorObj = GameObject.FindGameObjectWithTag("Door");
		if (ChangeableGridOff)
			ChangeableGridOff.SetActive(false);
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "Player"){
			if (!SwitchOffArt.activeSelf) // If switch is being turned off
			{
				if (!isolate)
				{
					GameObject[] a = GameObject.FindGameObjectsWithTag("togglebutton");
					for (int i = 0; i < a.Length; ++i)
					{
						if (!a[i].GetComponent<NewDoorSwitch_DRC>().isolate)
						{
							a[i].GetComponent<NewDoorSwitch_DRC>().SwitchOffArt.SetActive(true);
							a[i].GetComponent<NewDoorSwitch_DRC>().SwitchOnArt.SetActive(false);
							a[i].GetComponent<NewDoorSwitch_DRC>().isActive = false;
							if (a[i].GetComponent<NewDoorSwitch_DRC>().ChangeableGridOff)
								a[i].GetComponent<NewDoorSwitch_DRC>().ChangeableGridOff.SetActive(false);
							if (a[i].GetComponent<NewDoorSwitch_DRC>().ChangeableGridOn)
								a[i].GetComponent<NewDoorSwitch_DRC>().ChangeableGridOn.SetActive(true);
						}
					}
				}
				if (DoorObj)
				{
					if(DoorObj.GetComponent<NewDoor_DRC>())
						DoorObj.GetComponent<NewDoor_DRC>().DoorClose();
				}
				if(ChangeableGridOn)
					ChangeableGridOn.SetActive(true);
				if (ChangeableGridOff)
					ChangeableGridOff.SetActive(false);
			}
			else // If switch is being turned on
            {
				if (!isolate)
				{
					GameObject[] a = GameObject.FindGameObjectsWithTag("togglebutton");
					for (int i = 0; i < a.Length; ++i)
					{
						if (!a[i].GetComponent<NewDoorSwitch_DRC>().isolate)
						{
							a[i].GetComponent<NewDoorSwitch_DRC>().SwitchOffArt.SetActive(false);
							a[i].GetComponent<NewDoorSwitch_DRC>().SwitchOnArt.SetActive(true);
							a[i].GetComponent<NewDoorSwitch_DRC>().isActive = true;
							if (a[i].GetComponent<NewDoorSwitch_DRC>().ChangeableGridOff)
								a[i].GetComponent<NewDoorSwitch_DRC>().ChangeableGridOff.SetActive(true);
							if (a[i].GetComponent<NewDoorSwitch_DRC>().ChangeableGridOn)
								a[i].GetComponent<NewDoorSwitch_DRC>().ChangeableGridOn.SetActive(false);
						}
					}
				}
				if (DoorObj)
				{
					if (DoorObj.GetComponent<NewDoor_DRC>())
						DoorObj.GetComponent<NewDoor_DRC>().DoorOpen();
					else if (DoorObj.GetComponent<Door>())
						DoorObj.GetComponent<Door>().DoorOpen();
				}
				if (ChangeableGridOn)
					ChangeableGridOn.SetActive(false);
				if (ChangeableGridOff)
					ChangeableGridOff.SetActive(true);
			}
		}
	}

}
