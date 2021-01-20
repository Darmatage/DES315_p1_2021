using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/****************************************************************
 * Identical to Teacher prefaf DoorSwitch just now also checks
 * for projectiles from the player to activate the switch
****************************************************************/
public class ChaseGraves_GrenadeSwitch : MonoBehaviour
{
    public GameObject SwitchOffArt;
    public GameObject SwitchOnArt;
    public GameObject DoorObj;

    // Start is called before the first frame update
    void Start()
    {
        SwitchOffArt.SetActive(true);
        SwitchOnArt.SetActive(false);
        DoorObj = GameObject.FindGameObjectWithTag("Door");
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "bullet")
        {
            SwitchOffArt.SetActive(false);
            SwitchOnArt.SetActive(true);
            DoorObj.GetComponent<Door>().DoorOpen();
        }
    }
}
