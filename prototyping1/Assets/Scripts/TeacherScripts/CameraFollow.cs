using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	public GameObject playerObj;

	void Start(){

		if (GameObject.FindGameObjectWithTag ("Player") != null) {
			playerObj = GameObject.FindGameObjectWithTag ("Player");
		}

	}

	void FixedUpdate () {
		Vector2 pos = Vector2.Lerp ((Vector2)transform.position, (Vector2)playerObj.transform.position, Time.fixedDeltaTime * 5);
		//transform.position = new Vector3 (pos.x, pos.y, transform.position.y);
		transform.position = new Vector3 (pos.x, pos.y, transform.position.z);
	}

}
