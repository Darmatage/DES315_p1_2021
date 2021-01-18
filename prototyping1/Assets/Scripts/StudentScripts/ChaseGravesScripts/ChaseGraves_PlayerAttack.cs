using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseGraves_PlayerAttack : MonoBehaviour
{
    public GameObject Player;
    public GameObject Projectile;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector3 direction;
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            direction.x = mousePos.x - Player.transform.position.x;
            direction.y = mousePos.y - Player.transform.position.y;
            direction.z = 0.0f;
            GameObject projectile = Instantiate(Projectile, Player.transform.position, Quaternion.identity);
            projectile.GetComponent<ChaseGraves_ProjectileScript>().ChaseGraves_SetDirection(direction);
        }
    }
}
