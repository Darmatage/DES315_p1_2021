using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AustinSteadProjectileBounce : MonoBehaviour
{
    private AustinSteadProjectile projectile;

    private Vector2 bounceDirection = new Vector2();
    private float bounceSpeed;
    private float bounceTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        projectile = GetComponent<AustinSteadProjectile>();
    }

    // Update is called once per frame
    void Update()
    {
        if(bounceTimer > 0)
        {
            bounceTimer -= Time.deltaTime;

            if (bounceTimer <= 0)
            {
                bounceTimer = 0;
            }
        }
    }



    public void GetBounced(Vector2 bounceDirection, float bounceSpeed, float bounceDuration)
    {
        this.bounceDirection = bounceDirection;
        this.bounceSpeed = bounceSpeed;
        bounceTimer = bounceDuration;

        projectile.Redirect(bounceDirection, bounceSpeed, bounceDuration);
    }
}
