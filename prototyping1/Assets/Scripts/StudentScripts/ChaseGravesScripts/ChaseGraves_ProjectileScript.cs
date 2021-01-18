using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseGraves_ProjectileScript : MonoBehaviour
{
    public int damage;
    public float speed;
    public float life;

    private Vector3 direction;
    private float timer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + direction * speed;

        timer += Time.deltaTime;
        if(timer >= life)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }

        if (collision.gameObject.tag != "Player")
        {
            Destroy(gameObject);
        }
    }

    public void ChaseGraves_SetDirection(Vector3 dir)
    {
        direction = dir.normalized;
    }
}
