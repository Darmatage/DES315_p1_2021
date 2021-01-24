using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KT_Hover : MonoBehaviour
{
    public float Speed = 1.0f;
    public Vector3 Floatdir;
    private Vector3 OrigPos;
    private float Timer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        OrigPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = OrigPos + Floatdir * Mathf.Sin(Timer);
        Timer += Time.deltaTime * Speed;
    }
}
