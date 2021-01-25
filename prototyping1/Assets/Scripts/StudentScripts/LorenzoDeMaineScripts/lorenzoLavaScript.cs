using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lorenzoLavaScript : MonoBehaviour
{
	private GameObject fireDebuff;

	void Start()
	{

		if (GameObject.FindGameObjectWithTag("Debuff") != null)
		{
			fireDebuff = GameObject.FindGameObjectWithTag("Debuff");
		}
	}




    void OnTriggerStay2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			fireDebuff.GetComponent<lorenzoFireDebuff>().setActive(true);
		}
	}
}
