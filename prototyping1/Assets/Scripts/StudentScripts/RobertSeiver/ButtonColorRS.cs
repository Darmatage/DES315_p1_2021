using UnityEngine;
using UnityEngine.UI;

public class ButtonColorRS : MonoBehaviour
{
    [Range(0.1f, 10.0f)] public float ColorLerpSpeed = 10.0f;
    private Button button;

    private Vector3 originalScale;
    public float MinScale = 0.85f, MaxScale = 1.15f;
    public float SquishSpeed = 3.0f;

    private Text text;

    private string[] textStrings;
    private int currentText = 0;
    public float TextTimer = 2.0f;
    private float textSwitchTimer = 0.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();

        originalScale = transform.localScale;

        text = GetComponentInChildren<Text>();

        textStrings = new[]
        {
            "Robert S.",
            "Click Me!",
            "Robert S.",
            "Crunch!",
            "Robert S.",
            "Pick Me!",
            "Robert S.",
            "Me First!",
            "Robert S.",
            "Over Here!",
            "Robert S.",
            "This one!",
            "Robert S.",
            "Trust Me!",
        };
    }

    // Update is called once per frame
    void Update()
    {
        var colors = button.colors;
        
        Color c0 = Color.HSVToRGB(0.05f * (1.0f + Mathf.Sin(ColorLerpSpeed * Time.time)), 0.5f, 1.0f);
        Color c1 = Color.HSVToRGB(0.05f * (1.0f + Mathf.Sin(ColorLerpSpeed * Time.time)), 0.5f, 0.75f);
        Color c2 = Color.HSVToRGB(0.05f * (1.0f + Mathf.Sin(ColorLerpSpeed * Time.time)), 0.5f, 0.5f);
        colors.normalColor      = c0;
        colors.highlightedColor = c1;
        colors.pressedColor     = c2;
        colors.selectedColor    = c2;
        
        button.colors = colors;

        float currentX = Mathf.Lerp(originalScale.x * MinScale, originalScale.x * MaxScale, GetInterpolant(9));
        float currentY = Mathf.Lerp(originalScale.y * MinScale, originalScale.y * MaxScale, GetInterpolant(13));
        transform.localScale = new Vector3(currentX, currentY, 1.0f);

        textSwitchTimer += Time.deltaTime;
        if (textSwitchTimer > TextTimer)
        {
            textSwitchTimer = 0.0f;
            currentText = (currentText + 1) % textStrings.Length;
            text.text = textStrings[currentText];
        }
    }
    
    
    private float GetInterpolant(int n)
    {
        float interpolant = 0.0f;
        for (int i = 1; i <= n; i++)
            interpolant += Sin01(Time.time * i * SquishSpeed);
        interpolant /= n;

        return Mathf.Clamp01(interpolant);
    }

    private float Sin01(float x)
    {
        return 0.5f * (1.0f + Mathf.Sin(x));
    }
}
