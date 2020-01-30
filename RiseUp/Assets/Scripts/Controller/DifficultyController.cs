using UnityEngine;
using Utility;

namespace Controller
{
    public class DifficultyController : Singleton<DifficultyController>
    {
        public int highestDifficulty;
        public float difficultyModulo = 100f;
        public float difficultyDelay;

        private float updateTime;
        
        private void Update()
        {
            UpdateDifficulty();
        }

        private void UpdateDifficulty()
        {
            if (Time.time < updateTime + difficultyDelay) return;
            if ((int) RisingUpController.Instance.GetHeight() <= 0) return;
            if ((int) RisingUpController.Instance.GetHeight() % difficultyModulo != 0) return;
            if (DifficultyLevel >= highestDifficulty) return;
            
            ++DifficultyLevel;
            updateTime = Time.time;
        }

        public int DifficultyLevel { get; private set; } = 1;
    }
}