using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopBlock_CEGP : MonoBehaviour
{
    public Block_CEGP script;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Border_CEGP")
        {
            script.prev_state = script.cur_state;
            script.cur_state = Block_CEGP.BLOCK_STATE.IDLE;
        }
    }
}
