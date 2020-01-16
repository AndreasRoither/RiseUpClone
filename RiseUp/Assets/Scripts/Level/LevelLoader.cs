using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Level
{
    public class LevelLoader : MonoBehaviour
    {
        public float delayTime = 1f;
        public string triggerName = "Start";
        public Animator transition;
        
        private Coroutine currentRoutine = null;

        private void Start()
        {
            if (transition == null)
                Debug.LogWarning("transition not set!");
        }

        public void LoadLevel(int level)
        {
            if (currentRoutine != null) return;
            
            currentRoutine = StartCoroutine(StartTransition(level));
        }

        private IEnumerator StartTransition(int level)
        {
            transition.SetTrigger(triggerName);
            yield return new WaitForSeconds(delayTime);
            
            AsyncOperation operation = SceneManager.LoadSceneAsync(level);
            operation.allowSceneActivation = true;
            
            currentRoutine = null;
        }
    }
}
