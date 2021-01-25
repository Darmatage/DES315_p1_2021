using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Amogh
{
    public class BridgeConstructorSlow : MonoBehaviour
    {
        [Header("Use this script for placing bridges one-click at a time")]
        
        public GameObject bridgePrefab;
        public Grid grid;
        
        [Range(1, 5)]
        public int placementRange = 2;
        
        [Tooltip("These are mouse buttons")]
        public MouseButtons inputBtn = MouseButtons.LeftBtn;
        private int mouseBtn;
        
        private GameObject lastBridge;
        private GameObject currentBridge;

        private Vector3 mousePos;
        private Camera cam;
        
        void Start()
        {
            mouseBtn = (int) inputBtn;
            
            cam = Camera.main;
            Debug.Assert(cam != null, "No camera found");
        }

        private void PlaceBridge(Vector3 position)
        {
            position.z = 0;
            
            if (lastBridge)
            {
                var lastBridgePos = grid.WorldToCell(lastBridge.transform.position);
                float distance = Vector3Int.Distance(lastBridgePos, grid.WorldToCell(position));
                
                // New bridge is too far to be attached to the last bridge that was placed
                if (distance > placementRange)
                    lastBridge = null;
            }

            currentBridge = Instantiate(bridgePrefab, position, Quaternion.identity);
            
        }

        private void MoveCurrentBridge()
        {
            // No last placed bridge, so just move current bridge to the mouse position
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
        
        void Update()
        {
            mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            
            if (Input.GetMouseButton(mouseBtn))
            { 
                if (currentBridge) 
                    MoveCurrentBridge();
            }
            
            if (Input.GetMouseButtonDown(mouseBtn))
            {
                PlaceBridge(mousePos);
            }

            if (Input.GetMouseButtonUp(mouseBtn))
            {
                    currentBridge.GetComponent<Bridge>().BridgePlaced();
                    lastBridge = currentBridge;
                    currentBridge = null;
            }
        }
    }
}

