using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AkshatMadanWaveHandler : MonoBehaviour
{
    public int waveNumber = 1;
    public int maxWaves = 3;
    public GameObject waveText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        waveText.GetComponent<Text>().text = "WAVE: " + waveNumber.ToString() + " / " + maxWaves.ToString(); 
    }
}
