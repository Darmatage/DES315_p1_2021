using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lorenzoFireDebuff : MonoBehaviour
{
    public int dmgAmount = 5;
    public float damageTickTime = 1;


    private GameHandler gameHandlerObj;
    private bool active = false;
    private float damageTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.FindGameObjectWithTag("GameHandler") != null)
        {
            gameHandlerObj = GameObject.FindGameObjectWithTag("GameHandler").GetComponent<GameHandler>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            damageTimer += Time.deltaTime;

            if (damageTimer >= damageTickTime)
            {
                gameHandlerObj.TakeDamage(dmgAmount);
                damageTimer = 0f;
            }
        }
    }


    public void setActive(bool a)
    {
        active = a;
    }
}

