using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ExplodingEnemy_AS : MonoBehaviour
{
    public Tilemap destructableTilemap;
    private List<Vector3> tileWorldLocations;

    public float speed = 1f; // speed of enemy
    private Transform target; // the player target
    public int damage = 1; // how much damage it deals to player
    public int EnemyLives = 2;

    public GameObject explosionObj;

    private Renderer rend;
    private SpriteRenderer spriteRenderer;
    private SpriteRenderer circleRenderer;
    private Animator anim;


    private GameHandler gameHandlerObj;
    public Grid grid;

    public static float strobeDelay = .15f;
    public static float respawnDelay = 4f;
    float respawnTimer = respawnDelay;
    float strobeDelayTimer = strobeDelay;
    public float explodeRange = 2.0f;
    bool toggle = false;
    float detonateTimer = 2f; // in seconds
    bool bExplode = false;
    bool respawning = false;
    private bool attackPlayer = false;
    public int damageAmount = 10;
    static AStarPather pather;
    Color startingColor;
    //CircleCollider2D circleCollider;
    Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponentInChildren<Animator>();
        rend = GetComponentInChildren<Renderer>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        
        foreach(Transform child in transform)
        {
            if(child.name == "circle_art")
            {
                circleRenderer = child.GetComponent<SpriteRenderer>();
            }
        }

        startPosition = transform.position;
        startingColor = spriteRenderer.color;
        //circleRenderer.enabled = false;

        destructableTilemap = GameObject.Find("TilemapDestructables").GetComponent<Tilemap>();

        //if (destructableTilemap != null)
        //{
        //    print("GOT TILEMAP");
        //}

        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        }
        GameObject gameHandlerLocation = GameObject.FindWithTag("GameHandler");
        if (gameHandlerLocation != null)
        {
            gameHandlerObj = gameHandlerLocation.GetComponent<GameHandler>();
        }

        tileMapInit();


        pather = new AStarPather();
        grid = FindObjectOfType<Grid>();
        pather.setGrid(grid);
        pather.setObject(explosionObj);
        pather.init(grid);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //pather.DrawDebug();

        //Debug.DrawLine(new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 10000.0f, 0.0f), Color.red, 0f,false);
        if (target != null)
        {
            // if the player is within range, then blow up
            if(Vector2.Distance(target.position, transform.position) <= explodeRange && !respawning)
            {
                bExplode = true;
            }
            else if(Vector2.Distance(target.position, transform.position) > explodeRange && !bExplode && !respawning)
            {
                attackPlayer = true;
            }

            if(respawning)
            {
                if(respawnTimer >= 0f)
                {
                    respawnTimer -= Time.deltaTime;
                }
                else
                {
                    spriteRenderer.enabled = true;
                    circleRenderer.enabled = true;
                    transform.position = startPosition;
                    spriteRenderer.color = startingColor;
                    attackPlayer = true;
                    respawning = false;
                    respawnTimer = respawnDelay;
                }
            }

            if(bExplode && !respawning)
            {
                attackPlayer = false;
                //circleRenderer.enabled = true;
               // Debug.Log("Explode");

                if (detonateTimer >= 0)
                {

                    Strobe();
                    detonateTimer -= Time.deltaTime;
                }
                else
                {
                    StartCoroutine(Explode());

                    foreach(Vector3 tile in tileWorldLocations)
                    {

                        Vector3 leftLowerCorner = new Vector3(tile.x - .5f, tile.y - .5f, tile.z);
                        Vector3 rightLowerCorner = new Vector3(tile.x + .5f, tile.y - .5f, tile.z);
                        Vector3 leftUpperCorner = new Vector3(tile.x - .5f, tile.y + .5f, tile.z);
                        Vector3 rightUpperCorner = new Vector3(tile.x + .5f, tile.y + .5f, tile.z);

                        //Instantiate(explosionObj, leftLowerCorner, Quaternion.identity);
                        //Instantiate(explosionObj, rightLowerCorner, Quaternion.identity);
                        //Instantiate(explosionObj, leftUpperCorner, Quaternion.identity);
                        //Instantiate(explosionObj, rightUpperCorner, Quaternion.identity);

                        if (Vector2.Distance(tile, transform.position) <= explodeRange ||
                            Vector2.Distance(leftLowerCorner, transform.position) <= explodeRange ||
                            Vector2.Distance(rightLowerCorner, transform.position) <= explodeRange ||
                            Vector2.Distance(leftUpperCorner, transform.position) <= explodeRange ||
                            Vector2.Distance(rightUpperCorner, transform.position) <= explodeRange)
                        {
                            //Debug.Log("in range");

                            Vector3Int localPlace = destructableTilemap.WorldToCell(tile);

                            if (destructableTilemap.HasTile(localPlace))
                            {
                                StartCoroutine(WallBreak(tile));
                                destructableTilemap.SetTile(destructableTilemap.WorldToCell(tile), null);

                                pather.updateNodeGrid(tile);
                            }
                            //tileWorldLocations.Remove(tile);
                        }
                    }

                     // if the player is in range when the enemy explodes, they take damage
                    if(Vector2.Distance(target.position, transform.position) <= explodeRange)
                        gameHandlerObj.TakeDamage(damageAmount);

                    detonateTimer = 3f;
                }
            }

            if (attackPlayer == true)
            {
                List<Vector3> path = pather.computePath(transform.position, target.position);

                if (path != null)
                {
                    transform.position = Vector2.MoveTowards(transform.position, path[0], speed * Time.deltaTime);
                }
                else
                {
                    transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

                }
            }
            else if (attackPlayer == false)
            {
                transform.position = Vector2.MoveTowards(transform.position, target.position, speed * 0.0f * Time.deltaTime);
            }
        }


    }

    private void tileMapInit()
    {
        tileWorldLocations = new List<Vector3>();

        foreach (var pos in destructableTilemap.cellBounds.allPositionsWithin)
        {
            Vector3Int localPlace = new Vector3Int(pos.x, pos.y, pos.z);
             // makes it center of tile rather than lower left
            Vector3 place = destructableTilemap.CellToWorld(localPlace) + new Vector3(.5f, .5f, 0.0f);
            
            
            if (destructableTilemap.HasTile(localPlace))
            {
                tileWorldLocations.Add(place);
            }
        }
    }

    private void Strobe()
    {
        if (strobeDelayTimer <= 0f)
        {
            strobeDelayTimer = strobeDelay;

            toggle = !toggle;

            if (toggle)
                spriteRenderer.enabled = true;
            else
                spriteRenderer.enabled = false;
        }
        else
            strobeDelayTimer -= Time.deltaTime;
    }

    IEnumerator Explode()
    {
        spriteRenderer.color = new Color(2.0f, 1.0f, 0.0f, 0.5f); // changes color of enemy to yellow
       GameObject test =  Instantiate(explosionObj.gameObject, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(.5f); // waits so that the color can actually change before it is destroyed



        // Destroy(gameObject);
        spriteRenderer.enabled = false;
        circleRenderer.enabled = false;
        
        bExplode = false;
        attackPlayer = false;
        respawning = true;

       // yield return new WaitForSeconds(1f);

       Destroy(test);
    }

    IEnumerator WallBreak(Vector3 tilePos)
    {
        GameObject test = Instantiate(explosionObj.gameObject, tilePos, Quaternion.identity);
        yield return new WaitForSeconds(.5f); // waits so that the color can actually change before it is destroyed

        Destroy(test);
    }
}
