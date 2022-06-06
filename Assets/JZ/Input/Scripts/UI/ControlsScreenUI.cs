using JZ.CORE;
using UnityEngine;
using UnityEngine.UI;

namespace JZ.INPUT.UI
{
    /// <summary>
    /// UI for the basic controls screen
    /// Allows for switching between gamepad and PC controls easily
    /// </summary>
    public class ControlsScreenUI : MonoBehaviour
    {
        #region //Cached Components
        [SerializeField] private Button toKeyboardButton = null;
        [SerializeField] private Button toGamepadButton = null;
        [SerializeField] private GameObject keyboardScreen = null;
        [SerializeField] private GameObject gamepadScreen = null;
        private MenuingInputSystem menuSystem = null;
        #endregion


        #region //Monobehaviour
        private void Awake() 
        {
            menuSystem = new MenuingInputSystem(new MenuingInputs());    
        }

        private void OnEnable() 
        {
            SwapScreen(false);
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
                SwapScreen(true);
            }
            else if(menuSystem.GetXNav() < 0)
            {
                menuSystem.ExpendXDir();
                SwapScreen(false);
            }
        }
        #endregion

        #region //UI Modification
        public void SwapScreen(bool _toGamepad)
        {
            gamepadScreen.SetActive(_toGamepad);
            keyboardScreen.SetActive(!_toGamepad);
            toGamepadButton.gameObject.SetActive(!_toGamepad);
            toKeyboardButton.gameObject.SetActive(_toGamepad);
            GetComponent<ContentSizeFitterUpdater>().UpdateContentSizeFitter();
        }

        public void DisableForRebind()
        {
            toKeyboardButton.gameObject.SetActive(false);
            toGamepadButton.gameObject.SetActive(false);
            menuSystem.Deactivate();
        }

        public void EnableAfterRebind()
        {
            SwapScreen(IsOnGamepad());
            menuSystem.Activate();
        }
        #endregion

        #region //Getters
        public bool IsOnGamepad()
        {
            return gamepadScreen.activeInHierarchy;
        }

        public GameObject GetActiveScreen()
        {
            if(IsOnGamepad())
                return gamepadScreen;
            else
                return keyboardScreen;
        }
        #endregion
    }
}