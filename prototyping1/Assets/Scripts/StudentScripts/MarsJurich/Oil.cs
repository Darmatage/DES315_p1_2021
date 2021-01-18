using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
// INSTRUCTIONS:
//
// Create a Tilemap with a Rigidbody 2D, Tilemap Collider 2D,
// and a Composite Collider 2D. Then add the Oil script to it.
//

public class Oil : MonoBehaviour
{
	private GameHandler gameHandlerObj;

	void Start()
    {
		if (GameObject.FindGameObjectWithTag("GameHandler") != null)
        {
			gameHandlerObj = GameObject.FindGameObjectWithTag("GameHandler").GetComponent<GameHandler>();
		}
	}

	void FixedUpdate()
    {
        
    }

	void OnTriggerStay2D(Collider2D other)
    {
		if (other.gameObject.tag == "Player")
        {
            OilPlayerMove oilPlayerMove = other.gameObject.GetComponent<OilPlayerMove>();
            oilPlayerMove.isTouchingOil = true;

            Debug.Log("Is touching oil");
		}
	}

	void OnTriggerExit2D(Collider2D other)
    {
		if (other.gameObject.tag == "Player")
        {
            OilPlayerMove oilPlayerMove = other.gameObject.GetComponent<OilPlayerMove>();
            oilPlayerMove.isTouchingOil = false;
        
            Debug.Log("Stopped touching oil");
		}
	}
}