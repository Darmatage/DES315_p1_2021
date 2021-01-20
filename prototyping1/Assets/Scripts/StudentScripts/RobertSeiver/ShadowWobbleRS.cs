using UnityEngine;

public class ShadowWobbleRS : MonoBehaviour
{
    private float seed;
    private SpriteRenderer sprite;

    public Vector3 MaxScale = Vector3.one * 1.25f;
    public Vector3 MinScale = Vector3.one * 1.65f;
    public float MinAlpha = 0.3f;
    public float MaxAlpha = 0.6f;

    // Start is called before the first frame update
    void Start()
    {
        seed = Random.value * 1000.0f;
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Lerp(MinScale.x, MaxScale.x, GetInterpolant(5));
        scale.y = Mathf.Lerp(MinScale.y, MaxScale.y, GetInterpolant(6));
        scale.z = 1.0f;
        transform.localScale = scale;

        Color color = sprite.color;
        color.a = Mathf.Lerp(MinAlpha, MaxAlpha, GetInterpolant(4));
        sprite.color = color;
    }
    
    private float GetInterpolant(int n)
    {
        float interpolant = 0.0f;
        for (int i = 1; i <= n; i++)
            interpolant += Sin01(Time.time * i * i);
        interpolant /= n;

        return Mathf.Clamp01(interpolant);
    }

    private float Sin01(float x)
    {
        return 0.5f * (1.0f + Mathf.Sin(seed + x));
    }
}
