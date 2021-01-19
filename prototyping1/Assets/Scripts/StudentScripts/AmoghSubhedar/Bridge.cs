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

        [Tooltip("Start delay before gradient and scale are modified")]public float delay = 2f;
        public float lifetime = 3f;
        public Vector3 finalScale;
    
        private float timer;
        private SpriteRenderer sprite;

        private static int triggerCount = 0;

        private bool startTimer;
        private bool triggered = false;
        
        private Vector3 initScale;
        
        void Start()
        {
            timer = 0f;
            sprite = GetComponent<SpriteRenderer>();
            initScale = transform.localScale;
        }

        void Update()
        {
            if (!startTimer && delay > 0f)
            {
                delay -= Time.deltaTime;

                if (delay <= 0f)
                {
                    startTimer = true;
                    Destroy(gameObject, lifetime);
                }

                return;
            }
            
            
            timer += Time.deltaTime;
            sprite.color = gradient.Evaluate(timer/lifetime);
        
            transform.localScale = Vector3.Lerp(initScale, finalScale, timer/lifetime );
            
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            // Reference count the bridges being triggered by player
            if (other.CompareTag("Player"))
            {
                triggered = true;
                
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
                triggered = false;
                
                if (triggerCount == 0)
                {
                    Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Default"), LayerMask.NameToLayer("Lava"), false);
                }
            }
        }

        private void OnDestroy()
        {
            if (triggered)
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