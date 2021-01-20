using UnityEngine;

namespace RyanHeath
{
    public class RyanHeathBtn : MonoBehaviour
    {
        private RectTransform rectTransform;
        private Vector2 baseSize;
        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            baseSize = rectTransform.sizeDelta;
        }

        void Update()
        {
            rectTransform.sizeDelta = baseSize + new Vector2(Mathf.Sin(Time.time * 20) * 3, Mathf.Cos(Time.time * 10) * 3);
        }
    }
}
