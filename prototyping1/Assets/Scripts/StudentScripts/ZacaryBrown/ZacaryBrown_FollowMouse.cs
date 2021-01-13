using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZacaryBrown_FollowMouse : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;

        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        this.transform.SetPositionAndRotation(worldPos, Quaternion.identity);
    }
}
