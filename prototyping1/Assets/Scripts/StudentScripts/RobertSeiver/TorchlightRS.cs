using UnityEngine;

public class TorchlightRS : MonoBehaviour
{
    public Vector3 SmallAura;
    public Vector3 LargeAura;

    public float FirstAlpha;
    public float SecondAlpha;

    public int SizePeriod;
    public int AlphaPeriod;
    
    private SpriteRenderer aura;

    private float seed;
    
    // Start is called before the first frame update
    void Start()
    {
        aura = GetComponent<SpriteRenderer>();

        seed = Random.value * 1000.0f;

        SmallAura *= Random.Range(0.9f, 1.1f);
        LargeAura *= Random.Range(0.9f, 1.1f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = Vector3.Lerp(SmallAura, LargeAura, GetInterpolant(SizePeriod));
        
        Color color = aura.color;
        color.a = Mathf.Lerp(FirstAlpha, SecondAlpha, GetInterpolant(AlphaPeriod));
        aura.color = color;
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
