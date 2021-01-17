﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Movement logic script for the Door Mimic "enemy"
public class RyanGarvanDoorMimicMove : MonoBehaviour
{
    SpriteRenderer m_eyeSprite;      // Eye sprite component
    Collider2D m_childCollider;      // Collider component of child object (the door itself)
    RyanGarvanAStarPather m_pather;  // Pathfinding component
    GameObject m_player;             // Player character
    bool m_isAwakened = false;       // Whether the door's eye is open yet (opens when approached for the first time)
    public float m_speed = 20;       // The door's move speed
    public float m_viewDistance = 5; // How far the door can see
    public float m_pathRate = 1;
    float m_pathCountdown = 0;
    public int m_maxFleeDistance = 10;
    List<Vector3> m_path;

    // Start is called before the first frame update
    void Start()
    {
        m_eyeSprite = GetComponent<SpriteRenderer>(); // Get eye sprite
        m_eyeSprite.enabled = false;                  // Eye sprite starts hidden

        m_childCollider = transform.GetChild(0).GetComponent<Collider2D>();

        m_pather = GetComponent<RyanGarvanAStarPather>();

        m_player = GameObject.FindGameObjectWithTag("Player"); // Get player character
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 vec_to_player = m_player.transform.position - (transform.position + new Vector3(0, 1, 0)); // Vector from door to player
        float dist_from_player = vec_to_player.magnitude;                         // Distance from door to player

        List<RaycastHit2D> results = new List<RaycastHit2D>(); // List of raycast hits
        ContactFilter2D filter = new ContactFilter2D();        // Contact filter for raycast

        // Perform a raycast to see if the door has a line of sight to the player
        Physics2D.Raycast(transform.position, vec_to_player.normalized, filter, results, m_viewDistance);

        // If nothing was hit or the player was the first object hit, the door can see the player
        bool can_see_player = results.Count == 0 || results[0].collider.gameObject == m_player;

        // If the door is not yet awake, wait for the player to get close
        if (!m_isAwakened)
        {
            // If the door is open, the player is close, and the door has a line of sight to the player, wake up
            if (m_childCollider.enabled && dist_from_player <= 2 && can_see_player)
            {
                m_eyeSprite.enabled = true; // Make eye sprite visible
                m_isAwakened = true;        // Enable movement
            }
        }
        // If the door is awake, run away from the player if they're close
        else
        {
            // Raycast in the direction of the player to see if they're hiding behind anything

            if (m_pathCountdown > 0)
            {
                m_pathCountdown -= Time.deltaTime;
            }
            // If the player is close, run away
            else if (dist_from_player <= m_viewDistance && can_see_player)
            {
                m_pathCountdown = m_pathRate;
                m_path = m_pather.GetFleePath(transform.position, 100, m_player.transform.position);
                if (m_path.Count > m_maxFleeDistance)
                {
                    m_path.RemoveRange(m_maxFleeDistance, m_path.Count - m_maxFleeDistance);
                }
            }

            if (m_path.Count > 0)
            {
                Vector3 nextPos = m_path[0];
                nextPos.z = transform.position.z;
                Vector3 toNextPos = nextPos - transform.position;
                float dx = toNextPos.normalized.x * m_speed * Time.deltaTime;
                float dy = toNextPos.normalized.y * m_speed * Time.deltaTime;

                if (Mathf.Abs(dx) >= Mathf.Abs(toNextPos.x))
                {
                    transform.position = new Vector3(nextPos.x, transform.position.y, transform.position.z);
                }
                else
                {
                    transform.position = transform.position + new Vector3(dx, 0, 0);
                }

                if (Mathf.Abs(dy) >= Mathf.Abs(toNextPos.y))
                {
                    transform.position = new Vector3(transform.position.x, nextPos.y, transform.position.z);
                }
                else
                {
                    transform.position = transform.position + new Vector3(0, dy, 0);
                }

                if (transform.position == nextPos)
                {
                    m_path.RemoveAt(0);
                }
            }
            else
            {

            }
        }
    }
}
