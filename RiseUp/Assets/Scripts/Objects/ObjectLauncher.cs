using System.Collections;
using Controller;
using UnityEngine;

namespace Objects
{
    public class ObjectLauncher : MonoBehaviour
    {
        public float detectionRange = 5f;
        public float angle = 45f;
        public float thrust = 1f;
        public float timeBetweenObjects = 0.5f;
        public int maxRetries;
        public int objectAmount;
        
        // remove; testing purposes
        public GameObject objectToSpawn;

        private int retryCount;
        private Coroutine currentRoutine;
        private bool shooting;

        private void Update()
        {
            if (shooting) return;
            if (retryCount > maxRetries) return;
            if (!(transform.position.y - RisingUpController.Instance.transform.position.y <
                  detectionRange)) return;
            
            retryCount++;
            StartLaunch();
        }


        public void StartLaunch()
        {
            shooting = true;
            if (currentRoutine != null) StopCoroutine(currentRoutine);
            currentRoutine = StartCoroutine(LaunchRoutine());
        }

        private IEnumerator LaunchRoutine()
        {
            for (var i = 0; i < objectAmount; ++i)
            {
                Launch();
                yield return new WaitForSeconds(timeBetweenObjects);
            }

            shooting = false;
        }

        private void Launch()
        {
            // Spawn object
            // add force

            var obj = Instantiate(objectToSpawn);
            obj.transform.position = transform.position;
            var r2d = obj.GetComponent<Rigidbody2D>();
            if (r2d != null)
            {
                // angle starts on the right, then counter clockwise
                var direction = Quaternion.AngleAxis(angle, Vector3.forward) * Vector3.right;
                r2d.AddForce(direction * thrust);
            }
        }

        #region Gizmos

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position, new Vector3(10, detectionRange * 2));
        }

        #endregion
    }
}