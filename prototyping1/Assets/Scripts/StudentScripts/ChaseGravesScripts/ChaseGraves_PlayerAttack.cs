using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseGraves_PlayerAttack : MonoBehaviour
{
    public GameObject Player;
    public GameObject Projectile;
    public float AttackSpeed;

    private float attackTimer = 0.0f;
    private bool canAttack = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!canAttack)
        {
            attackTimer += Time.deltaTime;

            if(attackTimer >= AttackSpeed)
            {
                attackTimer = 0.0f;
                canAttack = true;
            }
        }

        if(canAttack && Input.GetMouseButtonDown(0))
        {
            Vector3 direction;
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            direction.x = mousePos.x - Player.transform.position.x;
            direction.y = mousePos.y - Player.transform.position.y;
            direction.z = 0.0f;
            GameObject projectile = Instantiate(Projectile, Player.transform.position, Quaternion.identity);
            projectile.GetComponent<ChaseGraves_ProjectileScript>().ChaseGraves_SetDirection(direction);

            canAttack = false;
        }
    }
}
