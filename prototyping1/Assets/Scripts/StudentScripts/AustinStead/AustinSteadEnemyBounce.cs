using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AustinSteadEnemyBounce : MonoBehaviour
{
    private Rigidbody2D rb2d;


    private MonsterMoveHit moveScript;
    private Vector2 bounceDirection = new Vector2();
    private float bounceSpeed;
    private float bounceTimer = 0;
    private float prevSpeed;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        moveScript = GetComponent<MonsterMoveHit>();
    }

    // Update is called once per frame
    void Update()
    {
        if(bounceTimer > 0)
        {
            bounceTimer -= Time.deltaTime;

            rb2d.MovePosition((Vector2)transform.position + bounceDirection * bounceSpeed * Time.deltaTime);
            if (bounceTimer <= 0)
            {
                bounceTimer = 0;
                moveScript.speed = prevSpeed;
            }
        }
    }



    public void GetBounced(Vector2 bounceDirection, float bounceSpeed, float bounceDuration)
    {
        this.bounceDirection = bounceDirection;
        this.bounceSpeed = bounceSpeed;
        bounceTimer = bounceDuration;
        prevSpeed = moveScript.speed;
        moveScript.speed = 0;
    }
}
