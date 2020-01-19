using UnityEngine;
using Utility;

namespace Controller
{
    public class DifficultyController : Singleton<DifficultyController>
    {
        public int highestDifficulty;
        public float difficultyModulo = 100f;

        private void Update()
        {
            UpdateDifficulty();
        }

        private void UpdateDifficulty()
        {
            if ((int) RisingUpController.Instance.GetHeight() <= 0) return;
            if ((int) RisingUpController.Instance.GetHeight() % difficultyModulo != 0) return;
            if (DifficultyLevel < highestDifficulty) ++DifficultyLevel;
        }

        public int DifficultyLevel { get; private set; } = 1;
    }
}