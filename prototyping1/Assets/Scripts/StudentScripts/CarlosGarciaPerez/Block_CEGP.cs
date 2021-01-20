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
    private const float speed = 0.012f;
    private Vector3 vel;

    public BoxCollider2D[] Colliders;

    public bool[] againstWall;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 4; ++i)
        {
            Colliders[i].enabled = false;
            againstWall[i] = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 4; ++i)
        {
            Colliders[i].enabled = false;
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
            Colliders[(int)cur_state].enabled = true;
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
            Vector2 player_dir = Vector2.zero;
            player_dir.x = Input.GetAxisRaw("Horizontal");
            player_dir.y = Input.GetAxisRaw("Vertical");
            player_dir.Normalize();
            if (player_dir == Vector2.up && player_pos.y < pos.y && !againstWall[(int)BLOCK_STATE.MOVE_UP])
            {
                cur_state = BLOCK_STATE.MOVE_UP;
            }
            else if (player_dir == Vector2.down && player_pos.y > pos.y && !againstWall[(int)BLOCK_STATE.MOVE_DOWN])
            {
                cur_state = BLOCK_STATE.MOVE_DOWN;
            }
            else if (player_dir == Vector2.left && player_pos.x > pos.x && !againstWall[(int)BLOCK_STATE.MOVE_LEFT])
            {
                cur_state = BLOCK_STATE.MOVE_LEFT;
            }
            else if (player_dir == Vector2.right && player_pos.x < pos.x && !againstWall[(int)BLOCK_STATE.MOVE_RIGHT])
            {
                cur_state = BLOCK_STATE.MOVE_RIGHT;
            }
        }
    }
}
