using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Crab_Fireball : MonoBehaviour
{
    public Vector3 target;
    public float speed;
    public TileBase lavatile;
    public Tilemap lavamap;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, target, speed * Time.deltaTime);
        if (gameObject.transform.position == target) 
        {
            Vector3Int ye = new Vector3Int((int)target.x, (int)target.y, (int)target.z);
            lavamap.SetTile(ye, lavatile);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") 
        {
            Vector3Int ye = new Vector3Int((int)gameObject.transform.position.x, (int)gameObject.transform.position.y, (int)gameObject.transform.position.z);
            lavamap.SetTile(ye, lavatile);
            GameObject.FindGameObjectWithTag("GameHandler").GetComponent<GameHandler>().TakeDamage(5);
            Destroy(gameObject);
        }
    }
}
