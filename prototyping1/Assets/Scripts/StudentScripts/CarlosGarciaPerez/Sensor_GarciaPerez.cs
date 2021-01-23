using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensor_GarciaPerez : MonoBehaviour
{
    public Block_CEGP script;

    public Block_CEGP.BLOCK_STATE dir;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Border_CEGP" || collision.gameObject.tag == "Block_CEGP")
        {
            script.againstWall[(int)dir] = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Border_CEGP" || collision.gameObject.tag == "Block_CEGP")
        {
            script.againstWall[(int)dir] = false;
        }
    }
}
