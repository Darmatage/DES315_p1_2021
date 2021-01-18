using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Amogh
{
    public class BridgeConstructor : MonoBehaviour
    {
        public GameObject bridgePrefab;

        public Transform lastBridge;

        private Vector3 mousePos;

        private GameObject currentBridge;

        private Camera cam;
        // Start is called before the first frame update
        void Start()
        {
            cam = Camera.main;
            Debug.Assert(cam != null, "No camera found");
            
            currentBridge = Instantiate(bridgePrefab, Vector3.zero, Quaternion.identity);
        }

        private void PlaceBridge(Vector3 position)
        {
            lastBridge = currentBridge.transform;
            currentBridge = Instantiate(bridgePrefab, position, Quaternion.identity);
        }

        private void MoveCurrentBridge()
        {
            Vector3 lastBridgePos = lastBridge.transform.position;
            Vector3 currPos = lastBridgePos;
            Vector3 scale = bridgePrefab.transform.localScale;

            float angle = Vector2.SignedAngle(lastBridge.position, mousePos);
            //Debug.Log(angle);
            
            if (angle < 45 && angle > 0)
            {
                // To the right
                currPos.x += scale.x;
            }
            else if (angle > 45)
            {
                // To the top
                currPos.y += scale.y;
            }

            if (angle < -45)
            {
                // To the left
                currPos.x -= scale.x;
            }
            else if (angle < 0)
            {
                // To the bottom
                currPos.y -= scale.y;
            }

            currentBridge.transform.position = currPos;
        }
        
        // Update is called once per frame
        void Update()
        {
            mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            
            MoveCurrentBridge();
            if (Input.GetMouseButtonDown(0))
            {
                PlaceBridge(mousePos);
            }
        }
    }
}

