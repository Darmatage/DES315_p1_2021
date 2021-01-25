using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

using UnityEngine.UI;

public class TextBoxManager : MonoBehaviour
{
    public GameObject text_box;
    public Text display_text;
    public Text continue_text;
    public Text try_text;

    private TextAsset text_file;
    public TextAsset text_file_1;
    public TextAsset text_file_2;
    public TextAsset text_file_3;
    public TextAsset text_file_4;
    public TextAsset text_file_5;

    public GameObject DoorObj;

    public string[] text_lines;
    public string[] answers;

    public int current_line_counter;
    public int end_line_counter;


    public bool answer1_correct;
    public bool answer2_correct;
    public bool answer3_correct;


    
    public Button answer1;
    public Button answer2;
    public Button answer3;

    public GameObject playerObj;
    public GameObject handlerObj;

    public int riddle;


    public void SetShown()
    {
        display_text.enabled = true;
        continue_text.enabled = true;
        answer1.gameObject.SetActive(false);
        answer2.gameObject.SetActive(false);
        answer3.gameObject.SetActive(false);
        answer1.enabled = false;
        answer2.enabled = false;
        answer3.enabled = false;
        text_box.SetActive(true);
    }

    public void GetAnswers()
    {
        switch (riddle)
        {
            case 1:
                answer1.GetComponentInChildren<Text>().text = "Rock";
                answer1_correct = false;
                answer2.GetComponentInChildren<Text>().text = "Fire";
                answer2_correct = true;
                answer3.GetComponentInChildren<Text>().text = "Dirt";
                answer3_correct = false;
                break;
            case 2:
                answer1.GetComponentInChildren<Text>().text = "Bucket";
                answer1_correct = false;
                answer2.GetComponentInChildren<Text>().text = "Hair";
                answer2_correct = false;
                answer3.GetComponentInChildren<Text>().text = "Sponge";
                answer3_correct = true;
                break;
            case 3:
                answer1.GetComponentInChildren<Text>().text = "Future";
                answer1_correct = true;
                answer2.GetComponentInChildren<Text>().text = "Eyes";
                answer2_correct = false;
                answer3.GetComponentInChildren<Text>().text = "Oxygen";
                answer3_correct = false;
                break;
            case 4:
                answer1.GetComponentInChildren<Text>().text = "Ego";
                answer1_correct = false;
                answer2.GetComponentInChildren<Text>().text = "Age";
                answer2_correct = true;
                answer3.GetComponentInChildren<Text>().text = "Satellites";
                answer3_correct = false;
                break;
            case 5:
                answer1.GetComponentInChildren<Text>().text = "Family Tree";
                answer1_correct = false;
                answer2.GetComponentInChildren<Text>().text = "Bank";
                answer2_correct = true;
                answer3.GetComponentInChildren<Text>().text = "Departments";
                answer3_correct = false;
                break;
        }
    }


