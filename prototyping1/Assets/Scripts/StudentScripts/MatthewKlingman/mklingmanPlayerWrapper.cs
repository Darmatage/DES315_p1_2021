using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class mklingmanPlayerWrapper : MonoBehaviour
{
    public enum ActionStates
    {
        Idle,
        Dash,
        Attack
    }
    
    [Header("Hotkeys")]
    public KeyCode dash;
    public KeyCode attack;

    [Header("Player")] 
    public GameObject player;
    // This will override the speed in the PlayerMove script.
    public float movementSpeed = 10.0f;

    [Header("Abilities")]
    public Color dashTint;
    public GameObject dashEffect;
    public float dashSpeed;
    public float dashDuration;
    private float dashTimer;
    private Vector3 dashDir;

    public Color attackTint;
    public GameObject attackEffect;
    public float attackDuration; 
    private float attackTimer;
    private Vector3 attackDir;

    private Vector3 dir = Vector2.right;
    private Renderer rend;
    private Animator anim;
    private Rigidbody2D rb2d;
    
    private bool isAlive = true;

    private ActionStates actionState = ActionStates.Idle;
    ///private ActionStates actionBuffer = ActionStates.Idle;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponentInChildren<Animator>();
        rend = GetComponentInChildren<Renderer>();
        
        if (player.GetComponent<Rigidbody2D>() != null) { 
            rb2d = player.GetComponent<Rigidbody2D>();
        }

        player.GetComponent<PlayerMove>().speed = movementSpeed;
        
        dashTimer = dashDuration;
    }

    // Update is called once per frame
    void Update()
    {
        
        
        if (!isAlive)
        {
            anim.SetTrigger("Dead");
            return;
        }

        dir = Vector2.zero;
        dir.x = Input.GetAxisRaw("Horizontal");
        dir.y = Input.GetAxisRaw("Vertical");

        // We don't want to accept input during an action. Not yet, anyway.
        if(actionState == ActionStates.Idle)
            GetInput();

        UpdateCurrentAction();
    }

    private void GetInput()
    {
        if (Input.GetKeyDown(dash))
        {
            actionState = ActionStates.Dash;
            rend.material.color = dashTint;
            player.GetComponent<PlayerMove>().speed = dashSpeed;
            
            GameObject fx = Instantiate(dashEffect, player.transform.position, quaternion.identity);
            Destroy(fx, 1.0f);
            
            if (dir == Vector3.zero)
            {
                if (player.transform.localScale.x < 0)
                    dashDir = Vector2.left;
                else
                    dashDir = Vector2.right;
            }
            else
            {
                dashDir = dir;
            }
        }

        if (Input.GetKeyDown(attack))
        {
            actionState = ActionStates.Attack;
            rend.material.color = attackTint;
            anim.SetTrigger("Attack");
            player.GetComponent<PlayerMove>().speed = 0f;

            if (dir == Vector3.zero)
            {
                if (player.transform.localScale.x < 0)
                    attackDir = Vector2.left;
                else
                    attackDir = Vector2.right;
            }
            else
            {
                attackDir = dir;
            }

            GameObject fx = Instantiate(attackEffect, player.transform.position + attackDir * 1.5f, quaternion.identity);
            Destroy(fx, 0.6f);
        }
    }

    private void UpdateCurrentAction()
    {
        if (actionState == ActionStates.Idle)
        {
            UpdateMovement();
        }
        else if (actionState == ActionStates.Dash)
        {
            UpdateDash();
        }
        else if (actionState == ActionStates.Attack)
        {
            UpdateAttack();
        }
    }

    private void UpdateMovement()
    {
        if (dir != Vector3.zero) {
            anim.SetBool("Walk", true);
        } else {
            anim.SetBool("Walk", false);
        }
    }

    private void UpdateDash()
    {
        if (dashTimer <= 0)
        {
            actionState = ActionStates.Idle;
            dashTimer = dashDuration;
            rend.material.color = Color.white;
            player.GetComponent<PlayerMove>().speed = movementSpeed;
        }
        else
        {
            dashTimer -= Time.deltaTime;
            rb2d.MovePosition(player.transform.position + dashDir * (dashSpeed * Time.deltaTime));
        }
    }

    private void UpdateAttack()
    {
        if (attackTimer <= 0)
        {
            actionState = ActionStates.Idle;
            attackTimer = attackDuration;
            rend.material.color = Color.white;
            player.GetComponent<PlayerMove>().speed = movementSpeed;
        }
        else
        {
            attackTimer -= Time.deltaTime;
            
            
        }
    }

}
