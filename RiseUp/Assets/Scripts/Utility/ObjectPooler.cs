using System;
using System.Collections.Generic;
using UnityEngine;

namespace Utility
{
    /// <summary>
    ///     Object pooler that is used to spawn objects without needing to instantiate them.
    ///     Holds references to objects that should be spawned and will reuse objects instead of destroying them.
    /// </summary>
    /// Author: Andreas Roither
    public class ObjectPooler : MonoBehaviour
    {
        [Serializable]
        public class Pool
        {
            public GameObject prefab;
            public int size;
            public string tag;
        }

        public bool destroyOnLoad;

        [SerializeField] 
        public Dictionary<string, Queue<GameObject>> poolDictionary;
        public List<Pool> pools;

        private static ObjectPooler instance;

        /// <summary>
        ///     Create all objects with pools on awake
        /// </summary>
        private void Awake()
        {
            //Check if instance already exists
            if (instance == null)
            {
                instance = this;
            }

            //If instance already exists and it's not this:
            else if (instance != this)
            {
                Destroy(gameObject);
                return;
            }

            poolDictionary = new Dictionary<string, Queue<GameObject>>();
            var poolParent = new GameObject("Pools");

            foreach (var pool in pools)
            {
                var poolType = new GameObject(pool.tag);
                Queue<GameObject> objectPool = new Queue<GameObject>();

                for (var i = 0; i < pool.size; ++i)
                {
                    var obj = Instantiate(pool.prefab);
                    obj.SetActive(false);
                    objectPool.Enqueue(obj);

                    if (!destroyOnLoad)
                        DontDestroyOnLoad(obj);
                    else
                        obj.transform.parent = poolType.transform;
                }

                poolDictionary.Add(pool.tag, objectPool);

                if (!destroyOnLoad)
                {
                    DontDestroyOnLoad(poolType);
                    Destroy(poolType);
                }
                else
                {
                    poolType.transform.parent = poolParent.transform;
                }
            }

            if (destroyOnLoad) return;
            DontDestroyOnLoad(gameObject);
            Destroy(poolParent);
        }

        /// <summary>
        ///     Retrieve object from pool and set at desired position/rotation
        /// </summary>
        /// <param name="poolTag">Pool tag from which an object should be received</param>
        /// <param name="position">Target position</param>
        /// <param name="rotation">Target rotation</param>
        /// <returns></returns>
        public GameObject SpawnFromPool(string poolTag, Vector3 position, Quaternion rotation)
        {
            if (!poolDictionary.ContainsKey(poolTag))
            {
                Debug.LogWarning($"ObjectPooler: Pool with tag {poolTag} does not exist");
                return null;
            }

            var obj = poolDictionary[poolTag].Dequeue();

            obj.transform.position = position;
            obj.transform.rotation = rotation;
            obj.SetActive(true);

            poolDictionary[poolTag].Enqueue(obj);

            return obj;
        }
    }
}