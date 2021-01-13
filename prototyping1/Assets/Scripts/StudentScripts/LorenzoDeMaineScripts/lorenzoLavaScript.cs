using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lorenzoLavaScript : MonoBehaviour
{
	private GameHandler gameHandlerObj;

	void Start()
	{

		if (GameObject.FindGameObjectWithTag("GameHandler") != null)
		{
			gameHandlerObj = GameObject.FindGameObjectWithTag("GameHandler").GetComponent<GameHandler>();
		}
	}



	void OnTriggerStay2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			gameHandlerObj.GetComponent<lorenzoFireDebuff>().setActive(true);
		}
	}
}
