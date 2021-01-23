using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
// INSTRUCTIONS:
//
// Create a Tilemap with a Rigidbody 2D, Tilemap Collider 2D,
// and a Composite Collider 2D. Then add the Oil script to it.

public class Oil : MonoBehaviour
{

	void Start()
    {
        
    }

	void FixedUpdate()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<OilPlayerMove>().SetTouchingOil(true);
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
		if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<OilPlayerMove>().SetTouchingOil(true);
		}
	}

	void OnTriggerExit2D(Collider2D other)
    {
		if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<OilPlayerMove>().SetTouchingOil(false);
        }
	}
}