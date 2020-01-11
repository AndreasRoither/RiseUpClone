using UnityEngine;

namespace Level
{
    /// <summary>
    /// Information class attached to a level prop, that is spawned at the corresponding level difficulty
    /// </summary>
    public class LevelProp : MonoBehaviour
    {
        public int difficultyLevel;
        public float height;
        public float marginTop;
        public float marginBottom;
    }
}