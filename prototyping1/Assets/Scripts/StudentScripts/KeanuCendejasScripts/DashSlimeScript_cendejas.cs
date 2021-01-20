using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashSlimeScript_cendejas : MonoBehaviour
{
	public float speed = 4f;
	private Transform target;
	public int damage = 1;
	public int EnemyLives = 3;
	private Renderer rend;
	private GameHandler gameHandlerObj;
	private Animator anim;
	private Rigidbody2D body;
	private Color col_;
	private float timer = 3.5f;
	private float staticTime = 3.5f;

	private float retreatTimer;
	public float retreatTime = 3.0f;
	private bool attackPlayer = true;

	void Start()
	{
		body = gameObject.GetComponent<Rigidbody2D>();
		anim = gameObject.GetComponentInChildren<Animator>();
		rend = GetComponentInChildren<Renderer>();
		col_ = rend.material.color;

		if (GameObject.FindGameObjectWithTag("Player") != null)
		{
			target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
		}
		GameObject gameHandlerLocation = GameObject.FindWithTag("GameHandler");
		if (gameHandlerLocation != null)
		{
			gameHandlerObj = gameHandlerLocation.GetComponent<GameHandler>();
		}
	}

	void Update()
	{
		Debug.Log(timer);

		timer -= Time.deltaTime;

		//int playerHealth = GameHandler.PlayerHealth; //access script directly in the case of a static variable 
		if (target != null)
		{
			//if ((attackPlayer == true) && (playerHealth >= 1)){
			if (attackPlayer == true)
			{
				transform.position = Vector2.MoveTowards(transform.position, target.position, 1f * Time.deltaTime);

				rend.material.SetColor("_Color", col_);

				if (timer <= 0)
                {
					rend.material.SetColor("_Color", Color.red);
					StartCoroutine("Dash");
                }
			}
			else if (attackPlayer == false)
			{
				transform.position = Vector2.MoveTowards(transform.position, target.position, speed * -1 * Time.deltaTime);
			}
		}
	}

	IEnumerator Dash()
    {
		Vector2 distance = target.position - transform.position;
		body.AddForce(distance * 1.5f);
		//transform.position = Vector2.MoveTowards(transform.position, target.position, 10f * Time.deltaTime);
		yield return new WaitForSeconds(0.5f);
		timer = staticTime;
    }

	void FixedUpdate()
	{
		retreatTimer += 0.1f;
		if (retreatTimer >= retreatTime)
		{
			attackPlayer = true;
			retreatTimer = 0f;
		}
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "bullet")
		{
			StopCoroutine("GetHit");
			StartCoroutine("GetHit");
		}
		else if (collision.gameObject.tag == "Player")
		{
			gameHandlerObj.TakeDamage(damage);
			attackPlayer = false;
			collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

			//EnemyLives -= EnemyLives;
			//rend.material.color = new Color(2.4f, 0.9f, 0.9f, 0.5f);
			//Destroy(gameObject);
		}
	}

	IEnumerator GetHit()
	{
		anim.SetTrigger("Hurt");
		EnemyLives -= 1;
		// color values are R, G, B, and alpha, each divided by 100
		rend.material.color = new Color(2.4f, 0.9f, 0.9f, 0.5f);
		if (EnemyLives < 1)
		{
			//gameHandlerObj.AddScore (1);
			Destroy(gameObject);
		}
		else yield return new WaitForSeconds(0.5f);
		rend.material.color = Color.white;
	}
}
