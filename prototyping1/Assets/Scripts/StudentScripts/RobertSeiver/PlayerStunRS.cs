using UnityEngine;

public class PlayerStunRS : MonoBehaviour
{
    public float StunTime = 1.0f;
    private Rigidbody2D playerRB;
    private Animator playerAnim;
    
    // Start is called before the first frame update
    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerRB = player.GetComponent<Rigidbody2D>();
        playerAnim = player.GetComponentInChildren<Animator>();
        Destroy(gameObject, StunTime);
        playerRB.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    private void Update()
    {
        playerAnim.Play("player_hurt");
    }

    private void OnDestroy()
    {
        playerRB.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
}
