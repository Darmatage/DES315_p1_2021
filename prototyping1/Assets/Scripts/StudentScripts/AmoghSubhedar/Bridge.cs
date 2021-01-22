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
        private static GameObject lavaTile;
        private static Collider2D lavaTileCol;

        private bool startTimer;
        private bool triggered = false;
        
        private Vector3 initScale;

        private GameObject overlay;
        private Collider2D boxCol;
        
        void Start()
        {
            if (!lavaTile)
            {
                lavaTile = GameObject.FindWithTag("lava");
                lavaTileCol = lavaTile.GetComponents<Collider2D>()[1];
                Debug.Log(lavaTileCol);
            }
            
            timer = 0f;
            initScale = transform.localScale;
        }

        public void BridgePlaced()
        {
            overlay = transform.GetChild(0).gameObject;
            boxCol = GetComponent<BoxCollider2D>();
            sprite = GetComponent<SpriteRenderer>();

            boxCol.enabled = true;
            overlay.SetActive(false);
            sprite.color = Color.white;
            
            this.enabled = true;
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
                    //Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Default"), LayerMask.NameToLayer("Lava"));
                    Physics2D.IgnoreCollision(other, lavaTileCol, true);
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
                    //Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Default"), LayerMask.NameToLayer("Lava"), false);
                    Physics2D.IgnoreCollision(other, lavaTileCol, false);
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
                    //Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Default"), LayerMask.NameToLayer("Lava"), false);
                    Physics2D.IgnoreCollision(GameObject.FindWithTag("Player").GetComponent<Collider2D>(), lavaTileCol, false);
                }
            }
        }
    }
}