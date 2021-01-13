using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseGraves_ProjectileScript : MonoBehaviour
{
    public int damage;
    public float speed;
    public float life;

    private Vector3 direction;
    private float timer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        direction.x = 1.0f;
        direction.y = 0.0f;
        direction.z = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + direction * speed;

        timer += Time.deltaTime;
        if(timer >= life)
        {
            Destroy(gameObject);
        }
    }
}
