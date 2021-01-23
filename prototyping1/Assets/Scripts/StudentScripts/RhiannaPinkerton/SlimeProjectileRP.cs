using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeProjectileRP : MonoBehaviour
{
	private GameHandler gameHandlerObj;
	public int damage = 1;
	public float speed = 10f;
	private Transform playerTrans;
	private Vector2 target;
	public GameObject hitEffectAnim;
	public float projectileLife = 3.0f;
	private float projectileTimer;

	[SerializeField] private GameObject SlimePrefab;

	public float SlimeTimeBtwShots;
	
	void Start() {
		//transform gets location, but we need Vector2 to get direction, so we can moveTowards.
		playerTrans = GameObject.FindGameObjectWithTag("Player").transform;
		target = new Vector2(playerTrans.position.x, playerTrans.position.y);

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

			if (!other.gameObject.name.Contains("Lava"))
			{
				GameObject animEffect = Instantiate (hitEffectAnim, transform.position, Quaternion.identity);
				Destroy (animEffect, 0.5f);
				Destroy (gameObject);
			}
		}
	}

	void FixedUpdate(){
		projectileTimer += 0.1f; 
		// Projectile life is over
		if (projectileTimer >= projectileLife)
		{
			// Spawn new slime
			GameObject slime = Instantiate(SlimePrefab, transform.position, Quaternion.identity);
			SlimeMoveShootRP slimeScript = slime.GetComponent<SlimeMoveShootRP>();
			// Increase the time between shots by 1
			slimeScript.startTimeBtwShots = SlimeTimeBtwShots + 2;
			// Set start color
			slime.GetComponentInChildren<SpriteRenderer>().color = slimeScript.SlimeSpawnColor;
			slime.transform.localScale = new Vector3(0.5f, 0.5f);
			Destroy (gameObject);
		}
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

