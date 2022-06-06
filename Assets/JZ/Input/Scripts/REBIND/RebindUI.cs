using System.Collections;
using CBH.CORE;
using JZ.CORE;
using JZ.INPUT.REBIND;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace JZ.INPUT.UI
{
    /// <summary>
    /// Overlay for when rebinding is active
    /// </summary>
    public class RebindUI : MonoBehaviour
    {
        #region //Cached UI Components
        [SerializeField] private GameObject rebindUIContainer = null;
        [SerializeField] private GameObject keyboardRebindUI = null;
        [SerializeField] private GameObject gamepadRebindUI = null;
        [SerializeField] private TextMeshProUGUI controlText = null;
        [SerializeField] private TextMeshProUGUI cancelTextPC = null;
        [SerializeField] private TextMeshProUGUI cancelTextGamepad = null;
        [SerializeField] private SettingsShifter shifter = null;
        private ControlsScreenUI controlsUI = null;
        private RebindManager rebindManager = null;
        #endregion

        #region //Input modification
        private GeneralInputs generalInputs = null;
        private GamePause gamePause = null;
        #endregion

        
        #region //Monobehaviour
        private void Awake()
        {
            rebindManager = FindObjectOfType<RebindManager>();
            generalInputs = InputManager.generalInputs;
            gamePause = FindObjectOfType<GamePause>();
            controlsUI = GetComponent<ControlsScreenUI>();
        }

        private void OnEnable()
        {
            RebindManager.RebindingStarted += OnRebindStarted;
            RebindManager.RebindingFinished += OnRebindFinished;
        }

        private void OnDisable()
        {
            RebindManager.RebindingStarted -= OnRebindStarted;
            RebindManager.RebindingFinished -= OnRebindFinished;
        }
        #endregion

        #region //Rebinding operations
        //Public
        public void ResetBindings()
        {
            var activeScreen = controlsUI.GetActiveScreen();
            foreach(var display in activeScreen.GetComponentsInChildren<RebindableControlDisplay>())
            {
                var action = display.GetAction();
                action.RemoveAllBindingOverrides();

                for(int ii = 0; ii < action.bindings.Count; ii++)
                {
                    PlayerPrefs.DeleteKey($"{action.actionMap} {action.name} {ii}");
                }
            }

            foreach(var display in FindObjectsOfType<RebindableControlDisplay>())
                display.UpdateUI();
        }

        //Show rebinding UI
        public void OpenForRebinding()
        {
            //Prevent exiting Rebind through other methods
            controlsUI.DisableForRebind();
            shifter.enabled = false;
            ShowRebindUI();
            if(gamePause != null)
                gamePause.enabled = false;

            //Hide non-rebindable inputs
            var activeScreen = controlsUI.GetActiveScreen();
            foreach(var display in activeScreen.GetComponentsInChildren<RebindableControlDisplay>())
            {
                if(!display.EnableRebind(true))
                {
                    display.Hide();
                }
            }
        }

        //Private
        private void CloseRebindMenu(InputAction.CallbackContext context)
        {
            CloseForRebinding();
            generalInputs.Map.Pause.performed -= CloseRebindMenu;
        }

        //Hide rebinding UI
        private void CloseForRebinding()
        {
            //Reactivate other UI
            shifter.enabled = true;
            controlsUI.EnableAfterRebind();
            HideRebindUI();
            if(gamePause != null)
                gamePause.enabled = true;

            //Close inputs for rebinding
            var activeScreen = controlsUI.GetActiveScreen();
            foreach(var display in activeScreen.GetComponentsInChildren<RebindableControlDisplay>())
            {
                display.EnableRebind(false);
                display.Show();
            }
        }
        #endregion

        #region //Rebinding UI modification
        private void ShowRebindUI()
        {
            OnRebindFinished();
            rebindUIContainer.SetActive(true);
            gamepadRebindUI.SetActive(controlsUI.IsOnGamepad());
            keyboardRebindUI.SetActive(!controlsUI.IsOnGamepad());
        }

        private void HideRebindUI()
        {
            rebindUIContainer.SetActive(false);
        }

        private void OnRebindStarted(InputAction _action)
        {
            controlText.text = $"Select new input for: {_action.name}";
            cancelTextPC.text = $"Cancel current rebinding: ";
            cancelTextGamepad.text = $"Cancel current rebinding: ";
            generalInputs.Map.Pause.performed -= CloseRebindMenu;
        }

        private void OnRebindFinished()
        {
            StartCoroutine(TurnOnDelay());
            controlText.text = $"Press input you would like to rebind";
            cancelTextPC.text = $"Exit rebinding menu: ";
            cancelTextGamepad.text = $"Exit rebinding menu: ";
        }

        private IEnumerator TurnOnDelay()
        {
            //This ensures gamepad pause can't double press
            for(int ii = 0; ii < 3; ii++)
            {
                yield return null;
            }
            generalInputs.Map.Pause.performed += CloseRebindMenu;
        }
        #endregion
    }
}