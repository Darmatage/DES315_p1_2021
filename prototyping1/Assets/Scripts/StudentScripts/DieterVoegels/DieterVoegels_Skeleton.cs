using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DieterVoegels_Skeleton : MonoBehaviour
{
  public GameObject playerReference = null;
  public float speed = 0;
  public float timer = 0;

  // Start is called before the first frame update
  void Start()
  {
    
  }

  // Update is called once per frame
  void Update()
  {
    Vector3 directionalVector = playerReference.GetComponent<Transform>().position - GetComponent<Transform>().position;
    directionalVector = directionalVector.normalized * speed * Time.deltaTime;
    GetComponent<Transform>().position += directionalVector;

    timer -= Time.deltaTime;
    if (timer <= 0)
    {
      Destroy(gameObject);
    }
  }

  private void OnCollisionEnter2D(Collision2D collision)
  {
    SceneManager.LoadScene("EndLose");
  }
}
