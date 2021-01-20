using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZacaryBrown_Spawner : MonoBehaviour
{
    public int maxSummons = 1;
    public GameObject Enemy;
    private List<GameObject> Enemies;
    
    // Start is called before the first frame update
    void Start()
    {
        Enemies = new List<GameObject>(maxSummons);

        for (int i = 0; i < maxSummons; ++i)
        {
            Enemies.Add(null);
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < maxSummons; ++i)
        {
            if (Enemies[i] == null)
            {
                Enemies[i] = Instantiate(Enemy, transform.position, Quaternion.identity);
            }
        }
    }
}
