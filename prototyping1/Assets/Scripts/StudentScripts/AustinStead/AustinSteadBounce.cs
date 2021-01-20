using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AustinSteadBounce : MonoBehaviour
{

	private Rigidbody2D rb2d;
	private Animator anim;

	private Renderer rend;


	private List<GameObject> enemies = new List<GameObject>();
	private bool charging = false;

	private LineRenderer circle;

	public Color circleIdleColor;
	public Color circleInRangeColor;
	public Color circleChargingColor;

	public float bounceDuration = 1.0f;
	private float bounceTimer = 0.0f;

	private PlayerMove playerMove;
	private float playerMoveSpeed;

	private Vector2 bounceDirection = new Vector2();
	public float bounceSpeed = 10;

	// Start is called before the first frame update
	void Start()
	{
		anim = gameObject.GetComponentInChildren<Animator>();
		rend = GetComponentInChildren<Renderer>();

		if (gameObject.GetComponent<Rigidbody2D>() != null)
		{
			rb2d = GetComponent<Rigidbody2D>();
		}

		circle = GetComponentInChildren<LineRenderer>();
		playerMove = GetComponent<PlayerMove>();
		playerMoveSpeed = playerMove.speed;


		circle.startColor = circleIdleColor;
		circle.endColor = circleIdleColor;
	}

    // Update is called once per frame
    void Update()
	{
		if(bounceTimer > 0)
        {
			bounceTimer -= Time.deltaTime;
			if (bounceTimer <= 0)
            {
				bounceTimer = 0;
				playerMove.speed = playerMoveSpeed;
			}
			else
            {
				rb2d.MovePosition((Vector2)transform.position + bounceDirection * bounceSpeed * Time.deltaTime);
				return;
			}
		}


		//Check all surrounding colliders 
		Collider2D[] colliders = Physics2D.OverlapCircleAll(gameObject.transform.position, 2);
		enemies.Clear();
		foreach (Collider2D collider in colliders)
		{
			if (canBounce(collider.gameObject))
			{
				enemies.Add(collider.gameObject);
			}
		}

		if(enemies.Count > 0)
        {
			circle.startColor = circleInRangeColor;
			circle.endColor = circleInRangeColor;

			if (Input.GetKeyDown(KeyCode.LeftShift))
			{
				GameObject enemy = enemies[0].gameObject;

				circle.startColor = circleChargingColor;
				circle.endColor = circleChargingColor;
				playerMove.speed = 0;
				
				bounceDirection = playerMove.transform.position - enemy.transform.position;
				bounceDirection.Normalize();

				bounceTimer = bounceDuration;

				AustinSteadEnemyBounce enemyBounce = enemy.GetComponent<AustinSteadEnemyBounce>();
				AustinSteadProjectileBounce projectileBounce = enemy.GetComponent<AustinSteadProjectileBounce>();

				if (enemyBounce != null)
                {
					enemyBounce.GetBounced(-bounceDirection, bounceSpeed, bounceDuration);
                }
				if(projectileBounce != null)
                {
					projectileBounce.GetBounced(-bounceDirection, bounceSpeed, bounceDuration);
				}

			}

		}
		else
        {
			circle.startColor = circleIdleColor;
			circle.endColor = circleIdleColor;
		}

	}







	private bool canBounce(GameObject possibleEnemy)
    {
		if (possibleEnemy.GetComponent<AustinSteadEnemyBounce>() != null)
			return true;
		if (possibleEnemy.GetComponent<AustinSteadProjectileBounce>() != null)
			return true;

		return false;
    }

}

