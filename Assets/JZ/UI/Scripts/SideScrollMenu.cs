using UnityEngine;
using UnityEngine.UI;
using JZ.INPUT;
using JZ.AUDIO;

namespace JZ.UI.MENU
{
    /// <summary>
    /// Menu entry that has multiple options that can be scrolled through
    /// </summary>
    public class SideScrollMenu : MonoBehaviour
    {
        #region //Component variables
        [SerializeField] private Button previousArrow = null;
        [SerializeField] private Button nextArrow = null;
        [SerializeField] private GameObject[] modes = new GameObject[0];
        private SoundPlayer musicPlayer = null;
        private MenuingInputSystem menuSystem;
        #endregion

        #region //Scroll type
        [SerializeField] private bool isForSong = true;
        string prefsKey => isForSong ? PlayerPrefKeys.startingSong : PlayerPrefKeys.colorMode;
        [SerializeField, HideIf("isForSong")] private GameObject customColorGO = null;
        #endregion


        #region //Monobehaviour
        private void Awake() 
        {
            menuSystem = new MenuingInputSystem(new MenuingInputs());
            int startMode = PlayerPrefs.GetInt(prefsKey, 0);

            if(isForSong)
            {
                musicPlayer = GameObject.FindGameObjectWithTag("Background Music").GetComponent<SoundPlayer>();
                SetSong(startMode, false);
            }
            else
            {
                SetColor(startMode);
            }
        }

        private void OnEnable() 
        {
            menuSystem.Activate();
        }

        private void OnDisable() 
        {
            menuSystem.Deactivate();
        }

        private void Update()
        {
            if(menuSystem.GetXNav() > 0)
            {
                menuSystem.ExpendXDir();
                if(nextArrow.gameObject.activeInHierarchy) NextMode();
            }
            else if(menuSystem.GetXNav() < 0)
            {
                menuSystem.ExpendXDir();
                if(previousArrow.gameObject.activeInHierarchy) PreviousMode();
            }
        }
        #endregion

        #region//Changing mode
        public void NextMode()
        {
            int newMode = PlayerPrefs.GetInt(prefsKey, 0) + 1;
            if(isForSong) SetSong(newMode, true);
            else SetColor(newMode);
            
        }

        public void PreviousMode()
        {
            int newMode = PlayerPrefs.GetInt(prefsKey, 0) - 1;
            if(isForSong) SetSong(newMode, true);
            else SetColor(newMode);
        }

        private void SetSong(int _songNumber, bool _changeSong)
        {
            PlayerPrefs.SetInt(PlayerPrefKeys.startingSong, _songNumber);
            for(int ii = 0; ii < modes.Length; ii++)
                modes[ii].SetActive(ii == _songNumber);

            previousArrow.gameObject.SetActive(_songNumber > 0);
            nextArrow.gameObject.SetActive(_songNumber < modes.Length - 1);

            if(_changeSong)
            {
                AudioClip clip = Resources.Load<AudioClip>($"Music/Song 0{_songNumber+1}");
                musicPlayer.ChangeClip("Background Track", clip);
                musicPlayer.Play("Background Track");
            }
        }

        private void SetColor(int _modeNumber)
        {
            ColorMode newColorMode = (ColorMode)_modeNumber;
            PlayerPrefs.SetInt(PlayerPrefKeys.colorMode, _modeNumber);
            GameSettings.SetColorMode(newColorMode);

            for(int ii = 0; ii < modes.Length; ii++)
                modes[ii].SetActive(ii == _modeNumber);

            previousArrow.gameObject.SetActive(_modeNumber > 0);
            nextArrow.gameObject.SetActive(_modeNumber < modes.Length - 1);
            customColorGO.SetActive(newColorMode == ColorMode.custom);
        }
        #endregion
    }
}