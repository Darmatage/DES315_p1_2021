using System;
using System.Collections;
using System.Collections.Generic;
using StudentScripts.MarkCulp;
using UnityEngine;

public class TeleportAbility : MonoBehaviour
{
    [SerializeField] private KeyCode teleportKey = KeyCode.T;
    
    [SerializeField] private TeleportAbilityMarker markerPrefab;
    private TeleportAbilityMarker markerInstance;

    private Camera camera;
    
    private void Awake()
    {
        camera = FindObjectOfType<Camera>();
        
        if (!markerPrefab)
        {
            Debug.LogError("Missing marker prefab! Disabling...");
            enabled = false;
        }
    }
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (markerInstance)
                Destroy(markerInstance.gameObject);

            var spawnPos = camera.ScreenToWorldPoint(Input.mousePosition);
            spawnPos.z = transform.position.z;
            
            markerInstance = Instantiate(markerPrefab, spawnPos, Quaternion.identity);
        }

        if (Input.GetKeyDown(teleportKey))
        {
            if (markerInstance)
            {
                StartCoroutine(Teleport());
            }
        }
    }

    // Implemented as a coroutine for future expandability-- triggering VFX, etc.
    private IEnumerator Teleport()
    {
        transform.position = markerInstance.transform.position;
        Destroy(markerInstance.gameObject);
        markerInstance = null;
        
        yield break;
    }
}
