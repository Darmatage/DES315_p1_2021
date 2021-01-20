using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseGraves_ProjectileScript : MonoBehaviour
{
    public GameObject Explosion;
    public int damage;
    public float speed;
    public float life;
    public float explosionDuration;

    private Vector3 direction;
    private float lifeTimer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.velocity = direction * speed;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = transform.position + direction * speed;

        lifeTimer += Time.deltaTime;
        if(lifeTimer >= life)
        {
            GameObject explosionEffect = Instantiate(Explosion, transform.position, Quaternion.identity);
            Destroy(explosionEffect, explosionDuration);
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }
        else if(collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "monsterShooter")
        {
            GameObject explosionEffect = Instantiate(Explosion, transform.position, Quaternion.identity);
            Destroy(explosionEffect, explosionDuration);
            Destroy(gameObject);
        }
        else
        {
            lifeTimer = 0.0f;
        }
    }

    public void ChaseGraves_SetDirection(Vector3 dir)
    {
        direction = dir.normalized;
    }
}
