using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabBossBreakDoor : MonoBehaviour
{
    public Sprite brokendoorsprite;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "door" || collision.gameObject.tag == "Door")
        {
            collision.gameObject.GetComponent<SpriteRenderer>().sprite = brokendoorsprite;
            collision.gameObject.GetComponent<BoxCollider2D>().enabled = false;


            gameObject.GetComponent<CrabBossFireball>().coverinlava();
        }
    }
}
