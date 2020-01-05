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
        [Header("Menu Fields")] public GameObject mainMenu;
        public GameObject optionsMenu;

        // Text inputs
        [Space] [Header("Text Fields")] public TMP_InputField playerNameInputText;


        /// <summary>
        ///     Load next scene in the build index
        /// </summary>
        public void PlayGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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
            optionsMenu.SetActive(true);
        }

        /// <summary>
        ///     Transition to Main Menu
        /// </summary>
        public void SwitchToMainMenu()
        {
            optionsMenu.SetActive(false);
            mainMenu.SetActive(true);
        }
    }
}