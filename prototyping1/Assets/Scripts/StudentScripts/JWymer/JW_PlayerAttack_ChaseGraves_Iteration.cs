// NOTE: The Majority of this code was written by Chase Graves. I had to make a copy of this script in order to manipulate it this way.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JW_PlayerAttack_ChaseGraves_Iteration : MonoBehaviour
{
    public GameObject Player;
    public GameObject Projectile;
    public InventoryHandlerJW inventoryHandler;
    public float AttackSpeed;

    private float attackTimer = 0.0f;
    private bool canAttack = true;

    // Start is called before the first frame update
    void Start()
    {
        if (inventoryHandler == null)
            inventoryHandler = GameObject.Find("JWInventoryHandler").GetComponent<InventoryHandlerJW>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!canAttack)
        {
            attackTimer += Time.deltaTime;

            if (attackTimer >= AttackSpeed)
            {
                attackTimer = 0.0f;
                canAttack = true;
            }
        }

        if (canAttack && Input.GetMouseButtonDown(0) && inventoryHandler.coinCount > 0)
        {
            Vector3 direction;
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            direction.x = mousePos.x - Player.transform.position.x;
            direction.y = mousePos.y - Player.transform.position.y;
            direction.z = 0.0f;
            GameObject projectile = Instantiate(Projectile, Player.transform.position, Quaternion.identity);
            projectile.GetComponent<ChaseGraves_ProjectileScript>().ChaseGraves_SetDirection(direction);

            canAttack = false;
            inventoryHandler.coinCount--;
        }
    }
}
