using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oil : MonoBehaviour
{
    private bool isTouchingOil = false;
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
        if (isTouchingOil)
        {
            // gameHandlerObj.TakeDamage(damage);
        }
    }

	void OnTriggerStay2D(Collider2D other)
    {
		if (other.gameObject.tag == "Player")
        {
            isTouchingOil = true;

            Debug.Log("Is touching oil");
		}
	}

	void OnTriggerExit2D(Collider2D other)
    {
		if (other.gameObject.tag == "Player")
        {
            isTouchingOil = false;

            Debug.Log("Stopped touching oil");
		}
	}
}