using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZacaryBrown_BouncyBullet : Projectile
{
    public int bounces = 2;
    public bool active = false;
    private Vector2 velocity;
    private float projectileTimer = 0.0f;

    private GameHandler gameHandler;
    // Start is called before the first frame update
    void Start()
    {
        transform.position += (GameObject.Find("Player").GetComponent<Transform>().position - transform.position).normalized;
        GetComponent<Rigidbody2D>().velocity = (GameObject.Find("Player").GetComponent<Transform>().position - transform.position).normalized * speed;
        velocity = (GameObject.Find("Player").GetComponent<Transform>().position - transform.position).normalized *
                   speed;
        gameHandler = GameObject.FindGameObjectWithTag ("GameHandler").GetComponent<GameHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        projectileTimer += 0.1f; 
        if (projectileTimer >= projectileLife){
            Destroy (gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        int i = 0;
        ++i;
        --i;
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        --bounces;

        if (bounces < 0)
        {
            Destroy(gameObject);
            return;
        }
        
        Rigidbody2D body = GetComponent<Rigidbody2D>();
        
        if (other.gameObject.GetComponent<ZacaryBrown_Surface>() != null && other.gameObject.GetComponent<ZacaryBrown_Surface>().Bouncy)
        {
            body.velocity = Vector2.Reflect(velocity, other.contacts[0].normal);
            velocity = body.velocity;
        }

        if (other.gameObject.name == "GameObject")
        {
            active = true;
        }

        if (other.gameObject.CompareTag("monsterShooter"))
        {
            Destroy(this.gameObject);
            other.gameObject.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            return;
        }

        if (other.gameObject.CompareTag("Player"))
        {
            gameHandler.TakeDamage(5);
            other.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            Destroy(this.gameObject);
        }
    }
}
