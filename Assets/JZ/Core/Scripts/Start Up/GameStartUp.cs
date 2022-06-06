using System;
using UnityEngine;

namespace JZ.CORE.STARTUP
{
    /// <summary>
    /// Logic run at the start of the game
    /// </summary>
    public class GameStartUp : MonoBehaviour
    {
        [SerializeField] private int frameRate = 60;
        [SerializeField] private bool runInBackground = true;
        public static event Action SetUpDone;

        private void Awake()
        {
            Application.targetFrameRate = frameRate;
            Application.runInBackground = runInBackground;
            ColorStartUp();
            InitiateVolume();
            SetUpDone?.Invoke();
        }

        private void Update() 
        {
            if(!GameSettings.InDevMode()) return;

            if(JZ.INPUT.Utils.CheckKeyCombo("QWER"))
                ResetData();
        }

        private void InitiateVolume()
        {
            if(!PlayerPrefs.HasKey(GameSettings.masterVolKey))
            {
                PlayerPrefs.SetFloat(GameSettings.masterVolKey, 0.5f);
                PlayerPrefs.SetFloat(GameSettings.musicVolKey, 0.5f);
                PlayerPrefs.SetFloat(GameSettings.sfxVolKey, 0.5f);
            }

            GameSettings.SetVolume(VolumeType.master, PlayerPrefs.GetFloat(GameSettings.masterVolKey));
            GameSettings.SetVolume(VolumeType.music, PlayerPrefs.GetFloat(GameSettings.musicVolKey));
            GameSettings.SetVolume(VolumeType.sfx, PlayerPrefs.GetFloat(GameSettings.sfxVolKey));
        }

        private static void ColorStartUp()
        {
            GameSettings.SetColorMode((ColorMode)PlayerPrefs.GetInt(PlayerPrefKeys.colorMode));
            PlayerPrefs.SetString("CircleColorDefault", "#FF6B6B");
            PlayerPrefs.SetString("TriangleColorDefault", "#78FF4F");
            PlayerPrefs.SetString("SquareColorDefault", "#6B90FF");
            PlayerPrefs.SetString("BackgroundColorDefault", "#262516");
        }
    
        private void ResetData()
        {
            Debug.Log("reset data");
            PlayerPrefs.DeleteAll();
            InitiateVolume();
            ColorStartUp();
        }
    }
}
