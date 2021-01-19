using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AustinSteadProjectile : MonoBehaviour
{
	private GameHandler gameHandlerObj;
	public int damage = 1;
	public float speed = 10f;
	private Transform playerTrans;
	private Vector2 target;
	public GameObject hitEffectAnim;
	public float projectileLife = 3.0f;
	private float projectileTimer;

	void Start() {
		//transform gets location, but we need Vector2 to get direction, so we can moveTowards.
		playerTrans = GameObject.FindGameObjectWithTag("Player").transform;

		Vector2 temp = new Vector2(playerTrans.position.x - transform.position.x, playerTrans.position.y - transform.position.y);
		temp.Normalize();
		target = new Vector2(transform.position.x + temp.x * speed * projectileLife, transform.position.y + temp.y * speed * projectileLife);

		GameObject gameHandlerLocation = GameObject.FindWithTag ("GameHandler");
		if (gameHandlerLocation != null) {
			gameHandlerObj = gameHandlerLocation.GetComponent<GameHandler> ();
		}
	}

	void Update () {
		transform.position = Vector2.MoveTowards (transform.position, target, speed * Time.deltaTime);
	}

	//if bullet hits a collider, play explosion animation, then destroy effect and bullet
	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag != "monsterShooter") {
			if (other.gameObject.tag == "Player"){
				gameHandlerObj.TakeDamage(damage);
			}
			GameObject animEffect = Instantiate (hitEffectAnim, transform.position, Quaternion.identity);
			Destroy (animEffect, 0.5f);
			Destroy (gameObject);
		}
	}

	void FixedUpdate(){
		projectileTimer += 0.1f; 
		if (projectileTimer >= projectileLife){
			Destroy (gameObject);
		}
	}


	public void Redirect(Vector2 bounceDirection, float bounceSpeed, float bounceDuration)
    {
		target = new Vector2(transform.position.x + bounceDirection.x * speed * projectileLife, transform.position.y + bounceDirection.y * speed * projectileLife);
		projectileTimer = 0;
    }


}


//public GameObject hitEffectAnim;
//
////if the bullet hits a collider, play the explosion animation, then destroy the effect and the bullet
//void OnCollisionEnter2D(Collision2D collision){
//	GameObject animEffect = Instantiate (hitEffectAnim, transform.position, Quaternion.identity);
//	Destroy(animEffect, 0.5f);
//	Destroy(gameObject);
//}

