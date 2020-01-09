﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using Utility;

namespace Controller
{
    public class RisingUpController : Singleton<RisingUpController>
    {
        [Space] [Header("General")] 
        public float riseSpeed = 0.5f;
        public GameObject shield;
        
        // TODO: testing, remove later
        public bool activateShield = false;
        
        [Space] [Header("Relays")] 
        public ColliderRelay bodyRelay;
        public ColliderRelay closeByRelay;

        [Space] [Header("Events")] 
        public UnityEvent closeByEvent = new UnityEvent();
        public UnityEvent hitEvent = new UnityEvent();
        
        private readonly List<float> modifier = new List<float>();
        private Coroutine currentRoutine;
        private bool hit;
        private bool risingUp;
        
        // Prevent non-singleton constructor use.
        protected RisingUpController()
        {
        }

        private void Start()
        {
            if (closeByRelay != null) closeByRelay.colliderRelayEvent.AddListener(OnCloseByColliderHit);
            if (bodyRelay != null) bodyRelay.colliderRelayEvent.AddListener(OnBodyColliderHit);
        }

        private void Update()
        {
            if (!risingUp) return;
            
            // TODO: remove, testing purpose
            shield.SetActive(activateShield);

            float multiplier = 1;

            if (modifier.Count > 0) multiplier = modifier.Aggregate((a, f) => a * f);

            transform.position += Vector3.up * (riseSpeed * multiplier * Time.deltaTime);
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