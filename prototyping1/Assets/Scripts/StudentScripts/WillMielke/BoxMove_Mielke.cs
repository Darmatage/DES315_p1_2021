using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxMove_Mielke : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionExit2D(Collision2D colExt)
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        if(colExt.gameObject.GetComponent<Rigidbody2D>() != null &&  colExt.gameObject.GetComponent<Rigidbody2D>().bodyType == RigidbodyType2D.Dynamic)
            colExt.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;

    }


}
