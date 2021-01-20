using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Movement logic script for the Door Mimic "enemy"
public class RyanGarvanDoorMimicMove : MonoBehaviour
{
    Transform m_children;                  // Empty GameObject containing all children
    SpriteRenderer m_eyeSprite;            // Eye sprite component
    SpriteRenderer m_pupilSprite;          // Pupil sprite component
    Sprite m_pupilImageNormal;             // Normal pupil sprite
    Sprite m_eyeImageNormal;
    Sprite m_outlineImageNormal;
    public Sprite m_pupilImageScared;      // Scared pupil sprite
    public Sprite m_eyeImageWide;
    public Sprite m_outlineImageWide;
    Vector3 m_pupilInitPos;                // Initial position of the door's pupil relative to the door
    SpriteRenderer m_outlineSprite;        // Eye outline sprite component
    Collider2D m_doorCollider;             // Collider component of child object (the door itself)
    Vector3 m_doorInitPos;                 // Initial position of the base door relative to the door mimic
    RyanGarvanAStarPather m_pather;        // Pathfinding component
    GameObject m_player;                   // Player character
    bool m_isAwakened = false;             // Whether the door's eye is open yet (opens when approached for the first time)
    public float m_speed = 20;             // The door's move speed
    public float m_viewDistance = 10;      // How far the door can see
    public float m_pathRate = 1;           // The minimum number of seconds between pathfinding checks
    float m_pathCountdown = 0;             // The number of seconds until the next pathfinding check
    public int m_maxFleeDistance = 10;     // The maximum distance the door will search to find an escape route
    List<Vector3> m_path;                  // The path the door is currently following
    float m_lookAngle = 0;                 // The angle at which the door is looking
    float m_awarenessDistance = 2.5f;      // The radius within which the door can hear the player
    float m_searchSpeed = Mathf.PI / 1.5f; // The speed at which the door looks around when it can't see the player
    AudioSource m_audioSource;             // Audio source component
    public AudioClip m_jumpSound;          // The door's jumping sound
    float m_pathStartTime;                 // The time at which the door last started moving

    // Start is called before the first frame update
    void Start()
    {
        m_children = transform.GetChild(0);

        m_eyeSprite = m_children.GetChild(0).GetComponent<SpriteRenderer>(); // Get eye sprite
        m_eyeSprite.enabled = false;                  // Eye sprite starts hidden
        m_eyeImageNormal = m_eyeSprite.sprite;

        m_pupilSprite = m_children.GetChild(1).GetComponent<SpriteRenderer>();
        m_pupilInitPos = m_pupilSprite.transform.localPosition;
        m_pupilImageNormal = m_pupilSprite.sprite;
        m_pupilSprite.enabled = false;

        m_outlineSprite = m_children.GetChild(2).GetComponent<SpriteRenderer>();
        m_outlineSprite.enabled = false;
        m_outlineImageNormal = m_outlineSprite.sprite;

        m_doorCollider = m_children.GetChild(3).GetComponent<Collider2D>();

        m_doorInitPos = m_children.localPosition;

        m_pather = GetComponent<RyanGarvanAStarPather>();
        m_path = new List<Vector3>();

        m_audioSource = GetComponent<AudioSource>();

        m_player = GameObject.FindGameObjectWithTag("Player"); // Get player character
    }

