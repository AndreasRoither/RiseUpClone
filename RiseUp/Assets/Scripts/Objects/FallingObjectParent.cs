using Controller;
using UnityEngine;
using UnityEngine.Events;

namespace Objects
{
    public class FallingObjectParent : MonoBehaviour
    {
        public float detectionRange = 1f;
        public UnityEvent initiateFallEvent;
        private bool falling;

        private void Update()
        {
            // already falling
            if (falling) return;

            // check if in falling range
            if (!(transform.position.y - RisingUpController.Instance.transform.position.y <
                  detectionRange)) return;

            // object is below player
            if (transform.position.y < RisingUpController.Instance.transform.position.y) return;
            InitiateFall();
        }

        private void InitiateFall()
        {
            falling = true;
            initiateFallEvent?.Invoke();
        }

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