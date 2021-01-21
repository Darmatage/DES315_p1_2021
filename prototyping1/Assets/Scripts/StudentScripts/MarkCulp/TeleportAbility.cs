using System;
using System.Collections;
using System.Collections.Generic;
using StudentScripts.MarkCulp;
using UnityEngine;

public class TeleportAbility : MonoBehaviour
{
    [SerializeField] private KeyCode placeMarkerKey = KeyCode.M;
    [SerializeField] private KeyCode teleportKey = KeyCode.T;
    
    [SerializeField] private TeleportAbilityMarker markerPrefab;
    private TeleportAbilityMarker markerInstance;
    
    private void Awake()
    {
        if (!markerPrefab)
        {
            Debug.LogError("Missing marker prefab! Disabling...");
            enabled = false;
        }
    }
    
    void Update()
    {
        if (Input.GetKeyDown(placeMarkerKey))
        {
            if (markerInstance)
                Destroy(markerInstance.gameObject);
            
            markerInstance = Instantiate(markerPrefab, transform.position + new Vector3(0, 0, -0.1f), Quaternion.identity);
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
