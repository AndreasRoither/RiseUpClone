using Controller;
using UnityEngine;

namespace Objects
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class FallingObject : MonoBehaviour
    {
        #region Fields

        public float detectionRange = 1f;
        public bool fallIfHit = true;

        private bool falling = false;
        private new Rigidbody2D rigidbody2D;
        private float initialGravity;

        #endregion

        #region Lifecycle

        private void Awake()
        {
            rigidbody2D = GetComponent<Rigidbody2D>();
            initialGravity = rigidbody2D.gravityScale;
            rigidbody2D.gravityScale = 0.0f;
        }

        private void Update()
        {
            if (falling) return;

            if (!(this.transform.position.y - RisingUpController.Instance.transform.position.y <
                  detectionRange)) return;
            InitiateFall();
        }

        #endregion

        #region Functions

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
            rigidbody2D.gravityScale = initialGravity;
        }

        #endregion

        #region Gizmos

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            //Gizmos.DrawWireCube(this.transform.position, new Vector3(10, detectionRange * 2));
            Gizmos.DrawLine(transform.position,
                new Vector3(transform.position.x, transform.position.y - detectionRange, transform.position.z));
        }

        #endregion
    }
}