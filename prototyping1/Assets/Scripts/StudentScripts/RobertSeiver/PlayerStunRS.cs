using UnityEngine;

public class PlayerStunRS : MonoBehaviour
{
    public float StunTime = 1.0f;
    private Rigidbody2D playerRB;
    private Animator playerAnim;
    private PlayerMoveRS movementScript;
    
    // Start is called before the first frame update
    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerRB = player.GetComponent<Rigidbody2D>();
        playerAnim = player.GetComponentInChildren<Animator>();
        movementScript = player.GetComponent<PlayerMoveRS>();
        Destroy(gameObject, StunTime);
        playerRB.constraints = RigidbodyConstraints2D.FreezeAll;
        movementScript.CanWalk = false;
    }

    private void Update()
    {
        playerAnim.Play("player_hurt");
    }

    private void OnDestroy()
    {
        playerRB.constraints = RigidbodyConstraints2D.FreezeRotation;
        movementScript.CanWalk = true;
    }
}
