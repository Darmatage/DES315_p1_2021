using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pritzCheckpoint : MonoBehaviour
{
    Collider2D pritzCollider;
    bool isActive = false;
    // Start is called before the first frame update
    void Start()
    {
        pritzCollider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!isActive && collision.tag == "Player")
        {
            collision.GetComponent<pritzPlayerMove>().spawnLocation = transform.position;
            isActive = true;
        }
    }
}