    public void IsAnswerCorrect(int button_number)
    {

        switch (button_number)
        {
            case 1:
                if (answer1_correct)
                {
                    DoorObj.GetComponent<Door>().DoorOpen();
                    answer1.enabled = false;
                    answer2.enabled = false;
                    answer3.enabled = false;
                    answer1.gameObject.SetActive(false);
                    answer2.gameObject.SetActive(false);
                    answer3.gameObject.SetActive(false);


                    text_box.SetActive(false);
                    display_text.enabled = false;
                    continue_text.enabled = false;
                    try_text.gameObject.SetActive(false);

                }
                else
                {
                    playerObj.GetComponent<PlayerMove>().playerHit();
                    handlerObj.GetComponent<GameHandler>().TakeDamage(50);
                    try_text.gameObject.SetActive(true);
                    answer1.enabled = false;
                    answer1.gameObject.SetActive(false);
                    if ((answer1.IsActive() == false && answer2.IsActive() == false) || answer3.IsActive() == false && answer1.IsActive() == false)
                    {
                        playerObj.GetComponent<PlayerMove>().playerDie();
                        SceneManager.LoadScene("EndLose");


                    }


                }
                break;
            case 2:
                if (answer2_correct)
                {
                    DoorObj.GetComponent<Door>().DoorOpen();
                    answer1.enabled = false;
                    answer2.enabled = false;
                    answer3.enabled = false;
                    answer1.gameObject.SetActive(false);
                    answer2.gameObject.SetActive(false);
                    answer3.gameObject.SetActive(false);


                    text_box.SetActive(false);
                    display_text.enabled = false;
                    continue_text.enabled = false;
                    try_text.gameObject.SetActive(false);


                }
                else 
                {
                    playerObj.GetComponent<PlayerMove>().playerHit();
                    handlerObj.GetComponent<GameHandler>().TakeDamage(50);
                    try_text.gameObject.SetActive(true);
                    answer2.enabled = false;
                    answer2.gameObject.SetActive(false);

                    if ((answer3.IsActive() == false && answer2.IsActive() == false) || answer2.IsActive() == false && answer1.IsActive() == false)
                    {
                        playerObj.GetComponent<PlayerMove>().playerDie();
                        SceneManager.LoadScene("EndLose");


                    }


                }
                break;
            case 3:
                if (answer3_correct)
                {
                    DoorObj.GetComponent<Door>().DoorOpen();
                    answer1.enabled = false;
                    answer2.enabled = false;
                    answer3.enabled = false;
                    answer1.gameObject.SetActive(false);
                    answer2.gameObject.SetActive(false);
                    answer3.gameObject.SetActive(false);


                    text_box.SetActive(false);
                    display_text.enabled = false;
                    continue_text.enabled = false;
                    try_text.gameObject.SetActive(false);


                }
                else 
                {
                    playerObj.GetComponent<PlayerMove>().playerHit();
                    handlerObj.GetComponent<GameHandler>().TakeDamage(50);
                    answer3.enabled = false;
                    answer3.gameObject.SetActive(false);

                    try_text.gameObject.SetActive(true);
                    if ((answer3.IsActive() == false && answer2.IsActive() == false) || answer3.IsActive() == false && answer1.IsActive() == false)
                    {
                        playerObj.GetComponent<PlayerMove>().playerDie();
                        SceneManager.LoadScene("EndLose");


                    }



                }
                break;
        }


    }
    public void DisplayAnswer()
    {
        answer1.gameObject.SetActive(true);
        answer2.gameObject.SetActive(true);
        answer3.gameObject.SetActive(true);
        answer1.enabled = true;
        answer2.enabled = true;
        answer3.enabled = true;
        continue_text.enabled = false;
        try_text.gameObject.SetActive(false);

    }


    // Start is called before the first frame update
    void Start()
    {
        answer1.enabled = false;
        answer2.enabled = false;
        answer3.enabled = false;
        answer1.gameObject.SetActive(false);
        answer2.gameObject.SetActive(false);
        answer3.gameObject.SetActive(false);


        text_box.SetActive(false);
        display_text.enabled = false;
        continue_text.enabled = false;
        try_text.gameObject.SetActive(false);

        current_line_counter = 0;
        //player = FindObjectOfType<PlayerMove>();

        riddle = 1;

        riddle = UnityEngine.Random.Range(1, 5);

        GetAnswers();
        switch (riddle)
        {
            case 1:
                text_file = text_file_1;
                break;
            case 2:
                text_file = text_file_2;
                break;
            case 3:
                text_file = text_file_3;
                break;
            case 4:
                text_file = text_file_4;
                break;
            case 5:
                text_file = text_file_5;
                break;
        }

        if(text_file)
        {
            text_lines = text_file.text.Split('\n');
        }


        if(end_line_counter == 0)
        {
            end_line_counter = text_lines.Length - 1;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (text_box.activeSelf == true)
        {
            display_text.text = text_lines[current_line_counter];
            if (Input.GetKeyDown(KeyCode.Space))
            {
                current_line_counter++;

                if (current_line_counter == end_line_counter)
                {
                    DisplayAnswer();

                }

            }
        }
    }
}
