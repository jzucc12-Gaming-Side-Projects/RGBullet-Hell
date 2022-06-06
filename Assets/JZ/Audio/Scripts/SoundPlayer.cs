using System.Collections.Generic;
using JZ.CORE.STARTUP;
using UnityEngine;


//Originally from Brackeys//
namespace JZ.AUDIO
{
    /// <summary>
    /// Manages a group of the Sound class
    /// Middle-man between sounds and other classes
    /// </summary>
    public class SoundPlayer : MonoBehaviour
    {
        [SerializeField] private List<Sound> mySounds = new List<Sound>();


        #region //Monobehaviour
        private void Awake()
        {
            foreach(Sound sound in mySounds)
                AddSource(sound);
        }

        private void OnEnable() 
        {
            VolumeSetter.VolumeUpdated += UpdateVolumes;
            GameStartUp.SetUpDone += UpdateVolumes;
        }

        private void OnDisable() 
        {
            VolumeSetter.VolumeUpdated -= UpdateVolumes;
            GameStartUp.SetUpDone -= UpdateVolumes;
        }
        #endregion

        #region //Getters
        public Sound GetSound(string _name)
        {
            Sound sound = mySounds.Find(x => x.title == _name);

            if(sound == null)
            {
                Debug.LogWarning("Couldn't find clip");
                return null;
            }
            return sound;
        }
        #endregion

        #region //Set Up
        //Public
        public void AddSound(Sound _sound)
        {
            mySounds.Add(_sound);
            AddSource(_sound);
        }

        //Private
        private void AddSource(Sound _sound)
        {
            GameObject child = new GameObject();
            child.transform.parent = transform;
            child.name = _sound.title + " sound";
            _sound.SetUpSound(child.AddComponent<AudioSource>(), child.AddComponent<SoundFader>());
        }
        #endregion

        #region //Sound Checking
        public bool IsSoundPlaying(string _name)
        {
            Sound sound = GetSound(_name);
            if(sound == null) return false;
            return sound.source.isPlaying;
        }
        
        public bool HasClip(string _name, AudioClip _clip)
        {
            Sound sound = GetSound(_name);
            if(sound == null) return true;
            return sound.clip == _clip;
        }
        #endregion

        #region //Playing and Stopping
        public void Play(string _name)
        {
            Sound sound = GetSound(_name);
            if(sound == null) return;
            sound.Play();
        }

        public void Stop(string _name)
        {
            Sound sound = GetSound(_name);
            if(sound == null) return;
            sound.Stop();
        }

        public void StopAllSFX()
        {
            foreach(Sound sound in mySounds)
            {
                if(sound.GetVolumeType() != VolumeType.sfx) continue;
                sound.Stop();
            }
        }
        #endregion

        #region //Modification
        public void UpdateVolumes()
        {
            foreach(Sound sound in mySounds)
                sound.SetSourceVolume();
        }
        
        public void ChangeClip(string _name, AudioClip _clip)
        {
            Sound sound = GetSound(_name);
            sound.ChangeClip(_clip);
        }
        #endregion
    }
}