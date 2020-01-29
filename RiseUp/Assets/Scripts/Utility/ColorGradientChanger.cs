using UnityEngine;
using UnityEngine.Events;

namespace Utility
{
    public class ColorGradientChanger : MonoBehaviour
    {
        public Camera mainCamera;
        public Gradient gradient;
        public float duration;
        
        private float passedTime;
        private bool changing;
        
        private void Update()
        {
            if (!changing) return;
            
            float value = Mathf.Lerp(0f, 1f, passedTime);
            passedTime += Time.deltaTime / duration;
            Color color = gradient.Evaluate(value);
            mainCamera.backgroundColor = color;
        }

        public void StartChange()
        {
            changing = true;
        }

        public void StopChange()
        {
            changing = false;
        }
    }
}