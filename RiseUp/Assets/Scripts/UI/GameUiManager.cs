using TMPro;
using UnityEngine;
using Utility;

namespace UI
{
    public class GameUiManager : MonoBehaviour
    {
        public TextMeshProUGUI levelText;
        public TextMeshProUGUI midText;
        public TextMeshProUGUI midText2;
        public GameObject retryUi;
        public TextMeshProUGUI scoreText;
        public ColorGradientChanger changer;

        public void SetScore(int score)
        {
            scoreText.text = score.ToString();
        }

        public void SetLevel(int level)
        {
            levelText.text = level.ToString();
        }

        public void SetMidText(string text)
        {
            midText.text = text;
        }
        
        public void SetMidText2(string text)
        {
            if (midText2 == null) return;
            midText2.text = text;
        }

        public void ToggleRetryUi(bool toggle)
        {
            retryUi.SetActive(toggle);
        }

        public void StartGradientChange()
        {
            if (changer == null) return;
            changer.StartChange();
        }
        
        public void StopGradientChange()
        {
            if (changer == null) return;
            changer.StopChange();
        }
    }
}