using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingEnemy_AS : MonoBehaviour
{
    public float speed = 1f; // speed of enemy
    private Transform target; // the player target
    public int damage = 1; // how much damage it deals to player
    public int EnemyLives = 2;
    private Renderer rend;
    private SpriteRenderer spriteRenderer;
    private GameHandler gameHandlerObj;
    private Animator anim;

    public static float strobeDelay = .15f;
    float strobeDelayTimer = strobeDelay;
    public float explodeRange = 100.0f;
    bool toggle = false;
    float detonateTimer = 2f; // in seconds
    bool bExplode = false;
    private bool attackPlayer = false;
    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponentInChildren<Animator>();
        rend = GetComponentInChildren<Renderer>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

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

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            // if the player is within range, then blow up
            if(Vector2.Distance(target.position, transform.position) <= explodeRange)
            {
                //attackPlayer = false;

                //Debug.Log("Explode");

                //if (detonateTimer >= 0)
                //{

                //    Strobe();
                //    detonateTimer -= Time.deltaTime;
                //}
                //else
                //{
                //    StopCoroutine("GetHit");
                //    StartCoroutine("GetHit");
                //}

                bExplode = true;
            }
            else if(Vector2.Distance(target.position, transform.position) > explodeRange && !bExplode)
            {
                attackPlayer = true;
            }

            if(bExplode)
            {
                attackPlayer = false;

                Debug.Log("Explode");

                if (detonateTimer >= 0)
                {

                    Strobe();
                    detonateTimer -= Time.deltaTime;
                }
                else
                {
                    // StartCoroutine(Wait());
                    // Destroy(gameObject);
                    StartCoroutine(Explode());

                    if(Vector2.Distance(target.position, transform.position) <= explodeRange)
                        gameHandlerObj.TakeDamage(10);
                    detonateTimer = 3f;
                    bExplode = false;
                }
            }

            if (attackPlayer == true)
            {
                transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            }
            else if (attackPlayer == false)
            {
                transform.position = Vector2.MoveTowards(transform.position, target.position, speed * 0.0f * Time.deltaTime);
            }
        }
    }

    private void Strobe()
    {
        if (strobeDelayTimer <= 0f)
        {
            strobeDelayTimer = strobeDelay;

            toggle = !toggle;

            if (toggle)
                spriteRenderer.enabled = true;
            else
                spriteRenderer.enabled = false;
        }
        else
            strobeDelayTimer -= Time.deltaTime;
    }

    IEnumerator Explode()
    {
        //anim.SetTrigger("Hurt");
        //EnemyLives -= 1;
        //// color values are R, G, B, and alpha, each divided by 100
        //rend.material.color = new Color(2.4f, 0.9f, 0.9f, 0.5f);
        //if (EnemyLives < 1)
        //{
        //    //gameHandlerObj.AddScore (1);
        //    Destroy(gameObject);
        //}

        spriteRenderer.color = new Color(2.0f, 1.0f, 0.0f, 0.5f);


        yield return new WaitForSeconds(.5f);
        //rend.material.color = Color.white;

        Destroy(gameObject);
    }
}
