using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Utility
{
    public class AudioMixer : MonoBehaviour
    {
        public AudioClip[] clips;
        public string[] clipPaths;
        public AudioSource audioSource;
        public bool randomPlay = false;
        public float audioFadeTime = 1f;

        private int clipOrder = 0;
        private static AudioMixer instance;
        private int loadedSceneIndex = -1;

        private void Awake()
        {
            if (instance != null)
            {
                DestroyImmediate(this);
            }
            else
            {
                instance = this;
                audioSource.loop = false;
                DontDestroyOnLoad(this);
                SceneManager.sceneLoaded += OnSceneLoaded;
            }
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode)
        {
            if (loadedSceneIndex == -1) loadedSceneIndex = scene.buildIndex;
            else if (scene.buildIndex != loadedSceneIndex)
            {
                StartCoroutine(scene.buildIndex == 0 ? FadeOutSound() : FadeInSound());
            }
        }

        private void Update()
        {
            if (audioSource.isPlaying) return;
            if (SceneManager.GetActiveScene().buildIndex == 0) return;

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

        private IEnumerator FadeOutSound()
        {
            float startVolume = audioSource.volume;

            while (audioSource.volume > 0)
            {
                audioSource.volume -= startVolume * Time.deltaTime / audioFadeTime;
                yield return null;
            }

            audioSource.Stop();
            audioSource.volume = startVolume;
        }

        private IEnumerator FadeInSound()
        {
            audioSource.Stop();
            float initialVolume = audioSource.volume;
            float startVolume = audioSource.volume;

            audioSource.volume = 0;

            while (audioSource.volume < initialVolume)
            {
                audioSource.volume += startVolume * Time.deltaTime / audioFadeTime;
                yield return null;
            }
        }
    }
}