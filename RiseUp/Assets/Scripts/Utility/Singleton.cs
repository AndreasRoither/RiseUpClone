using System;
using UnityEngine;

namespace Utility
{
    /// <summary MyClassName="{}">
    ///     Inherit from this base class to create a singleton.
    ///     e.g. public class MyClassName : Singleton
    /// </summary>
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        // Check to see if we're about to be destroyed.
        private static readonly object mLock = new object();
        private static T mInstance;
        
        /// <summary>
        ///     Access singleton instance through this propriety.
        /// </summary>
        public static T Instance
        {
            get
            {
                lock (mLock)
                {
                    if (mInstance != null) return mInstance;
                    // Search for existing instance.
                    mInstance = (T) FindObjectOfType(typeof(T));

                    // Create new instance if one doesn't already exist.
                    if (mInstance != null) return mInstance;
                    
                    // Need to create a new GameObject to attach the singleton to.
                    var singletonObject = new GameObject();
                    mInstance = singletonObject.AddComponent<T>();
                    singletonObject.name = typeof(T) + " (Singleton)";

                    // Make instance persistent.
                    DontDestroyOnLoad(singletonObject);

                    return mInstance;
                }
            }
        }

        private void OnApplicationQuit()
        {
            mInstance = null;
        }
        
        private void OnDestroy()
        {
            mInstance = null;
        }
    }
}