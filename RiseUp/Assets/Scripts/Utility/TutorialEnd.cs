using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Utility
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class TutorialEnd : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player")) return;

            // Use a coroutine to load the Scene in the background
            StartCoroutine(LoadYourAsyncScene());
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
           
        }

        IEnumerator LoadYourAsyncScene()
        {
            // The Application loads the Scene in the background as the current Scene runs.
            // This is particularly good for creating loading screens.
            // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
            // a sceneBuildIndex of 1 as shown in Build Settings.

            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(0);

            // Wait until the asynchronous scene fully loads
            while (!asyncLoad.isDone)
            {
                yield return null;
            }
        }
    }
}