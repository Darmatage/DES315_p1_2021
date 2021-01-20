using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushBlock_CEGP : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /*
    private void OnTriggerEnter2D(Collider2D other)
    {
        // hit player?
        if (other.gameObject.tag == "Player" && cur_state == BLOCK_STATE.IDLE)
        {
            Vector2 player_pos = new Vector2(other.gameObject.transform.position.x, other.gameObject.transform.position.y);
            Vector2 pos = new Vector2(transform.position.x, transform.position.y);
            float angle = Vector2.SignedAngle(Vector2.right, player_pos - pos);
            if (angle < -135.0f)
            {
                cur_state = BLOCK_STATE.MOVE_RIGHT;
            }
            else if (angle < -45.0f)
            {
                cur_state = BLOCK_STATE.MOVE_UP;
            }
            else if (angle < 45.0f)
            {
                cur_state = BLOCK_STATE.MOVE_LEFT;
            }
            else if (angle < 135.0f)
            {
                cur_state = BLOCK_STATE.MOVE_DOWN;
            }
            else
            {
                cur_state = BLOCK_STATE.MOVE_RIGHT;
            }
        }
    }*/
}
