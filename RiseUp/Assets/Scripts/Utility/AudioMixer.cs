using System.Security.Cryptography;
using UnityEngine;

namespace Utility
{
    public class AudioMixer : MonoBehaviour
    {
        public AudioClip[] clips;
        public AudioSource audioSource;
        public bool randomPlay = false;
        private int clipOrder = 0;
        private static AudioMixer instance;

        private void Update()
        {
            if (audioSource.isPlaying) return;

            audioSource.clip = randomPlay ? GetRandomClip() : GetNextClip();
            audioSource.Play();
        }

        private AudioClip GetRandomClip()
        {
            return clips[Random.Range(0, clips.Length)];
        }

        private AudioClip GetNextClip()
        {
            if (clipOrder >= clips.Length - 1)
            {
                clipOrder = 0;
            }
            else
            {
                clipOrder += 1;
            }

            return clips[clipOrder];
        }

        private void Awake()
        {
            if (instance != null)
                DestroyImmediate(this);
            else
            {
                instance = this;
                audioSource.loop = false;
                DontDestroyOnLoad(transform.gameObject);            
            }
        }
    }
}