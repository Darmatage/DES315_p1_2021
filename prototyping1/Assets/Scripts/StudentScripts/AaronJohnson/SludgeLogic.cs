using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SludgeLogic : MonoBehaviour
{
    public float slowRate = 0.15f;
    float slowTime = 0.5f;
    public float MinSpeed = 0.2f;
    private bool isSlowing = false;
    private float slowTimer = 0f;
    private GameHandler gameHandlerObj;
    private GameObject player;
    private float startSpeed;

    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.FindGameObjectWithTag("GameHandler") != null)
        {
            gameHandlerObj = GameObject.FindGameObjectWithTag("GameHandler").GetComponent<GameHandler>();
        }

        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            startSpeed = player.GetComponent<PlayerMove>().speed;
        }
    }

    private void FixedUpdate()
    {
        if (isSlowing == true)
        {
            slowTimer += 0.1f;
            if (slowTimer >= slowTime)
            {
                //gameHandlerObj.TakeDamage(SlowRate);
                player.GetComponent<PlayerMove>().speed -= (slowRate / 2.0f);
                if (player.GetComponent<PlayerMove>().speed <= MinSpeed)
                    player.GetComponent<PlayerMove>().speed = MinSpeed;

                slowTimer = 0f;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            isSlowing = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            isSlowing = false;
            player.GetComponent<PlayerMove>().speed = startSpeed; ;
        }
    }
}
