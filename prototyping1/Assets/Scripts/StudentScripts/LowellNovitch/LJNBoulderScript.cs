using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LJNBoulderScript : MonoBehaviour
{
    public float speed = 1;
    public Vector2 dir;
    public int EnemyDamage = 3;
    public int PlayerDamage = 10;

    private GameHandler gameHandlerObj;
    // Start is called before the first frame update
    void Start()
    {
        GameObject gameHandlerLocation = GameObject.FindWithTag("GameHandler");
        if (gameHandlerLocation != null)
        {
            gameHandlerObj = gameHandlerLocation.GetComponent<GameHandler>();
        }

        dir.Normalize();

        GetComponent<Rigidbody2D>().velocity = dir * speed;
    }

    // Update is called once per frame
    void Update()
    {
        //GetComponent<Rigidbody2D>().velocity = dir * speed;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Bullet" && collision.gameObject.tag != "Trap")
        {
            if (collision.gameObject.tag == "Player")
            {
                gameHandlerObj.TakeDamage(PlayerDamage);
            }
            else if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "monsterShooter")
            {
               
                MonoBehaviour ene =  collision.gameObject.GetComponent<MonoBehaviour>();
                if( ene != null)
                {
                    for(int i = 0; i < EnemyDamage; ++i)
                    {
                        ene.StopCoroutine("GetHit");
                        ene.StartCoroutine("GetHit");
                    }
                }

                collision.gameObject.GetComponent<Rigidbody2D>().drag = 0.5f;
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(dir*4,ForceMode2D.Impulse);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

 
}
