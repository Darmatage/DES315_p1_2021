using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDoor : MonoBehaviour
{
    public bool state;
    // Start is called before the first frame update
    void Start()
    {
        state = gameObject.activeSelf;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenDoor()
    {
        if (state == true) //open door
        {
            gameObject.SetActive(false);
            state = false;
        }
        else
        {
            gameObject.SetActive(true);
            state = true;
        }
    }
}
