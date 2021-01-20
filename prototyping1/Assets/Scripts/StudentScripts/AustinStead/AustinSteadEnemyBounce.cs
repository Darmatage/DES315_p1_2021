using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AustinSteadEnemyBounce : MonoBehaviour
{
    private Rigidbody2D rb2d;


    private AustinSteadMonsterMoveHit moveScript;
    private AustinSteadMonsterShootMove shootMoveScript;

    private Vector2 bounceDirection = new Vector2();
    private float bounceSpeed;
    private float bounceTimer = 0;
    private float prevSpeed;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        moveScript = GetComponent<AustinSteadMonsterMoveHit>();
        shootMoveScript = GetComponent<AustinSteadMonsterShootMove>();
    }

    // Update is called once per frame
    void Update()
    {
        if(bounceTimer > 0)
        {
            bounceTimer -= Time.deltaTime;

            if(rb2d)
                rb2d.MovePosition((Vector2)transform.position + bounceDirection * bounceSpeed * Time.deltaTime);
            
            if (bounceTimer <= 0)
            {
                bounceTimer = 0;
                ResumeMovement();
            }
        }
    }



    public void GetBounced(Vector2 bounceDirection, float bounceSpeed, float bounceDuration)
    {
        this.bounceDirection = bounceDirection;
        this.bounceSpeed = bounceSpeed;
        bounceTimer = bounceDuration;
        StopMovement();
    }


    private void StopMovement()
    {
        if(moveScript != null)
        {
            prevSpeed = moveScript.speed;
            moveScript.speed = 0;
        }
        else if(shootMoveScript != null)
        {
            prevSpeed = shootMoveScript.speed;
            shootMoveScript.speed = 0;
        }
    }

    private void ResumeMovement()
    {
        if (moveScript != null)
        {
            moveScript.speed = prevSpeed;
        }
        else if (shootMoveScript != null)
        {
            shootMoveScript.speed = prevSpeed;
        }
    }

}
