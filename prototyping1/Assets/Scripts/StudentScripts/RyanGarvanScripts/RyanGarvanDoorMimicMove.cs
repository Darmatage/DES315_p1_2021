using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RyanGarvanDoorMimicMove : MonoBehaviour
{
    SpriteRenderer m_eyeSprite; // Eye sprite component
    Rigidbody2D m_rigidbody;    // Rigidbody component
    Collider2D m_collider;      // Collider component
    GameObject m_player;        // Player character
    bool m_isAwakened = false;  // Whether the door's eye is open yet (opens when approached for the first time)
    public float m_speed = 20;  // The door's move speed

    // Start is called before the first frame update
    void Start()
    {
        m_eyeSprite = GetComponent<SpriteRenderer>(); // Get eye sprite
        m_eyeSprite.enabled = false;                  // Eye sprite starts hidden

        m_rigidbody = GetComponent<Rigidbody2D>(); // Get door rigidbody

        m_collider = GetComponent<Collider2D>(); // Get door collider

        m_player = GameObject.FindGameObjectWithTag("Player"); // Get player character
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 vec_to_player = m_player.transform.position - transform.position; // Vector from door to player
        float dist_from_player = vec_to_player.magnitude;                         // Distance from door to player

        // If the door is not yet awake, wait for the player to get close
        if (!m_isAwakened)
        {
            // If the player is close, wake up
            if (dist_from_player <= 2)
            {
                m_eyeSprite.enabled = true; // Make eye sprite visible
                m_isAwakened = true;        // Enable movement
            }
        }
        // If the door is awake, run away from the player if they're close
        else
        {
            // If the player is close, run away
            if (dist_from_player <= 5)
            {
                m_rigidbody.velocity = -vec_to_player.normalized * m_speed; // Run directly away from player
            }
            // Otherwise, stay still
            else
            {
                m_rigidbody.velocity = Vector2.zero;
            }
        }
    }
}
