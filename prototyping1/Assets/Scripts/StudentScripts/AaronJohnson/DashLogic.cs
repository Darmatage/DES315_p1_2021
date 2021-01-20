using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashLogic : MonoBehaviour
{

    private GameObject player;
    public float DashDistance = 2.0f;
    public float CoolDown = 1.0f;
    private float CoolDownStart = 1.0f;
    public Slider CoolDownSlider;

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
        if (CoolDown < 0.0f)
            CoolDown = 0.0f;

        CoolDownSlider.value = 1 - (CoolDown / CoolDownStart);

        if(Input.GetKeyDown(KeyCode.LeftShift) && CoolDown == 0.0f)
        {
            Vector2 pos = player.GetComponent<PlayerMove>().transform.position;

            if(Input.GetAxis("Vertical") > 0)
                pos.y += DashDistance;

            if (Input.GetAxis("Horizontal") < 0)
                pos.x -= DashDistance;

            if (Input.GetAxis("Vertical") < 0)
                pos.y -= DashDistance;

            if (Input.GetAxis("Horizontal") > 0)
                pos.x += DashDistance;

            player.GetComponent<PlayerMove>().transform.position = pos;
            CoolDown = CoolDownStart;
        }
    }
}
