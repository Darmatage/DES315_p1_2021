using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Amogh
{
    [RequireComponent(typeof(Gradient), typeof(SpriteRenderer))]
    public class Bridge : MonoBehaviour
    {
        public Gradient gradient;

        public float lifetime = 3f;
        public Vector3 finalScale;
    
        private float timer;
        private SpriteRenderer sprite;

        private static int triggerCount = 0;
        
        private Vector3 initScale;
        // Start is called before the first frame update
        void Start()
        {
            timer = 0f;
            sprite = GetComponent<SpriteRenderer>();
            initScale = transform.localScale;
        }

        // Update is called once per frame
        void Update()
        {
            timer += Time.deltaTime;
        
            sprite.color = gradient.Evaluate(timer/lifetime);
        
            //transform.localScale = Vector3.Lerp(initScale, finalScale, timer/lifetime );
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            // Reference count the bridges being triggered by player
            if (other.CompareTag("Player"))
            {
                ++triggerCount;
                
                if (triggerCount == 1)
                {
                    Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Default"), LayerMask.NameToLayer("Lava"));
                }
            }
            
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            // Reference count the bridges being triggered by player
            if (other.CompareTag("Player"))
            {
                --triggerCount;
                
                if (triggerCount == 0)
                {
                    Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Default"), LayerMask.NameToLayer("Lava"), false);
                }
            }
        }
    }
}