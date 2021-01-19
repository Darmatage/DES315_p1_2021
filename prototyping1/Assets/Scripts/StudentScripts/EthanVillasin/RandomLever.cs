using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomLever : MonoBehaviour
{
    public GameObject[] AllDoors;
    public GameObject[] RandomDoors;

    bool active = false;
    public GameObject LeverOn, LeverOff;
    // Start is called before the first frame update
    void Start()
    {
        AllDoors = GameObject.FindGameObjectsWithTag("WallDoor");

        int numDoors = Random.Range(0, 3);

        if (numDoors == 0)
            return;

        RandomDoors = new GameObject[numDoors];

        for (int i = 0; i < numDoors; i++)
        {
            int rand = Random.Range(0, AllDoors.Length);
            RandomDoors[i] = AllDoors[rand];
        }

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
                for(int i = 0; i < RandomDoors.Length; i++)
                {
                    RandomDoors[i].GetComponent<WallDoor>().OpenDoor();
                }
            }
            else
            {
                LeverOn.SetActive(false);
                LeverOff.SetActive(true);
                active = false;
                for (int i = 0; i < RandomDoors.Length; i++)
                {
                    RandomDoors[i].GetComponent<WallDoor>().OpenDoor();
                }
            }
        }
    }
}
