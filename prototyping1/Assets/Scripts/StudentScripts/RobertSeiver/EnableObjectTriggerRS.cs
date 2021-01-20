using UnityEngine;

public class EnableObjectTriggerRS : MonoBehaviour
{
    public GameObject Object;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Object.SetActive(true);
            GetComponent<Collider2D>().enabled = false;
        }
    }
}
