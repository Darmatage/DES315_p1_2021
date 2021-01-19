using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashLogic : MonoBehaviour
{

    private GameObject player;
    public float DashDistance = 2.0f;
    public float CoolDown = 1.0f;
    private float CoolDownStart = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        CoolDownStart = CoolDown;
    }

    // Update is called once per frame
    void Update()
    {
        CoolDown -= Time.deltaTime;

        if(Input.GetKeyDown(KeyCode.LeftShift) && CoolDown <= 0.0f)
        {
            Vector2 pos = player.GetComponent<PlayerMove>().transform.position;

            if(Input.GetKey(KeyCode.W))
                pos.y += DashDistance;

            if (Input.GetKey(KeyCode.A))
                pos.x -= DashDistance;

            if (Input.GetKey(KeyCode.S))
                pos.y -= DashDistance;

            if (Input.GetKey(KeyCode.D))
                pos.x += DashDistance;

            player.GetComponent<PlayerMove>().transform.position = pos;
            CoolDown = CoolDownStart;
        }
    }
}
