using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water_CEGP : MonoBehaviour
{
    public Sprite watersprite;
    public Sprite icesprite;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Block_CEGP")
        {
            this.GetComponent<SpriteRenderer>().sprite = icesprite;
            this.GetComponent<BoxCollider2D>().enabled = false;
            collision.gameObject.SetActive(false);
        }
    }
}
