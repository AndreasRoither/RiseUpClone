using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.Menu.MainMenu
{
    /// <summary>
    ///     Manages Menu state and transition
    /// </summary>
    /// Author: Andreas Roither
    public class MainMenuManager : MonoBehaviour
    {
        [Header("Menu Fields")] 
        public GameObject mainMenu;
        public GameObject optionsMenu;
        public GameObject playMenu;
        public TextMeshProUGUI score;
        
        [Space] [Header("Text Fields")] 
        public TMP_InputField playerNameInputText;

        private const string PlayerScore = "player_highscore";
        
        public void Awake()
        {
            score.text = PlayerPrefs.GetInt(PlayerScore, 0).ToString();
        }

        public void PlayMenu()
        {
            mainMenu.SetActive(false);
            optionsMenu.SetActive(false);
            playMenu.SetActive(true);
        }
        
        public void PlayTutorial()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        
        public void PlayGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
        }

        public void QuitGame()
        {
            Application.Quit();
        }
        
        public void SwitchToOptionsMenu()
        {
            mainMenu.SetActive(false);
            playMenu.SetActive(false);
            optionsMenu.SetActive(true);
        }
        
        public void SwitchToMainMenu()
        {
            optionsMenu.SetActive(false);
            playMenu.SetActive(false);
            mainMenu.SetActive(true);
        }
    }
}