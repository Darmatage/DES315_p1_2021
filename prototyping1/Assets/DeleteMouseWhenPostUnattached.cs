using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteMouseWhenPostUnattached : MonoBehaviour
{
    public GameObject mouse;

    // Update is called once per frame
    void Update()
    {
        if(!GetComponent<KT_Post>().IsAttached)
        {
            Destroy(mouse);
        }
    }
}
