using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretLogic : MonoBehaviour
{
    public GameObject turret;
    public GameObject spike;
    //public Vector2 direction;
    Transform playerTransform;
    bool triggered = false;
    public float speed = 10f;
    public float projectileLife = 3f;
    public float spikeCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(triggered == true)
        {
            while (spikeCount != 1)
            {
                Instantiate(spike);
                spikeCount = 1;
            }
            spike.transform.position = Vector2.MoveTowards(turret.transform.position, playerTransform.position, speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        triggered = true;
    }
}
