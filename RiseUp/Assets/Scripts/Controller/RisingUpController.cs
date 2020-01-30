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
        [Space] [Header("General")] public GameObject shield;
        public bool activeShield;

        [Space] [Header("Movement")] public float riseSpeed = 0.5f;
        public bool curveMovement = false;
        public AnimationCurve yMovementCurve;
        public AnimationCurve xMovementCurve;

        [Space] [Header("Relays")] public ColliderRelay bodyRelay;
        public ColliderRelay closeByRelay;

        [Space] [Header("Events")] public UnityEvent closeByEvent = new UnityEvent();
        public UnityEvent hitEvent = new UnityEvent();

        private Rigidbody2D body;
        private readonly List<float> modifier = new List<float>();
        private Coroutine currentRoutine;
        private bool hit;
        private bool risingUp;

        // Prevent non-singleton constructor use.
        private RisingUpController() { }

        private void Start()
        {
            if (closeByRelay != null) closeByRelay.colliderRelayEvent.AddListener(OnCloseByColliderHit);
            if (bodyRelay != null) bodyRelay.colliderRelayEvent.AddListener(OnBodyColliderHit);
            body = GetComponent<Rigidbody2D>();
            
            if (shield != null)
                shield.SetActive(activeShield);
        }

        private void FixedUpdate()
        {
            if (!risingUp) return;

            // Option with speed multiplier
            float multiplier = 1;
            if (modifier.Count > 0) multiplier = modifier.Aggregate((a, f) => a * f);
            transform.position += Vector3.up * (riseSpeed * multiplier * Time.deltaTime);

            // New possible way with movement according to a curve
            /*
            timeElapsed += Time.deltaTime;
            if (timeElapsed >= 1) timeElapsed = 0;
            
            var yModifier = yMovementCurve.Evaluate (timeElapsed);
            var yMovement = Vector3.up * (riseSpeed * yModifier);
            
            var xyModifier = xMovementCurve.Evaluate (timeElapsed);
            var xMovement = Vector3.left * (riseSpeed * yModifier);
            */
        }

        private void OnCloseByColliderHit(Collider2D other)
        {
            closeByEvent?.Invoke();
        }

        private void OnBodyColliderHit(Collider2D other)
        {
            if (hit) return;
            if (currentRoutine != null) StopCoroutine(currentRoutine);
            currentRoutine = null;
            risingUp = false;
            hit = true;
            hitEvent?.Invoke();
        }

        public float GetHeight()
        {
            return transform.position.y;
        }

        public void AddModifier(float f)
        {
            modifier.Add(f);
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
    }
}