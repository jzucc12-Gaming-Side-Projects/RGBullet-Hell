using JZ.CORE;
using JZ.INPUT;
using UnityEngine.InputSystem;

namespace CBH.PLAYER.INPUT
{
    /// <summary>
    /// Hub for all player inputs
    /// </summary>
    public class PlayerInputSystem : MyInputSystemBehaviour<PlayerInputActions>
    {
        #region //Input actions
        public InputAction movementInput { get; private set; }
        public InputAction gamepadAimInput { get; private set; }
        public InputAction mouseAimInput { get; private set; }
        public InputAction fireInput { get; private set; }
        public InputAction reloadInput { get; private set; }
        public InputAction shiftWeaponInput { get; private set; }
        public InputAction setWeaponInput { get; private set; }
        public InputAction gameSpeedInput { get; private set; }
        #endregion


        #region //Monobehaviour
        protected override void Awake()
        {
            inputs = InputManager.playerInputs;
            movementInput = inputs.Player.Move;
            gamepadAimInput = inputs.Player.GamepadAim;
            mouseAimInput = inputs.Player.MouseAim;
            fireInput = inputs.Player.Fire;
            reloadInput = inputs.Player.Reload;
            shiftWeaponInput = inputs.Player.ShiftWeaponIndex;
            setWeaponInput = inputs.Player.SetWeaponIndex;
            gameSpeedInput = inputs.Player.ChangeGameSpeed;
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            GamePause.OnPause += DisableAllActions;
            GamePause.OnUnPause += EnableAllActions;
            EndGame.OnEndGame += DisableAllActions;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            GamePause.OnPause -= DisableAllActions;
            GamePause.OnUnPause -= EnableAllActions;
            EndGame.OnEndGame -= DisableAllActions;
        }
        #endregion

        #region //Start up and shutdown
        protected override void EnableAllActions()
        {
            movementInput.Enable();
            mouseAimInput.Enable();
            gamepadAimInput.Enable();
            fireInput.Enable();
            reloadInput.Enable();
            shiftWeaponInput.Enable();
            setWeaponInput.Enable();
            gameSpeedInput.Enable();
        }

        protected override void DisableAllActions()
        {
            movementInput.Disable();
            mouseAimInput.Disable();
            gamepadAimInput.Disable();
            fireInput.Disable();
            reloadInput.Disable();
            shiftWeaponInput.Disable();
            setWeaponInput.Disable();
            gameSpeedInput.Disable();
        }
        #endregion
    }
}