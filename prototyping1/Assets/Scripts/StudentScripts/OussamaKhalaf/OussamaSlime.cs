using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OussamaSlime : MonoBehaviour
{

    public float replicationTime = 3.0f;
    public int replicationLimit = 5;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        timer = replicationTime;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            if (replicationLimit == -1 || replicationLimit > 0)
            {
                if (replicationLimit > 0)
                {
                    replicationLimit--;
                }
                GameObject Child = Instantiate(this.gameObject);

            }
            timer = replicationTime;
        }
    }
}
