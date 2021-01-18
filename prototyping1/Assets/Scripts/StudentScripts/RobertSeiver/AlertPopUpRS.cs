using UnityEngine;

public class AlertPopUpRS : MonoBehaviour
{
    [Range(0.1f, 5.0f)] public float Lifetime = 1.0f;
    [Range(0.1f, 1.0f)] public float ColorTimer = 0.5f;
    private float colorTimer; 
    
    private SpriteRenderer sprite;

    private bool firstColor = true;
    
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, Lifetime);

        sprite = GetComponentInChildren<SpriteRenderer>();
        colorTimer = ColorTimer;
    }

    // Update is called once per frame
    void Update()
    {
        colorTimer -= Time.deltaTime;
        if (colorTimer < 0.0f)
        {
            colorTimer = ColorTimer;

            firstColor = !firstColor;
            if (firstColor)
                sprite.color = Color.yellow;
            else
                sprite.color = Color.white;
        }
    }
}
