using System;
using UnityEngine;
using UnityEngine.UI;

namespace JZ.AUDIO
{
    /// <summary>
    /// Alters and saves the volume game settings
    /// </summary>
    public class VolumeSetter : MonoBehaviour
    {
        #region //Variables
        private Slider mySlider = null;
        [SerializeField] private VolumeType myType = VolumeType.master;
        private float vol = 0;
        public static event Action VolumeUpdated;
        #endregion


        #region //Monobehaviour
        private void Awake() 
        {
            mySlider = GetComponentInChildren<Slider>();
        }

        private void Start() 
        {
            mySlider.value = GameSettings.GetVolume(myType);
            vol = mySlider.value;
        }

        private void OnEnable() 
        {
            mySlider.onValueChanged.AddListener(SetVolume);
        }

        private void OnDisable() 
        {
            mySlider.onValueChanged.RemoveListener(SetVolume);
        }
        #endregion

        #region //Modify Volume
        //Public
        public void ResetVolume()
        {
            float defaultVol = GameSettings.GetDeffaultVolume(myType);
            mySlider.value = defaultVol;
            SetVolume(defaultVol);
        }

        public void SaveVolume()
        {
            switch(myType)
            {
                case VolumeType.master:
                    PlayerPrefs.SetFloat(GameSettings.masterVolKey, vol);
                    return;
                case VolumeType.music:
                    PlayerPrefs.SetFloat(GameSettings.musicVolKey, vol);
                    return;
                case VolumeType.sfx:
                    PlayerPrefs.SetFloat(GameSettings.sfxVolKey, vol);
                    return;
            }
        }

        //Private
        private void SetVolume(float _newValue)
        {
            GameSettings.SetVolume(myType, _newValue);
            vol = _newValue;
            SaveVolume();
            VolumeUpdated?.Invoke();
        }
        #endregion
    }
}
