using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner_AS : MonoBehaviour
{
    public GameObject enemyPrefab;
    // Start is called before the first frame update
    //void Start()
    //{

    //}

    // Update is called once per frame
    void FixedUpdate()
    {
        if(spawnEnemy)
        {
            if (waitTimer >= 0f)
            {
                waitTimer -= Time.deltaTime;
            }
            else
            {
                Instantiate(enemyPrefab, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }

    public static float waitDelay = 2f;
    float waitTimer = waitDelay;

    bool spawnEnemy = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            //Debug.Log("ON TRIGGER");

            spawnEnemy = true;

            //if(waitTimer >= 0f)
            //{
            //    waitTimer -= Time.deltaTime;
            //}
            //else
            //{
            //    Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            //    Destroy(gameObject);
            //}
        }
    }
}
