using System;
using Controller;
using UnityEngine;

namespace Objects
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class FallingObject : MonoBehaviour
    {
        public float distanceBeforeFall = 1f;
        public bool falling = false;
        public bool fallIfHit = true;

        private new Rigidbody2D rigidbody2D;
        private float gravity;

        private void Awake()
        {
            rigidbody2D = GetComponent<Rigidbody2D>();
            gravity = rigidbody2D.gravityScale;
            rigidbody2D.gravityScale = 0.0f;
        }

        private void Update()
        {
            if (falling) return;

            if (!(this.transform.position.y - RisingUpController.Instance.transform.position.y <
                  distanceBeforeFall)) return;
            InitiateFall();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                InitiateFall();
            }
        }

        private void InitiateFall()
        {
            falling = true;
            rigidbody2D.gravityScale = gravity;
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(this.transform.position, new Vector3(10, distanceBeforeFall * 2));
            //Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - distanceBeforeFall, transform.position.z));
        }
    }
}