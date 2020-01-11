using UnityEngine;

namespace Utility
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class DeactivationZone : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Player")) return;
            other.gameObject.SetActive(false);
            Destroy(other.gameObject);
        }
    }
}