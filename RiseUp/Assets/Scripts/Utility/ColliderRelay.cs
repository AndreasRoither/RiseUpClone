using System.Collections.Generic;
using UnityEngine;

namespace Utility
{
    /// <summary>
    ///     Class that relays hits to subscribers
    ///     Holds list of tags that should be ignored
    /// </summary>
    [RequireComponent(typeof(Collider2D))]
    public class ColliderRelay : MonoBehaviour
    {
        public readonly ColliderRelayEvent colliderRelayEvent = new ColliderRelayEvent();
        public List<string> ignoredTags = new List<string>();

        private Collider2D objectCollider;

        private void Awake()
        {
            objectCollider = GetComponent<Collider2D>();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (objectCollider.isTrigger) return;
            if (ignoredTags.Contains(other.gameObject.tag)) return;
            colliderRelayEvent?.Invoke(other.collider);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!objectCollider.isTrigger) return;
            if (ignoredTags.Contains(other.gameObject.tag)) return;
            colliderRelayEvent?.Invoke(other);
        }
    }
}