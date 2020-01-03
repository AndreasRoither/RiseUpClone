﻿using TMPro;
using UnityEngine;

namespace UI
{
    public class GameUiManager : MonoBehaviour
    {
        public TextMeshProUGUI levelText;
        public TextMeshProUGUI scoreText;
        public TextMeshProUGUI midText;
        public GameObject retryUi;

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

        public void ToggleRetryUi(bool toggle)
        {
            retryUi.SetActive(toggle);
        }
    }
}