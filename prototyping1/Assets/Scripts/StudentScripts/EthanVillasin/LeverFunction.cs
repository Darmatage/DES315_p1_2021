using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverFunction : MonoBehaviour
{
    public GameObject LeverOn, LeverOff;
    public GameObject[] Doors;
    bool active = false;

    // Start is called before the first frame update
    void Start()
    {
        LeverOn.SetActive(false);
        LeverOff.SetActive(true);
        active = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (active == false)
            {
                LeverOff.SetActive(false);
                LeverOn.SetActive(true);
                active = true;
                for(int i = 0; i < Doors.Length; i++)
                {
                    Doors[i].GetComponent<WallDoor>().OpenDoor();
                }
            }
            else
            {
                LeverOn.SetActive(false);
                LeverOff.SetActive(true);
                active = false;
                for (int i = 0; i < Doors.Length; i++)
                {
                    Doors[i].GetComponent<WallDoor>().OpenDoor();
                }
            }
        }
    }
}
