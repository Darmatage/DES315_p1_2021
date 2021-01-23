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
}
