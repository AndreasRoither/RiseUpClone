using System;
using System.Collections;
using Controller;
using UnityEngine;

namespace Objects
{
    public class ObjectLauncher : MonoBehaviour
    {
        #region Fields

        public int objectAmount = 0;
        public float timeBetweenObjects = 0.5f;
        public float detectionRange = 5f;
        public float thrust = 1f;
        public float angle = 45f;
        public int maxRetries = 0;

        private bool shooting = false;
        private int retryCount = 0;
        private Coroutine currentRoutine = null;

        // remove; testing purposes
        public GameObject objectToSpawn;

        #endregion

        #region LifeCycle

        private void Update()
        {
            if (shooting) return;
            if (retryCount > maxRetries) return;

            retryCount++;
            if (!(this.transform.position.y - RisingUpController.Instance.transform.position.y <
                  detectionRange)) return;
            StartLaunch();
        }

        #endregion

        #region Functions

        public void StartLaunch()
        {
            shooting = true;


            if (currentRoutine != null)
            {
                StopCoroutine(currentRoutine);
            }

            currentRoutine = StartCoroutine(LaunchRoutine());
        }

        private IEnumerator LaunchRoutine()
        {
            for (int i = 0; i < objectAmount; ++i)
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

            GameObject obj = Instantiate(objectToSpawn);
            obj.transform.position = transform.position;
            Rigidbody2D r2d = obj.GetComponent<Rigidbody2D>();
            if (r2d != null)
            {
                // angle starts on the right, then counter clockwise
                Vector3 direction = Quaternion.AngleAxis(angle, Vector3.forward) * Vector3.right;
                r2d.AddForce(direction * thrust);
            }
        }

        #endregion

        #region Gizmos

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(this.transform.position, new Vector3(10, detectionRange * 2));
        }

        #endregion
    }
}