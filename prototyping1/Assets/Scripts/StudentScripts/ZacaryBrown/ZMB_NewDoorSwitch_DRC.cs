using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZMB_NewDoorSwitch_DRC : MonoBehaviour
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
		if (ChangeableGridOff)
			ChangeableGridOff.SetActive(false);
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "Player" || (other.gameObject.tag == "bullet" && other.gameObject.GetComponent<ZacaryBrown_BouncyBullet>() != null && other.gameObject.GetComponent<ZacaryBrown_BouncyBullet>().active)){
			if (!SwitchOffArt.activeSelf)
			{
				/*GameObject[] a = GameObject.FindGameObjectsWithTag("togglebutton");
				for(int i = 0; i < a.Length; ++i)
                {
					a[i].GetComponent<ZMB_NewDoorSwitch_DRC>().SwitchOffArt.SetActive(true);
					a[i].GetComponent<ZMB_NewDoorSwitch_DRC>().SwitchOnArt.SetActive(false);
					a[i].GetComponent<ZMB_NewDoorSwitch_DRC>().isActive = false;
				}*/

				SwitchOffArt.SetActive(true);
				SwitchOnArt.SetActive(false);
				
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
			else
            {
				/*GameObject[] a = GameObject.FindGameObjectsWithTag("togglebutton");
				for (int i = 0; i < a.Length; ++i)
				{
					a[i].GetComponent<ZMB_NewDoorSwitch_DRC>().SwitchOffArt.SetActive(false);
					a[i].GetComponent<ZMB_NewDoorSwitch_DRC>().SwitchOnArt.SetActive(true);
					a[i].GetComponent<ZMB_NewDoorSwitch_DRC>().isActive = true;
				}*/
				
				SwitchOffArt.SetActive(false);
				SwitchOnArt.SetActive(true);
				
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
