using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JiwonJung_Proliferation : MonoBehaviour
{
    public float time_limit = 20.0f;
    public GameObject monster;
    private float new_x;

    // Update is called once per frame
    void Update()
    {
        time_limit -= Time.deltaTime;

        if(time_limit <= 0)
        {
            time_limit = 20.0f;
            GameObject new_monster = GameObject.Instantiate(monster);
            new_x = transform.position.x - 2.0f;
            new_monster.transform.position = new Vector3(new_x, transform.position.y, transform.position.z);
            
        }
    }
}
