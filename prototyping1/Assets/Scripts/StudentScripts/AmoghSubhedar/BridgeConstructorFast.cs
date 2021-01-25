using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Amogh
{
    public class BridgeConstructorFast : MonoBehaviour
    {
        [Header("Use this script for placing bridges in high-speed situations")]
        
        public GameObject bridgePrefab;
        
        [Tooltip("How long can the player keep placing bridges")]
        [Range(1f, 60f)]
        public float timeLimit = 1f;
        
        [Tooltip("How long does the player have to wait before being able to place bridges again")]
        [Range(0f, 10f)]
        public float CooldownTime = 0.1f;

        [Tooltip("These are mouse buttons")]
        public MouseButtons inputBtn = MouseButtons.LeftBtn;
        private int mouseBtn;
        
        private float timer;
        private GameObject lastBridge;
        private GameObject currentBridge;

        private Vector3 mousePos;
        private Camera cam;

        private bool cooldown = false;
        void Start()
        {
            mouseBtn = (int) inputBtn;
            timer = 0f;
            cam = Camera.main;
            Debug.Assert(cam != null, "No camera found");
        }

        private void PlaceBridge(Vector3 position)
        {
            position.z = 0;

            if (currentBridge)
            { 
                lastBridge = currentBridge;
                currentBridge = Instantiate(bridgePrefab, position, Quaternion.identity);
            }
            else
            { 
                currentBridge = Instantiate(bridgePrefab, position, Quaternion.identity); 
                PlaceBridge(position);
            }
            
            lastBridge.GetComponent<Bridge>().BridgePlaced();
        }

        private void MoveCurrentBridge()
        {
            if (lastBridge == null)
            {
                currentBridge.transform.position = new Vector3(mousePos.x, mousePos.y, 0f);
                return;
            }
            
            Vector3 lastBridgePos = lastBridge.transform.position;
            Vector3 currPos = lastBridgePos;
            Vector3 scale = bridgePrefab.transform.localScale;

            if (lastBridgePos.y < mousePos.y)
            {
                // To the left
                if (lastBridgePos.x - scale.x > mousePos.x)
                {
                    currPos.x -= scale.x;
                }
                else if (lastBridgePos.x + scale.x < mousePos.x)
                {
                    // To the right
                    currPos.x += scale.x;
                }
                else
                {
                    // To the top
                    currPos.y += scale.y;
                }
            }
            else
            {
                // To the left
                if (lastBridgePos.x - scale.x > mousePos.x)
                {
                    currPos.x -= scale.x;
                }
                else if (lastBridgePos.x + scale.x < mousePos.x)
                {
                    // To the right
                    currPos.x += scale.x;
                }
                else
                {
                    // To the bottom
                    currPos.y -= scale.y;
                }
            }
            
            currentBridge.transform.position = currPos;
        }

        private void StopCooldown()
        {
            cooldown = false;
        }

        private void StopBridges()
        {
            timer = 0f;
            if (currentBridge)
                currentBridge.GetComponent<Bridge>().BridgePlaced();
            lastBridge = currentBridge = null;
        }
        
        void Update()
        {
            mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            
            if (!cooldown && currentBridge)
                MoveCurrentBridge();
            
            if (!cooldown && (Input.GetMouseButtonDown(mouseBtn) || Input.GetMouseButton(mouseBtn)))
            {
                PlaceBridge(mousePos);
                timer += Time.deltaTime;

                if (timer >= timeLimit)
                {
                    StopBridges();
                    cooldown = true;
                    Invoke(nameof(StopCooldown), CooldownTime);
                }
            }

            if (Input.GetMouseButtonUp(mouseBtn))
            {
                StopBridges();
            }
        }
    }

    public enum MouseButtons
    {
        LeftBtn = 0,
        RightBtn = 1
        
    }
}

