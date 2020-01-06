using UnityEngine;

namespace UI
{
    /// <summary>
    ///     Simple class to deactivate text if hit in the tutorial
    /// </summary>
    public class TutorialText : MonoBehaviour
    {
        public void Hit()
        {
            gameObject.SetActive(false);
        }
    }
}