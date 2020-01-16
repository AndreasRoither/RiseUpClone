using UnityEngine;
using Utility;

namespace Level
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class TutorialEnd : MonoBehaviour
    {
        public LevelLoader loader;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player")) return;

            loader.LoadLevel(0);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
           
        }
    }
}