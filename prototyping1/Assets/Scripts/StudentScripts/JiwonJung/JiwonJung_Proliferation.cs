using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JiwonJung_Proliferation : MonoBehaviour
{
    public float time_limit = 5.0f;
    public GameObject monster;
    private float new_x;

    private Renderer rend;
    public Color col;

    void Start()
    {
        rend = GetComponentInChildren<Renderer>();
        col = rend.material.color;
    }
    
    // Update is called once per frame
    void Update()
    {
        time_limit -= Time.deltaTime;

        if(time_limit <= 0)
        {
            time_limit = 5.0f;
            GameObject new_monster = GameObject.Instantiate(monster);
            new_x = transform.position.x - 2.0f;
            new_monster.transform.position = new Vector3(new_x, transform.position.y, transform.position.z);
        }

        if (time_limit <= 5.0f && time_limit >= 4.5f)
        {
            rend.material.SetColor("_Color", Color.blue);
        }
        else
        {
            rend.material.SetColor("_Color", Color.magenta);
        }
    }
}
