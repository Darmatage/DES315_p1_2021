using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPowers_Dodge : MonoBehaviour
{
  private Vector3 change; // Player movement direction

  private Transform trans; // Player transform

  private SpriteRenderer rend; // Child sprite renderer

  public float dodgeCooldown; // Max cooldown
  private float cooldownTimer; // Current cooldown

  public float dodgeDistance; // How far go

  public float dodgeDuration; // How long does the dodge last
  private float dodgeTimer; // How long does the dodge have left

  // Start is called before the first frame update
  void Start()
  {
    gameObject.tag = "Player";
    cooldownTimer = 0f;
    trans = gameObject.GetComponent<Transform>();
    rend = GetComponentInChildren<SpriteRenderer>();
  }

  void OnCollisionEnter2D(Collision2D collision)
  {
    if (collision.gameObject.name == "TilemapWalls" || collision.gameObject.name == "TilemapSpikes")
    {
      dodgeTimer = 0f;
    }
  }

  // Physics update
  void FixedUpdate()
  {
    if (dodgeTimer > 0f)
    {
      Vector3 pos = (change * dodgeDistance / dodgeDuration) * Time.fixedDeltaTime;
      trans.position += pos;
      dodgeTimer -= Time.fixedDeltaTime;
    }
  }

  // Update is called once per frame
  void Update()
  {
    if (dodgeTimer > 0f)
    {
      rend.color = new Color(0.3f, 0f, 0.7f);
      return;
    }

    if (cooldownTimer <= 0)
    {
      rend.color = Color.white;
      if (Input.GetButtonDown("Fire3"))
      {
        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");

        cooldownTimer = dodgeCooldown;
        dodgeTimer = dodgeDuration;
      }
    }
    else
    {
      rend.color = new Color(0.8f, 0.8f, 0.8f);
      cooldownTimer -= Time.deltaTime;
    }
  }
}
