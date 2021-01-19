using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Amogh
{
    public class BridgeConstructor : MonoBehaviour
    {
        public GameObject bridgePrefab;

        private GameObject lastBridge;

        private Vector3 mousePos;

        private GameObject currentBridge;

        private Camera cam;

        // Start is called before the first frame update
        void Start()
        {
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

            lastBridge.GetComponent<Bridge>().enabled = true;
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
        
        // Update is called once per frame
        void Update()
        {
            mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            
            if (currentBridge)
                MoveCurrentBridge();
            
            if (Input.GetMouseButtonDown(0))
            {
                PlaceBridge(mousePos);
            }
            else if (Input.GetMouseButtonDown(1))
            {
                Destroy(currentBridge);
            }
        }
    }
}

