using UnityEngine;

public class ForceOneUseSwitchRS : MonoBehaviour
{
    private Collider2D c;
    
    // Start is called before the first frame update
    void Start()
    {
        c = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
            c.enabled = false;
    }
}
