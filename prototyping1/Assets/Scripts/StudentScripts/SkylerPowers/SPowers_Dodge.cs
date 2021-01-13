using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPowers_Dodge : MonoBehaviour
{
  private Vector3 change; // Player movement direction

  private Transform trans; // Player transform
  private Rigidbody2D rb2d; // Player ridigbody

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
    rb2d = gameObject.GetComponent<Rigidbody2D>();
  }

  // Update is called once per frame
  void Update()
  {
    if (dodgeTimer > 0f)
    {
      rb2d.MovePosition(trans.position + (change * dodgeDistance / dodgeDuration) * Time.deltaTime);
      dodgeTimer -= Time.deltaTime;
    }
    else if (cooldownTimer <= 0)
    {
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
      cooldownTimer -= Time.deltaTime;
    }
  }
}
