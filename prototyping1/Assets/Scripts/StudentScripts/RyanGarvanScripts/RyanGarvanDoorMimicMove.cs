using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RyanGarvanDoorMimicMove : MonoBehaviour
{
    SpriteRenderer m_eyeSprite;      // Eye sprite component
    Rigidbody2D m_rigidbody;         // Rigidbody component
    Collider2D m_collider;           // Collider component
    Collider2D m_childCollider;      // Collider component of child object (the door itself)
    GameObject m_player;             // Player character
    bool m_isAwakened = false;       // Whether the door's eye is open yet (opens when approached for the first time)
    public float m_speed = 20;       // The door's move speed
    public float m_viewDistance = 5; // How far the door can see
    public LayerMask m_wallMask;     // Mask that determines what the player can hide behind

    // Start is called before the first frame update
    void Start()
    {
        m_eyeSprite = GetComponent<SpriteRenderer>(); // Get eye sprite
        m_eyeSprite.enabled = false;                  // Eye sprite starts hidden

        m_rigidbody = GetComponent<Rigidbody2D>(); // Get door rigidbody

        m_collider = GetComponent<Collider2D>(); // Get door collider
        m_collider.enabled = false;              // Disable collider while door is asleep

        m_childCollider = transform.GetChild(0).GetComponent<Collider2D>();

        m_player = GameObject.FindGameObjectWithTag("Player"); // Get player character
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 vec_to_player = m_player.transform.position - transform.position; // Vector from door to player
        float dist_from_player = vec_to_player.magnitude;                         // Distance from door to player

        List<RaycastHit2D> results = new List<RaycastHit2D>();
        ContactFilter2D filter = new ContactFilter2D();
        m_collider.Raycast(vec_to_player.normalized, filter, results, m_viewDistance);

        bool can_see_player = results.Count == 0 || results[0].collider.gameObject == m_player;

        // If the door is not yet awake, wait for the player to get close
        if (!m_isAwakened)
        {
            // If the door is open, the player is close, and the door has a line of sight to the player, wake up
            if (m_childCollider.enabled && dist_from_player <= 2 && can_see_player)
            {
                m_eyeSprite.enabled = true; // Make eye sprite visible
                m_isAwakened = true;        // Enable movement
                m_collider.enabled = true;  // Enable collider for wall collision
            }
        }
        // If the door is awake, run away from the player if they're close
        else
        {
            // Raycast in the direction of the player to see if they're hiding behind anything
            

            // If the player is close, run away
            if (dist_from_player <= m_viewDistance && can_see_player)
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
