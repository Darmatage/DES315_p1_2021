using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CrabBossFireball : MonoBehaviour
{

    public Tilemap background;
    public Tilemap lavalayer;
    public TileBase lavatile;
    public GameObject fireball_object;

    List<Vector3Int> tilelist;

    public bool fireballs = false;
    public float fireballfreq = 3.0f;
    public float fireballmovedelay = 1.0f;
    public float fireballspeed = 5.0f;
    public int fireballamount = 1;
    float timer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        if (background != null && lavalayer != null)
        {
            tilelist = new List<Vector3Int>();

            for (int i = background.origin.x; i <= background.size.x; i++)
            {
                for (int j = background.origin.y; j <= background.size.y; j++)
                {
                    Vector3Int temp = new Vector3Int(i, j, background.origin.z);
                    TileBase tile = background.GetTile(temp);
                    if (tile != null)
                    {
                        tilelist.Add(temp);
                    }
                }
            }
        }
        else 
        {
            fireballs = false;
        }
    }

    public void coverinlava()
    {
        for (int i = 0; i < tilelist.Count; i++)
        {
            //shoot lava
            Vector3Int pos = tilelist[i];
            //lavalayer.SetTile(pos, lavatile);

            Vector3 temptarget = new Vector3(pos.x, pos.y, pos.z);
            GameObject fireball = Instantiate(fireball_object, gameObject.transform.position, Quaternion.identity);
            Crab_Fireball ye = fireball.GetComponent<Crab_Fireball>();
            ye.lavamap = lavalayer;
            ye.lavatile = lavatile;
            ye.target = temptarget;
            ye.speed = fireballspeed * 2;

        }
        switchfireballs(false);
    }

    public void switchfireballs(bool onoff) 
    {
        fireballs = onoff;
    }

    // Update is called once per frame
    void Update()
    {
        if (fireballs) 
        {
            if (timer <= 0.0f)
            {
                for (int i = 0; i < fireballamount; i++)
                {
                    //shoot lava
                    Vector3Int pos = tilelist[Random.Range(0, tilelist.Count)];
                    //lavalayer.SetTile(pos, lavatile);

                    Vector3 temptarget = new Vector3(pos.x, pos.y, pos.z);
                    GameObject fireball = Instantiate(fireball_object, gameObject.transform.position, Quaternion.identity);
                    Crab_Fireball ye = fireball.GetComponent<Crab_Fireball>();
                    ye.lavamap = lavalayer;
                    ye.lavatile = lavatile;
                    ye.target = temptarget;
                    ye.speed = fireballspeed;

                }
                gameObject.GetComponent<CrabWalkBoss>().set_delay(fireballmovedelay);
                timer = fireballfreq;
            }
            else 
            {
                if (gameObject.GetComponent<CrabWalkBoss>().get_delayed() == false) 
                {
                    timer -= Time.deltaTime;
                }
            }
        }
    }
}
