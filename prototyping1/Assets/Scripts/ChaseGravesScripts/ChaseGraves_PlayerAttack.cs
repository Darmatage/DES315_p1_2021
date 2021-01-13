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
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(Projectile, Player.transform.position, Quaternion.identity);
        }
    }
}
