using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class pritzverQuinnsSpikes : MonoBehaviour
{

    public int damagePerTick = 10;
    public float tickTime = .75f;
    public float activeTime = 3.0f;
    public bool startActive = false;

    private bool isDamaging;
    private float damageTimer;
    private bool isActive;
    private float activeTimer;

    private pritzGameHandler GameHandlerObj;

    // Start is called before the first frame update
    void Start()
    {
        GameObject temp = GameObject.FindGameObjectWithTag("GameHandler");

        if(temp)
        {
            GameHandlerObj = temp.GetComponent<pritzGameHandler>();
        }

        isDamaging = false;

        SetActive(startActive);
    }

    // Update is called once per frame
    void Update()
    {
        activeTimer -= Time.deltaTime;
        if(activeTimer <= 0.0f)
        {
            SetActive(!isActive);
        }


        if (isActive && isDamaging)
        {
            damageTimer -= Time.deltaTime;
            if (damageTimer <= 0.0f)
            {
                GameHandlerObj.TakeDamage(damagePerTick);
                damageTimer += tickTime;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            isDamaging = true;
            damageTimer = 0.0f;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            isDamaging = false;
        }
    }

    private void SetActive(bool activate)
    {
        Tilemap map = gameObject.GetComponent<Tilemap>();
        map.color = activate ? Color.red : Color.white;

        isActive = activate;
        
        activeTimer = activeTime;
    }
}
