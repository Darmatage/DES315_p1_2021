using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDoor : MonoBehaviour
{
    public bool state;
    public GameObject doors;
    // Start is called before the first frame update
    void Start()
    {
        state = doors.activeSelf;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenDoor()
    {
        if (state == true) //open door
        {
            doors.SetActive(false);
            state = false;
        }
        else
        {
            doors.SetActive(true);
            state = true;
        }
    }
}
