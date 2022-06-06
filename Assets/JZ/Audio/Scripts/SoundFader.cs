using System.Collections;
using UnityEngine;

namespace JZ.AUDIO
{
    /// <summary>
    /// Fades volume of sounds
    /// </summary>
    public class SoundFader : MonoBehaviour
    {
        private AudioSource source;


        private void Awake()
        {
            source = GetComponent<AudioSource>();
        }
    
        public void FadeTo(float _fadeTime, float _finalVolumePercentage)
        {
            StopAllCoroutines();
            StartCoroutine(FadeRoutine2(_fadeTime, _finalVolumePercentage));
        }

        public void FadeOut(float _fadeTime)
        {
            FadeTo(_fadeTime, 0);
        }

        private IEnumerator FadeRoutine2(float _fadeTime, float _finalVolumePercentage)
        {
            float startVolume = source.volume;
            float currTime = 0f;

            yield return new WaitUntil(() => 
            {
                float newVolumePercentage = Mathf.Lerp(1, _finalVolumePercentage, Mathf.Min(currTime / _fadeTime, 1));
                source.volume = startVolume * newVolumePercentage;
                currTime += Time.deltaTime;
                return currTime >= _fadeTime;
            });

            source.volume = startVolume * _finalVolumePercentage;
        }
    }
}
