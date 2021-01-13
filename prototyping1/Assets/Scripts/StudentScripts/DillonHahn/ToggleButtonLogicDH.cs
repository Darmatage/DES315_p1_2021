using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleButtonLogicDH : MonoBehaviour
{
  public int channel = 0;
  public List<ToggleWallLogicDH> wallList = new List<ToggleWallLogicDH>();
  public Sprite pressedSprite;
  public Sprite unpressedSprite;
  public bool snapToNearestGridSpace = true;

  bool pressed = false;
  bool setup = true;

  // Start is called before the first frame update
  void Start()
  {
    if (snapToNearestGridSpace)
    {
      float newX = Mathf.Round(transform.position.x - 0.5f) + 0.5f;
      float newY = Mathf.Round(transform.position.y - 0.5f) + 0.5f;

      transform.position = new Vector3(newX, newY, transform.position.z);
    }

  }

  // Update is called once per frame
  void Update()
  {
    if(setup)
    {
      foreach(ToggleWallLogicDH wall in wallList)
      {
        wall.SetColor(GetComponent<SpriteRenderer>().color);
      }
      setup = false;
    }
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
    if(collision.gameObject.name == "Player")
    {
      pressed = true;
      GetComponent<SpriteRenderer>().sprite = pressedSprite;
      foreach(ToggleWallLogicDH wall in wallList)
      {
        wall.ToggleState();
      }
    }
    
  }

  private void OnTriggerExit2D(Collider2D collision)
  {
    if (collision.gameObject.name == "Player")
    {
      pressed = false;
      GetComponent<SpriteRenderer>().sprite = unpressedSprite;
    }
  }
}
