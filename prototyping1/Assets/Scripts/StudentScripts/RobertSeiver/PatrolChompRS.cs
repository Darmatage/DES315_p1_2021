using UnityEngine;

public class PatrolChompRS : MonoBehaviour
{
    public GameObject CrunchHandlerPrefab;
    public GameObject PlayerStunPrefab;
    
    public PatrolRS PatrolScript;
    private GameHandler gameHandlerScript;
    private bool chomped = false;

    // Start is called before the first frame update
    void Start()
    {
        gameHandlerScript = GameObject.FindGameObjectWithTag("GameHandler").GetComponent<GameHandler>();
    }
    
    // Chomp the player
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!chomped && other.transform.CompareTag("Player"))
        {
            chomped = true;
            
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
