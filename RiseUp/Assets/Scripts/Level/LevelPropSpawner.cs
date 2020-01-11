﻿using System;
using Controller;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Random = System.Random;

namespace Level
{
    /// <summary>
    ///     Spawns Level Props depending on the difficulty
    ///     New objects are always spawned when the last prop spawn height is reached
    /// </summary>
    public class LevelPropSpawner : MonoBehaviour
    {
        [Header("Initial Spawn")]
        public int initialSpawnAmount;
        public float initialSpawnDistance;
        
        [Header("Ingame spawn")]
        public int spawnAmount = 2;
        public float spawnMargin;
        
        private Vector3 nextLevelPropPosition = Vector3.zero;
        private float spawnHeightTrigger;
        private readonly Random random = new Random();

        private void Awake()
        {
            nextLevelPropPosition.y += initialSpawnDistance;

            // spawn initial props
            for (int i = 0; i < initialSpawnAmount; ++i)
            {
                spawnHeightTrigger = nextLevelPropPosition.y - spawnMargin;
                SpawnProp();
            }
        }

        private void Update()
        {
            if (!(RisingUpController.Instance.GetHeight() >= spawnHeightTrigger)) return;

            for (int i = 0; i < spawnAmount; ++i)
            {
                spawnHeightTrigger = nextLevelPropPosition.y - spawnMargin;
                SpawnProp();
            }
        }

        private void SpawnProp()
        {
            var propList = LevelPooler.Instance.GetDifficultyList(DifficultyController.Instance.DifficultyLevel);
            var prop = propList[random.Next(0, propList.Count)];

            // apply margin in case a prop needs extra space
            nextLevelPropPosition.y += prop.marginBottom;
            Instantiate(prop, nextLevelPropPosition, Quaternion.identity);
            nextLevelPropPosition.y = nextLevelPropPosition.y + prop.height + prop.marginTop + spawnMargin;
        }

        #region Gizmos

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(new Vector3(0, spawnHeightTrigger, 0), Vector3.one);
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireCube(nextLevelPropPosition, Vector3.one);
        }

        #endregion
    }
}