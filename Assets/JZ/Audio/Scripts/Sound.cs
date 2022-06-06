using UnityEngine;

namespace JZ.AUDIO
{
    /// <summary>
    /// Audioclips with their own AudioSources
    /// </summary>
    [System.Serializable]
    public class Sound
    {
        #region //Variables
        [Header("Sound Properties")]
        [SerializeField] public string title = "";
        [SerializeField] public AudioClip clip = null;
        [SerializeField] private VolumeType myType = VolumeType.sfx;
        [HideInInspector] public AudioSource source = null;
        [HideInInspector] public SoundFader fader = null;

        [Header("Source Properties")]
        [Range(0f, 1f)] [SerializeField] private float volume = 1f;
        [Range(0.1f, 3f)] [SerializeField] private float pitch = 1f;
        [SerializeField] private bool isBackwards = false;
        [SerializeField] private bool loop = false;
        [SerializeField] private bool playOnStart = false;
        #endregion


        #region //Constructor
        public Sound(string _title, AudioClip _clip, VolumeType _type)
        {
            title = _title;
            myType = _type;
            clip = _clip;
        }
        #endregion

        #region //Getters
        public VolumeType GetVolumeType() { return myType; }
        public float GetBaseVolume()
        {
            return volume * GameSettings.GetAdjustedVolume(myType);
        }
        #endregion

        #region //Set Up
        public void SetUpSound(AudioSource _source, SoundFader _fader)
        {
            fader = _fader;
            source = _source;
            source.clip = clip;
            source.pitch = pitch;
            source.loop = loop;
            SetSourceVolume();
            
            if(isBackwards)
            {
                source.pitch *= -1;
                source.timeSamples = source.clip.samples - 1;
            }

            if(playOnStart) Play();
            else source.playOnAwake = false;
        }
        #endregion
    
        #region //Playing and Stopping
        public void Play()
        {
            fader.StopAllCoroutines();
            SetSourceVolume();
            source.Play();
        }

        public void Stop()
        {
            fader.StopAllCoroutines();
            source.Stop();
        }
        #endregion

        #region //Modification
        public void SetSourceVolume(float _mod = 1)
        {
            source.volume = volume * GameSettings.GetAdjustedVolume(myType) * _mod;
        }

        public void ChangeClip(AudioClip _clip)
        {
            if(clip == _clip)
            {
                Debug.LogWarning("Tried to change to the currently active clip");
                return;
            }

            Stop();
            clip = _clip;
            source.clip = _clip;
        }
        #endregion
    }
}