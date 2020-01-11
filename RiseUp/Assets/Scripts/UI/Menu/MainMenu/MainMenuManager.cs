using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menu.MainMenu
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

        // Text inputs
        [Space] [Header("Text Fields")] 
        public TMP_InputField playerNameInputText;

        /// <summary>
        ///     Load next scene in the build index
        /// </summary>
        public void PlayMenu()
        {
            mainMenu.SetActive(false);
            optionsMenu.SetActive(false);
            playMenu.SetActive(true);
        }

        /// <summary>
        ///     Load tutorial level
        /// </summary>
        public void PlayTutorial()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        /// <summary>
        ///     Load regular game
        /// </summary>
        public void PlayGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
            
        }
        /// <summary>
        ///     Close application
        /// </summary>
        public void QuitGame()
        {
            Application.Quit();
        }

        /// <summary>
        ///     Transition to Options Menu
        /// </summary>
        public void SwitchToOptionsMenu()
        {
            mainMenu.SetActive(false);
            playMenu.SetActive(false);
            optionsMenu.SetActive(true);
        }

        /// <summary>
        ///     Transition to Main Menu
        /// </summary>
        public void SwitchToMainMenu()
        {
            optionsMenu.SetActive(false);
            playMenu.SetActive(false);
            mainMenu.SetActive(true);
        }
    }
}