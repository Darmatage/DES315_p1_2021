using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintTextPinkerton : MonoBehaviour
{

    [SerializeField] private float HintTimerLength;

    private float HintTimer;
    
    // Start is called before the first frame update
    void Start()
    {
        HintTimer = 0f;
        gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        HintTimer += Time.deltaTime;

        if (HintTimer >= HintTimerLength)
        {
            gameObject.SetActive(false);
        }
    }
}
