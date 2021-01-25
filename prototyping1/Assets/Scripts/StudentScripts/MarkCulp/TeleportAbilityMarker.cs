using System;
using UnityEngine;

namespace StudentScripts.MarkCulp
{
    public class TeleportAbilityMarker : MonoBehaviour
    {

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag.Equals("Walls"))
                Destroy(gameObject);
        }
    }
}