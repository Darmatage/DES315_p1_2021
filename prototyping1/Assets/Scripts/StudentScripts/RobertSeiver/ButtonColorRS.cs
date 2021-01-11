using UnityEngine;
using UnityEngine.UI;

public class ButtonColorRS : MonoBehaviour
{
    [Range(0.1f, 10.0f)] public float ColorLerpSpeed = 10.0f;
    private Button button;
    
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
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
    }
}