    // Update is called once per frame
    void Update()
    {
        m_children.localPosition = m_doorInitPos;

        Vector2 eyePos = transform.position;
        Vector2 vec_to_player = (Vector2)m_player.transform.position - new Vector2(0, 0.5f) - eyePos; // Vector from door to player
        float dist_from_player = vec_to_player.magnitude;                         // Distance from door to player

        List<RaycastHit2D> results = new List<RaycastHit2D>(); // List of raycast hits
        ContactFilter2D filter = new ContactFilter2D();        // Contact filter for raycast

        // Perform a raycast to see if the door has a line of sight to the player
        Physics2D.Raycast(eyePos + vec_to_player.normalized, vec_to_player.normalized, filter, results, m_viewDistance);

        float angleToPlayer = Mathf.Atan2(-vec_to_player.y, vec_to_player.x);

        if (angleToPlayer < 0)
        {
            angleToPlayer += Mathf.PI * 2;
        }
        
        if (dist_from_player <= m_awarenessDistance)
        {
            m_lookAngle = Mathf.Atan2(-vec_to_player.y, vec_to_player.x);
        }

        // If nothing was hit or the player was the first object hit, the door can see the player
        bool can_see_player = (results.Count == 0 || results[0].collider.gameObject == m_player) && Mathf.Abs(Mathf.DeltaAngle(Mathf.Rad2Deg * angleToPlayer, Mathf.Rad2Deg * m_lookAngle) * Mathf.Deg2Rad) < Mathf.PI / 3.0f && dist_from_player <= m_viewDistance;

        // If the door is not yet awake, wait for the player to get close
        if (!m_isAwakened)
        {
            // If the door is open, the player is close, and the door has a line of sight to the player, wake up
            if (m_doorCollider.enabled && dist_from_player <= m_awarenessDistance)
            {
                m_eyeSprite.enabled = true; // Make eye sprite visible
                m_pupilSprite.enabled = true; // Make pupil sprite visible
                m_outlineSprite.enabled = true; // Make eye outline sprite visible
                m_isAwakened = true;        // Enable movement
            }
        }
        // If the door is awake, run away from the player if they're close
        else
        {
            m_pupilSprite.sprite = m_pupilImageNormal;
            m_eyeSprite.sprite = m_eyeImageNormal;
            m_outlineSprite.sprite = m_outlineImageNormal;
            
            if (m_pathCountdown > 0)
            {
                m_pathCountdown -= Time.deltaTime;
            }
            // If the player is close, run away
            else if (dist_from_player <= m_viewDistance && can_see_player)
            {
                if (m_path.Count == 0)
                {
                    m_pathStartTime = Time.time;
                }

                m_pathCountdown = m_pathRate;
                m_path = m_pather.GetFleePath(transform.position, 100, m_player.transform.position);
                if (m_path.Count > m_maxFleeDistance)
                {
                    m_path.RemoveRange(m_maxFleeDistance, m_path.Count - m_maxFleeDistance);
                }
            }
            
            if (m_path.Count > 0)
            {
                m_eyeSprite.sprite = m_eyeImageWide;
                m_outlineSprite.sprite = m_outlineImageWide;

                m_children.localPosition += new Vector3(0, Mathf.Abs(Mathf.Sin((Time.time - m_pathStartTime) * 10.0f) / 2.0f), 0);
                if (Mathf.Sign(Mathf.Sin((Time.time - m_pathStartTime) * 10.0f)) != Mathf.Sign(Mathf.Sin((Time.time - Time.deltaTime - m_pathStartTime) * 10.0f)))
                {
                    m_audioSource.pitch = Random.Range(0.75f, 1.25f);
                    m_audioSource.Play();
                }

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

                    if (m_path.Count > 0)
                    {
                        List<Collider2D> results2 = new List<Collider2D>(Physics2D.OverlapPointAll(m_path[0]));

                        foreach (Collider2D collider in results2)
                        {
                            if (!collider.isTrigger)
                            {
                                Rigidbody2D rb = collider.GetComponent<Rigidbody2D>();

                                if (rb == null || rb.bodyType == RigidbodyType2D.Static)
                                {
                                    m_path.Clear();
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                if (!can_see_player)
                {
                    m_lookAngle += m_searchSpeed * Time.deltaTime;
                }
                else
                {
                    m_children.localPosition += new Vector3(Mathf.Sin(Time.time * 40.0f) / 32.0f, 0, 0);
                    m_pupilSprite.sprite = m_pupilImageScared;
                }
            }

            if (can_see_player)
            {
                m_lookAngle = Mathf.Atan2(-vec_to_player.y, vec_to_player.x);
            }

            if (m_lookAngle >= Mathf.PI * 2)
                m_lookAngle -= Mathf.PI * 2;
            if (m_lookAngle < 0)
                m_lookAngle += Mathf.PI * 2;

            m_pupilSprite.transform.localPosition = m_pupilInitPos + new Vector3(Mathf.Cos(m_lookAngle) / 16.0f, -Mathf.Sin(m_lookAngle) / 16.0f, 0);
        }
    }
}
