using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mklingmanPlayerWrapper : MonoBehaviour
{
    [Header("Hotkeys")]
    public KeyCode dash;
    public KeyCode attack;

    [SerializeField] private mklingmanPlayerDash _dash;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(dash))
        {
            _dash.Dash();
        } 
        if (Input.GetKeyDown(attack))
        {
            //attack.
        }
    }
}
