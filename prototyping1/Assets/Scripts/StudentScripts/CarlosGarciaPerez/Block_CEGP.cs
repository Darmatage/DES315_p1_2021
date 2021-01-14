using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block_CEGP : MonoBehaviour
{
    public enum BLOCK_STATE
    {
        MOVE_LEFT,
        MOVE_RIGHT,
        MOVE_UP,
        MOVE_DOWN,
        IDLE
    }

    public BLOCK_STATE cur_state = BLOCK_STATE.IDLE;
    public BLOCK_STATE prev_state = BLOCK_STATE.IDLE;
    private const float speed = 0.005f;
    private Vector3 vel;

    public GameObject[] Colliders;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 4; ++i)
        {
            Colliders[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 4; ++i)
        {
            Colliders[i].SetActive(false);
        }
        switch (cur_state)
        {
            case BLOCK_STATE.MOVE_DOWN:
                vel = Vector3.down;
                break;
            case BLOCK_STATE.MOVE_LEFT:
                vel = Vector3.left;
                break;
            case BLOCK_STATE.MOVE_RIGHT:
                vel = Vector3.right;
                break;
            case BLOCK_STATE.MOVE_UP:
                vel = Vector3.up;
                break;
            default:
                vel = Vector3.zero;
                break;
        }
        vel *= speed;

        if (cur_state != BLOCK_STATE.IDLE)
        {
            Colliders[(int)cur_state].SetActive(true);
        }

        //move
        transform.position += vel;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // hit player?
        if (other.gameObject.tag == "Player" && cur_state == BLOCK_STATE.IDLE)
        {
            Vector2 player_pos = new Vector2(other.gameObject.transform.position.x, other.gameObject.transform.position.y);
            Vector2 pos = new Vector2(transform.position.x, transform.position.y);
            float angle = Vector2.SignedAngle(Vector2.right, player_pos - pos);
            if (angle <= -140.0f && prev_state != BLOCK_STATE.MOVE_RIGHT)
            {
                cur_state = BLOCK_STATE.MOVE_RIGHT;
            }
            else if (angle >= -130.0f && angle <= -50.0f && prev_state != BLOCK_STATE.MOVE_UP)
            {
                cur_state = BLOCK_STATE.MOVE_UP;
            }
            else if (angle >= -40.0f && angle <= 40.0f && prev_state != BLOCK_STATE.MOVE_LEFT)
            {
                cur_state = BLOCK_STATE.MOVE_LEFT;
            }
            else if (angle >= 50.0f && angle <= 130.0f && prev_state != BLOCK_STATE.MOVE_DOWN)
            {
                cur_state = BLOCK_STATE.MOVE_DOWN;
            }
            else if (angle >= 140.0f && prev_state != BLOCK_STATE.MOVE_RIGHT)
            {
                cur_state = BLOCK_STATE.MOVE_RIGHT;
            }
        }
        /*
        // hit wall?
        if (other.gameObject.tag == "Border_CEGP")
        {
            //if (IntoWall(other.gameObject.transform.position))
            // stop movement in this direction
            cur_state = BLOCK_STATE.IDLE;
        }*/
    }

    private bool IntoWall(Vector3 wall_pos)
    {
        Vector3 pos = transform.position;
        switch(cur_state)
        {
            case BLOCK_STATE.IDLE:
                break;
            case BLOCK_STATE.MOVE_DOWN:
                if (wall_pos.x == pos.x && wall_pos.y < pos.y) return true;
                break;
            case BLOCK_STATE.MOVE_LEFT:
                if (wall_pos.y == pos.y && wall_pos.x < pos.x) return true;
                break;
            case BLOCK_STATE.MOVE_RIGHT:
                if (wall_pos.y == pos.y && wall_pos.x > pos.x) return true;
                break;
            case BLOCK_STATE.MOVE_UP:
                if (wall_pos.x == pos.x && wall_pos.y > pos.y) return true;
                break;
            default:
                break;
        }
        return false;
    }
}
