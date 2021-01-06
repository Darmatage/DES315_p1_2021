using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
	public int damage = 1;
	public float damageTime = 0.5f;
	private bool isDamaging = false;
	private float damageTimer = 0f;
	private GameHandler gameHandlerObj;

	void Start () {

		if (GameObject.FindGameObjectWithTag ("GameHandler") != null) {
			gameHandlerObj = GameObject.FindGameObjectWithTag ("GameHandler").GetComponent<GameHandler>();
		}
	}

	void FixedUpdate(){
		if (isDamaging == true){
			damageTimer += 0.1f;
			if (damageTimer >= damageTime){
				gameHandlerObj.TakeDamage (damage);
				damageTimer = 0f;
			}
		}
	}

	void OnTriggerStay2D(Collider2D other){
		if (other.gameObject.tag == "Player") {
			isDamaging = true;
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if (other.gameObject.tag == "Player") {
			isDamaging = false;
		}
	}
		
}
