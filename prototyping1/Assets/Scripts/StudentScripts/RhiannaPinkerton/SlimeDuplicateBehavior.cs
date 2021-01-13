using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeDuplicateBehavior : MonoBehaviour
{
    [SerializeField] private GameObject SlimePrefab;
    [SerializeField] public float SpawnTimerLength;
    private float SpawnTimer;
    
    // Start is called before the first frame update
    void Start()
    {
        SpawnTimer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        SpawnTimer += Time.deltaTime;

        // Time is up so spawn a slime
        if (SpawnTimer >= SpawnTimerLength)
        {
            GameObject slime = Instantiate(SlimePrefab, transform.position, Quaternion.identity);
            // TODO This is temporary until shooting is implemented
            slime.GetComponent<SlimeDuplicateBehavior>().SpawnTimerLength = Random.Range(2f, 4f);
            SpawnTimer = 0f;
        }
    }
}
