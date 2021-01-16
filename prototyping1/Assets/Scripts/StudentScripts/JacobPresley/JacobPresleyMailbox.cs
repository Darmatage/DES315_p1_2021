using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JacobPresleyMailbox : MonoBehaviour {

  public GameObject dialogueBox;
  public Text dialogueText;
  public bool playerInRange;
  public string dialogue;

  void Update ()
  {
    if (playerInRange)
    {
      dialogueBox.SetActive(true);
      dialogueText.text = dialogue;
    }
    else
    {
      dialogueBox.SetActive(false);
      dialogueText.text = "";
    }
  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.CompareTag("Player")) 
    { 
      playerInRange = true;
    }
  }
                      
  private void OnTriggerExit2D(Collider2D other)
  {
    if (other.CompareTag("Player")) 
    {
      playerInRange = false;
      dialogueBox.SetActive(false);
    }
  }
}