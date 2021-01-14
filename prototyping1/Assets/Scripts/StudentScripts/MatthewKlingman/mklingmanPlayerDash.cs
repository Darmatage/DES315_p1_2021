using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mklingmanPlayerDash : MonoBehaviour
{
    public float Distance;
    public float Duration;

    
    
    public void Dash()
    {
        Debug.Log("Activated Dash: Distance = " + Distance + ", Duration = " + Duration + ".");
    }
    
}
