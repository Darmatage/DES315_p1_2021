using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lorenzoFireDebuff : MonoBehaviour
{
    public int dmgAmount = 5;
    public float damageTickTime = 1;
    public GameObject playerArt;
    public AudioClip sizzleSFX;


    private GameHandler gameHandlerObj;
    private GameObject player;
    private GameObject fireDebuff;
    private bool active = false;
    private float damageTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.FindGameObjectWithTag("GameHandler") != null)
        {
            gameHandlerObj = GameObject.FindGameObjectWithTag("GameHandler").GetComponent<GameHandler>();
        }


        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z) ;


        if (active)
        {
            damageTimer += Time.deltaTime;
            playerArt.GetComponent<SpriteRenderer>().material.color = new Color(0.9f, 0.5f, 0.3f, 1.0f);


            if (damageTimer >= damageTickTime)
            {
                gameHandlerObj.TakeDamage(dmgAmount);
                damageTimer = 0f;
            }
        }
    }


    public void setActive(bool a)
    {
        if (a)
        {
            if (!active)
            {
                this.GetComponent<ParticleSystem>().Play();
                this.GetComponent<AudioSource>().Play();
            }
        }
        else
        {
            if (active)
            {
                playerArt.GetComponent<SpriteRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f);
                this.GetComponent<ParticleSystem>().Stop();
                this.GetComponent<AudioSource>().PlayOneShot(sizzleSFX);
            }
        
        }


        active = a;
    }

    public bool getActive()
    {
        return active;
    }
}

