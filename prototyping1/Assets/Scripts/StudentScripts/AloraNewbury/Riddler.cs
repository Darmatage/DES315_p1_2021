using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Riddler : MonoBehaviour
{

    public GameObject riddler;
    public GameObject player;
    public Text display_text;
    public float distance;
    public bool isDone;
    public GameObject boxManager;


    // Start is called before the first frame update
    void Start()
    {
        display_text.enabled = false;
        distance = 10000000.0f;
        isDone = false;

    }

    void BeginRiddle()
    {
        display_text.enabled = false;

        boxManager.GetComponent<TextBoxManager>().SetShown();

    }

    // Update is called once per frame
    void Update()
    {

        float x_pos = Mathf.Abs(riddler.transform.position.x - player.transform.position.x) * Mathf.Abs(riddler.transform.position.x - player.transform.position.x);
        float y_pos = Mathf.Abs(riddler.transform.position.y - player.transform.position.y) * Mathf.Abs(riddler.transform.position.y - player.transform.position.y);

        distance = Mathf.Sqrt(x_pos + y_pos);


        if (distance < 1.0f && isDone == false)
        {
            
            display_text.enabled = true;
            if (Input.GetKeyDown(KeyCode.Return))
            {
                display_text.enabled = false;
                isDone = true;
                BeginRiddle();
            }
        }
        else
        {
            display_text.enabled = false;

        }

    }





}
