using UnityEngine;
using Random = UnityEngine.Random;

public class HealthPickupRS : MonoBehaviour
{
    public int HealingAmount;
    private GameHandler gh;
    private float seed;

    public float MinHeartScale = 0.75f;
    public float MaxHeartScale = 1.25f;
    private Vector3 initialScale;
    
    // Start is called before the first frame update
    private void Start()
    {
        gh = GameObject.FindGameObjectWithTag("GameHandler").GetComponent<GameHandler>();
        seed = Random.value * 1000.0f;

        initialScale = transform.localScale;
    }

    private void Update()
    {
        transform.localScale = Vector3.Lerp(MinHeartScale * initialScale, MaxHeartScale * initialScale, GetInterpolant(5));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameHandler.PlayerHealth = Mathf.Clamp(GameHandler.PlayerHealth + HealingAmount, 0, gh.PlayerHealthStart);
            gh.UpdateHealth();
            GetComponentInChildren<SpriteRenderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;
        }
    }
    
    private float GetInterpolant(int n)
    {
        float interpolant = 0.0f;
        for (int i = 1; i <= n; i++)
            interpolant += Sin01(Time.time * i);
        interpolant /= n;

        return Mathf.Clamp01(interpolant);
    }

    private float Sin01(float x)
    {
        return 0.5f * (1.0f + Mathf.Sin(seed + x));
    }
}
