using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burning_Dustin_Keplinger : MonoBehaviour
{
	public int damage = 1;
	public float damageTime = 2.0f;
	private float damageTimer = 0f;
	private bool isOnFire = false;
	private float BurnTimerComplete = 30.0f;
	private float CurrentBurnTimer = 0.0f;

	private GameHandler gameHandlerObj;
	private SpriteRenderer onFireSprite;

	void Start()
	{
		if (GameObject.FindGameObjectWithTag("GameHandler") != null)
		{
			gameHandlerObj = GameObject.FindGameObjectWithTag("GameHandler").GetComponent<GameHandler>();
		}

		if (GameObject.FindGameObjectWithTag("Fire") != null)
		{
			onFireSprite = GameObject.FindGameObjectWithTag("Fire").GetComponent<SpriteRenderer>();

			onFireSprite.enabled = false;
		}
	}

	void FixedUpdate()
	{
		if (isOnFire == true)
		{
			damageTimer += Time.deltaTime;
			if (damageTimer >= damageTime)
			{
				gameHandlerObj.TakeDamage(damage);
				damageTimer = 0.0f;
			}

			CurrentBurnTimer += Time.deltaTime;
			//if the timer runs out the player stops burning
			if(CurrentBurnTimer > BurnTimerComplete)
            {
				isOnFire = false;
				onFireSprite.enabled = false;
			}

		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "lava")
		{
			isOnFire = true;
			CurrentBurnTimer = 0.0f;
			onFireSprite.enabled = true;
		}

		if (other.gameObject.tag == "Water")
		{
			isOnFire = false;
			onFireSprite.enabled = false;
		}
	}
}
