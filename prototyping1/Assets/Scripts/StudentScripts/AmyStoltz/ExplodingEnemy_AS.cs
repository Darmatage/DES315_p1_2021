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
    public float explodeRange = 2.0f;
    bool toggle = false;
    float detonateTimer = 2f; // in seconds
    bool bExplode = false;
    private bool attackPlayer = false;
    public int damageAmount = 10;

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
                    StartCoroutine(Explode());

                     // if the player is in range when the enemy explodes, they take damage
                    if(Vector2.Distance(target.position, transform.position) <= explodeRange)
                        gameHandlerObj.TakeDamage(damageAmount);

                     // resets values
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
        spriteRenderer.color = new Color(2.0f, 1.0f, 0.0f, 0.5f); // changes color of enemy to yellow
        yield return new WaitForSeconds(.5f); // waits so that the color can actually change before it is destroyed
        Destroy(gameObject);
    }
}
