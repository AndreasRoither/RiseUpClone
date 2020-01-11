using System.Collections.Generic;
using UnityEngine;
using Utility;

namespace Level
{
    public class LevelPooler : Singleton<LevelPooler>
    {
        [Header("Level Props")] public List<LevelProp> difficulty1 = new List<LevelProp>();
        public List<LevelProp> difficulty2 = new List<LevelProp>();
        public List<LevelProp> difficulty3 = new List<LevelProp>();
        public List<LevelProp> difficulty4 = new List<LevelProp>();
        public List<LevelProp> difficulty5 = new List<LevelProp>();

        public List<LevelProp> GetDifficultyList(int difficulty)
        {
            switch (difficulty)
            {
                case 1:
                    return difficulty1;
                case 2:
                    return difficulty2;
                case 3:
                    return difficulty3;
                case 4:
                    return difficulty4;
                case 5:
                    return difficulty5;
                default: return difficulty1;
            }
        }
    }
}