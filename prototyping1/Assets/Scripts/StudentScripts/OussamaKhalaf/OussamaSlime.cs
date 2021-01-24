using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OussamaSlime : MonoBehaviour
{

    public float replicationTime;
    public Gradient gradient;
    public int replicationLimit;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        timer = replicationTime;
    }

    // Update is called once per frame
    void Update()
    {

        if (replicationLimit == 0 || !gameObject.GetComponentInChildren<SpriteRenderer>().isVisible)
        {

            return;
        }

        timer -= Time.deltaTime;
        this.gameObject.GetComponentInChildren<SpriteRenderer>().color = gradient.Evaluate(timer / replicationTime);
        if (timer <= 0)
        {
            if (replicationLimit != -1)
            {
                replicationLimit--;
            }
            timer = replicationTime;

            GameObject child = Instantiate(this.gameObject);
            child.GetComponent<OussamaSlime>().replicationTime = replicationTime;
            child.transform.position = this.transform.position;
            child.GetComponent<OussamaSlime>().replicationLimit = replicationLimit;
        }
    }
}
