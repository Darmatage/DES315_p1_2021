using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZacaryBrown_BouncyBullet : Projectile
{
    // Start is called before the first frame update
    void Start()
    {
        transform.position += (GameObject.Find("Player").GetComponent<Transform>().position - transform.position).normalized;
        GetComponent<Rigidbody2D>().velocity = (GameObject.Find("Player").GetComponent<Transform>().position - transform.position).normalized * speed;
    }

    // Update is called once per frame
    void Update()
    {
        //GetComponent<Rigidbody2D>().velocity = vel;
    }

    private void OnCollisionEnter(Collision other)
    {
        Rigidbody2D body = GetComponent<Rigidbody2D>();
        
        if (other.gameObject.GetComponent<ZacaryBrown_Surface>().Bouncy)
        {
            body.velocity = Vector2.Reflect(body.velocity, other.contacts[0].normal);
        }
    }
}
