using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using Utility;

namespace Controller
{
    public class RisingUpController : Singleton<RisingUpController>
    {
        #region Fields

        public float riseSpeed = 0.5f;
        public bool risingUp = false;
        public UnityEvent hitEvent = new UnityEvent();
        public bool hit = false;
        private readonly List<float> modifier = new List<float>();
        private Coroutine currentRoutine = null;

        #endregion

        #region Lifecycle

        // Prevent non-singleton constructor use.
        protected RisingUpController()
        {
        }

        private void Update()
        {
            if (!risingUp) return;
            float multiplier = 1;

            if (modifier.Count > 0)
            {
                multiplier = modifier.Aggregate((a, f) => a * f);
            }

            transform.position += Vector3.up * (riseSpeed * multiplier * Time.deltaTime);
        }

        #endregion

        #region Methods

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player")) return;
            if (hit) return;
            if (currentRoutine != null) StopCoroutine(currentRoutine);
            currentRoutine = null;
            risingUp = false;
            hit = true;
            hitEvent.Invoke();
        }

        public float GetHeight()
        {
            return transform.position.y;
        }

        public void AddModifier(float f)
        {
            this.modifier.Add(f);
        }

        public void ClearModifier()
        {
            modifier.Clear();
        }

        public void StartRise(float delayDuration)
        {
            currentRoutine = StartCoroutine(RiseUpToggle(delayDuration, true));
        }

        public void StopRise(float delayDuration)
        {
            currentRoutine = StartCoroutine(RiseUpToggle(delayDuration, false));
        }

        public void Reset()
        {
            risingUp = false;
            hit = false;
            modifier.Clear();
        }

        private IEnumerator RiseUpToggle(float delayDuration, bool toggle)
        {
            yield return new WaitForSeconds(delayDuration);
            risingUp = toggle;
        }

        #endregion
    }
}