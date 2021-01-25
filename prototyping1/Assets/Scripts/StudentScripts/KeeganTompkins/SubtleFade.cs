using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubtleFade : MonoBehaviour
{
    public float CycleTime = 1.0f;

    public Vector2 MinMax = new Vector2(0.0f,1.0f);

    float timer = 0.0f;

    public float InOut = 1.0f;

    // Update is called once per frame
    void Update()
    {
        float t = timer / CycleTime;

        if (t >= MinMax.y)
            InOut = -1.0f;
        else if (t <= MinMax.x)
            InOut = 1.0f;

        timer += Time.deltaTime * InOut;

        var img = GetComponent<Image>();
        {
            if (img)
            {
                //Change the alpha lerped between the min and max.
                var modColor = img.color;
                modColor.a = MinMax.x * t + (1.0f - t) * MinMax.y;
                img.color = modColor;
            }
        }


    }
}
