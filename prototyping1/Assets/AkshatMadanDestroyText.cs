using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AkshatMadanDestroyText : MonoBehaviour
{
    public float delay;
    public GameObject boss;
    public GameObject spawner;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(LateCall());
    }

    IEnumerator LateCall()
    {
        yield return new WaitForSeconds(delay);

        boss.SetActive(true);
        spawner.SetActive(true);
        Destroy(gameObject, delay / 4.0f); // Destroy this after delay
    }
}
