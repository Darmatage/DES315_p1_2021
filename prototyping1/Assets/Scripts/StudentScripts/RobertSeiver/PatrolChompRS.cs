using UnityEngine;

public class PatrolChompRS : MonoBehaviour
{
    public GameObject CrunchHandlerPrefab;
    public GameObject PlayerStunPrefab;
    
    public PatrolRS PatrolScript;
    private GameHandler gameHandlerScript;
    public bool Chomped { get; set; } = false;

    // Start is called before the first frame update
    void Start()
    {
        gameHandlerScript = GameObject.FindGameObjectWithTag("GameHandler").GetComponent<GameHandler>();
    }
    
    // Chomp the player
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!Chomped && other.transform.CompareTag("Player"))
        {
            Chomped = true;

            if (other.transform.position.x > transform.position.x)
            {
                PatrolScript.skullSprite.flipX = true;
                PatrolScript.shadowSprite.flipX = true;
            }
            else
            {
                PatrolScript.skullSprite.flipX = false;
                PatrolScript.shadowSprite.flipX = false;
            }

            // Play sound effect
            Instantiate(CrunchHandlerPrefab, transform.position, Quaternion.identity);
            
            // Stun player
            Instantiate(PlayerStunPrefab);
            
            // Play animation
            PatrolScript.Anim.Play("monsterSkull_attack");
            
            // Damage player
            gameHandlerScript.TakeDamage(PatrolScript.ChompDamage);
            
            // Destroy object
            Destroy(transform.parent.gameObject, 0.5f);
        }
    }
}
