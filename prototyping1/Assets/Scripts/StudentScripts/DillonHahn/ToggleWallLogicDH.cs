using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleWallLogicDH : MonoBehaviour
{
  public int channel = 0;
  public bool startOn = true;
  bool toggledOn = true;
  public Sprite onSprite;
  public Sprite offSprite;
  public bool snapToNearestGridSpace = true;

  // Start is called before the first frame update
  void Start()
  {
    GameObject[] buttonArr = GameObject.FindGameObjectsWithTag("togglebutton");

    foreach (GameObject button in buttonArr)
    {
      if (button.GetComponent<ToggleButtonLogicDH>().channel == channel)
      {
        button.GetComponent<ToggleButtonLogicDH>().wallList.Add(this);
      }
    }

    if (!startOn)
      ToggleState();

    if(snapToNearestGridSpace)
    {
      float newX = Mathf.Round(transform.position.x - 0.5f) + 0.5f;
      float newY = Mathf.Round(transform.position.y - 0.5f) + 0.5f;

      transform.position = new Vector3(newX, newY, transform.position.z);
    }
  }

  // Update is called once per frame
  void Update()
  {

  }

  public void ToggleState()
  {
    toggledOn = !toggledOn;
    if (toggledOn)
    {
      GetComponent<SpriteRenderer>().sprite = onSprite;
      GetComponent<BoxCollider2D>().isTrigger = false;
    }
    else
    {
      GetComponent<SpriteRenderer>().sprite = offSprite;
      GetComponent<BoxCollider2D>().isTrigger = true;
    }
  }

  public void SetColor(Color col)
  {
    GetComponent<SpriteRenderer>().color = col;
  }  
}
