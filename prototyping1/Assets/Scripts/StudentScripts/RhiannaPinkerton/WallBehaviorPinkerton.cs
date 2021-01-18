using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBehaviorPinkerton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {   
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // Projectile hit
        if (other.gameObject.name.Contains("ProjectilePinkerton"))
        {
            // Do behavior for hitting a wall
            other.gameObject.GetComponent<SlimeProjectileRP>().WallHit();
        }
    }
}
