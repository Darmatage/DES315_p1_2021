using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewDoorSwitch_DRC : MonoBehaviour
{
	public GameObject SwitchOffArt;
	public GameObject SwitchOnArt;
	public GameObject DoorObj;
	public GameObject ChangeableWalls;

    // Start is called before the first frame update
    void Start()
    {
		SwitchOffArt.SetActive(true);
		SwitchOnArt.SetActive(false);
		DoorObj = GameObject.FindGameObjectWithTag("Door");
    }

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "Player"){
			if (!SwitchOffArt.activeSelf)
			{
				SwitchOffArt.SetActive(true);
				SwitchOnArt.SetActive(false);
				DoorObj.GetComponent<NewDoor_DRC>().DoorClose();
				ChangeableWalls.SetActive(true);
			}
			else
            {
				SwitchOffArt.SetActive(false);
				SwitchOnArt.SetActive(true);
				DoorObj.GetComponent<NewDoor_DRC>().DoorOpen();
				ChangeableWalls.SetActive(false);
			}
		}
	}

}
