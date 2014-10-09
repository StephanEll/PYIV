using UnityEngine;
using System.Collections;

namespace PYIV.Helper
{

    public class MusicHelper : MonoBehaviour
    {

        AudioSource audioSource;
        AudioClip standardMusic;
        private float fadeRate =1f;
        // Use this for initialization
        void Start()
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            standardMusic = Resources.Load(ConfigReader.Instance.GetSetting("music", "menu")) as AudioClip;
            audioSource.clip = standardMusic;
            audioSource.Play();
            audioSource.loop = true;
        }

        public void changeToStandardMusic()
        {
            StartCoroutine(changeToMusicClip(standardMusic));
        }

        public void changeToSceneMusic(AudioClip clip)
        {
            StartCoroutine(changeToMusicClip(clip));
        }


        private IEnumerator changeToMusicClip(AudioClip clip)
        {
            while (audioSource.volume > 0.1f)
            {
                audioSource.volume = Mathf.Lerp(audioSource.volume, 0.0f, fadeRate * Time.deltaTime);
                yield return null;
            }

            audioSource.Stop();

            audioSource.clip = clip;

            audioSource.Play();

            while (audioSource.volume < 1)
            {
                audioSource.volume = Mathf.Lerp(audioSource.volume, 1.0f, fadeRate * Time.deltaTime);

                yield return null;
            }

        }



    }
}