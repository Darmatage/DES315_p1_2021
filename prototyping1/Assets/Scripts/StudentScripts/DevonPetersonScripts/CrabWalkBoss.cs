using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CrabWalkBoss : MonoBehaviour
{
    public float speed = 1.0f;
    float delay_timer = 0.0f;
    bool delayed = false;
    public GameObject movetowards;
    Vector3 firstmove;
    Vector3 secondmove;
    bool switchmoves = false;
    bool atlocation = false;

    public void set_delay(float delay) 
    {
        delay_timer = delay;
    }

    public bool get_delayed() 
    {
        return (delayed);
    }

    // Start is called before the first frame update
    void Start()
    {

        firstmove = gameObject.transform.position;
        secondmove = gameObject.transform.position;

        if (movetowards) 
        {
            Vector3 targetposition = movetowards.transform.position;
            Vector3 position = transform.position;
            float X = Mathf.Abs(targetposition.x - position.x);
            float Y = Mathf.Abs(targetposition.y - position.y);

            if (X <= Y)
            {
                firstmove = new Vector3(targetposition.x, position.y, position.z);
                secondmove = new Vector3(targetposition.x, targetposition.y, position.z);
            }
            else
            {
                firstmove = new Vector3(position.x, targetposition.y, position.z);
                secondmove = new Vector3(targetposition.x, targetposition.y, position.z);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
        if (!atlocation) 
        {
            if (delay_timer >= 0.0f)
            {
                delayed = true;
                delay_timer -= Time.deltaTime;
            }
            else 
            {
                delayed = false;
                if (switchmoves == false && transform.position != firstmove)
                {
                    transform.position = Vector3.MoveTowards(transform.position, firstmove, speed * Time.deltaTime);
                    if (transform.position == firstmove) switchmoves = true;
                }
                else if (switchmoves == true && transform.position != secondmove)
                {
                    transform.position = Vector3.MoveTowards(transform.position, secondmove, speed * Time.deltaTime);
                    if (transform.position == secondmove) atlocation = true;
                }
            }
            
        }

        

        //gameObject.transform.position += new Vector3(transform.position.x + (h * speed * Time.deltaTime), transform.position.y + (v * speed * Time.deltaTime), transform.position.z);
    }

    public void KillCrab() 
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == movetowards) 
        {
            gameObject.GetComponent<CrabBossFireball>().coverinlava();
        }
    }
}
