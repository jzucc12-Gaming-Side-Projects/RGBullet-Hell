using System;
using JZ.INPUT;
using UnityEngine;
using UnityEngine.InputSystem;

namespace JZ.CORE
{
    /// <summary>
    /// Pauses game from player input
    /// </summary>
    public class GamePause : MonoBehaviour
    {
        #region //Input variables
        private GeneralInputs inputs = null;
        private InputAction pauseInput = null;
        #endregion

        #region //Cached components
        [SerializeField] private GameObject pauseMenu = null;
        [SerializeField] private GameObject pauseMenuButtons = null;
        [SerializeField] private GameObject[] menusToClose = new GameObject[0];
        #endregion

        #region //State variables
        private bool isPaused => Time.timeScale == 0;
        public static event Action OnPause;
        public static event Action OnUnPause;
        #endregion


        #region //Monobehaviour
        private void Awake()
        {
            inputs = InputManager.generalInputs;
            pauseInput = inputs.Map.Pause;
        }

        private void OnEnable()
        {
            pauseInput.performed += OnPauseInput;
        }

        private void OnDisable()
        {
            pauseInput.performed -= OnPauseInput;
        }
        #endregion

        #region //Pausing
        //Public
        public void PausePressed(bool _pause)
        {
            if(_pause)
                Pause();
            else
                UnPause();
        }

        //Private
        private void OnPauseInput(InputAction.CallbackContext context)
        {
            PausePressed(!isPaused);
        }

        private void Pause()
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
            OnPause?.Invoke();

            pauseMenuButtons.SetActive(true);
            foreach(var menu in menusToClose)
                menu.SetActive(false);
        }

        private void UnPause()
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1;
            OnUnPause?.Invoke();
        }
        #endregion
    }

}