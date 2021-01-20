using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LJNBoulderTrapScript : MonoBehaviour
{
    public Vector2 spawn_distance;
    public GameObject BoulderPrefab;
    public float speed = 1;
    public Sprite triggered;
    public int EnemyDamage = 3;
    public int PlayerDamage = 10;


    private bool activated = false;
    private GameHandler gameHandlerObj;
    // Start is called before the first frame update
    void Start()
    {
        GameObject gameHandlerLocation = GameObject.FindWithTag("GameHandler");
        if (gameHandlerLocation != null)
        {
            gameHandlerObj = gameHandlerLocation.GetComponent<GameHandler>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Bullet")
        {
            
            if (!activated)
            {
                Vector3 spawnpos = transform.position;
                spawnpos.x += spawn_distance.x;
                spawnpos.y += spawn_distance.y;
                spawnpos.z = -2;

                GameObject bould = Instantiate(BoulderPrefab, spawnpos, Quaternion.identity);

                LJNBoulderScript bs = bould.GetComponent<LJNBoulderScript>();
                bs.speed = speed;
                bs.dir = spawn_distance * -1.0f;
                bs.EnemyDamage = EnemyDamage;
                bs.PlayerDamage = PlayerDamage;

                GetComponent<SpriteRenderer>().sprite = triggered;
                activated = true;
            }
            
        }
    }
}
