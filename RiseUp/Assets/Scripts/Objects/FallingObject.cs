using Controller;
using UnityEngine;

namespace Objects
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class FallingObject : MonoBehaviour
    {
        public float detectionRange = 1f;
        public bool fallIfHit = true;

        private bool falling;
        private bool subscribedToParent;
        private float initialGravity;
        private new Rigidbody2D rigidbody2D;

        private void Awake()
        {
            rigidbody2D = GetComponent<Rigidbody2D>();
            initialGravity = rigidbody2D.gravityScale;
            rigidbody2D.gravityScale = 0.0f;

            if (transform.parent != null)
            {
                FallingObjectParent fp = transform.parent.GetComponent<FallingObjectParent>();

                if (fp != null)
                {
                    subscribedToParent = true;
                    fp.initiateFallEvent.AddListener(InitiateFall);
                }
            }
        }

        private void Update()
        {
            // already falling
            if (falling || subscribedToParent) return;

            // check if in falling range
            if (!(transform.position.y - RisingUpController.Instance.transform.position.y <
                  detectionRange)) return;

            // object is below player
            if (transform.position.y < RisingUpController.Instance.transform.position.y) return;
            InitiateFall();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Player")) InitiateFall();
        }

        private void InitiateFall()
        {
            falling = true;
            rigidbody2D.gravityScale = initialGravity;
        }

        #region Gizmos

        private void OnDrawGizmos()
        {
            if (transform.parent != null)
            {
                FallingObjectParent fp = transform.parent.GetComponent<FallingObjectParent>();
                if (fp != null) return;
            }

            Gizmos.color = Color.red;
            //Gizmos.DrawWireCube(this.transform.position, new Vector3(10, detectionRange * 2));
            Gizmos.DrawLine(transform.position,
                new Vector3(transform.position.x, transform.position.y - detectionRange, transform.position.z));
        }

        #endregion
    }
}