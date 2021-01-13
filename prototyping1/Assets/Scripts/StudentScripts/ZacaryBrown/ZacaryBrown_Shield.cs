using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

public class ZacaryBrown_Shield : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;

        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos) - transform.position;
        worldPos.z = 0;
        transform.right = worldPos;
        
        transform.SetPositionAndRotation(player.transform.position, transform.rotation);
    }
}
