using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;

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

    [Header("Abilities")]
    [SerializeField] private Color dashTint;
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashDuration;

    [SerializeField] private Color attackTint;

    private Vector3 dir = Vector2.up;
    private Renderer rend;
    private Animator anim;
    private Rigidbody2D rb2d;

    private ActionStates actionState = ActionStates.Idle;
    private ActionStates actionBuffer = ActionStates.Idle;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = player.gameObject.GetComponentInChildren<Animator>();
        rend = player.GetComponentInChildren<Renderer>();
        
        if (player.GetComponent<Rigidbody2D>() != null) { 
            rb2d = player.GetComponent<Rigidbody2D>();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        dir = Vector3.right;
        dir.x = Input.GetAxisRaw("Horizontal");
        dir.y = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(dash))
        {
            if (actionState == ActionStates.Idle)
                actionBuffer = ActionStates.Dash;
            else
            {
                actionState = ActionStates.Dash;
            }
        }

        if (Input.GetKeyDown(attack))
        {
            if (actionState == ActionStates.Idle)
                actionBuffer = ActionStates.Attack;
            else
            {
                actionState = ActionStates.Attack;
            }
        }

        if (actionState == ActionStates.Idle)
        {
            actionState = actionBuffer;
            actionBuffer = ActionStates.Idle;
        }
        
        PerformAction();
    }

    private void PerformAction()
    {
        switch (actionState)
        {
            case ActionStates.Idle:
                break;
            case ActionStates.Dash:
                StartCoroutine(DashCoroutine());
                break;
            case ActionStates.Attack:
                StartCoroutine(AttackCoroutine());
                break;
        }
    }

    IEnumerator DashCoroutine()
    {
        rend.material.color = Color.clear;
        
        yield return new WaitForSeconds(0.5f);
        actionState = ActionStates.Idle;
        rend.material.color = Color.white;
    }

    IEnumerator AttackCoroutine()
    {
        yield return new WaitForSeconds(0.5f);
        actionState = ActionStates.Idle;
    }
    
    IEnumerator ChangeColor(Color tint){
        // color values are R, G, B, and alpha, each 0-255 divided by 100
        rend.material.color = tint;
        yield return new WaitForSeconds(0.5f);
        rend.material.color = Color.white;
    }
}
