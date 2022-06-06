using UnityEngine;
using UnityEngine.InputSystem;

namespace CBH.PLAYER.ABILITY
{
    /// <summary>
    /// Aim from player input
    /// </summary>
    public class PlayerAiming : PlayerAbilityDataContainer
    {
        #region //Input actions
        private InputAction gamepadAimInput = null;
        private InputAction mouseAimInput = null;
        #endregion

        #region //Aiming variables
        private float currentAimAngle;
        #endregion

        
        #region //Monobehaviour
        protected override void Awake()
        {
            base.Awake();
            mouseAimInput = inputSystem.mouseAimInput;
            gamepadAimInput = inputSystem.gamepadAimInput;
        }

        private void OnEnable()
        {
            mouseAimInput.performed += AimFromMouse;
            gamepadAimInput.performed += AngleFromGamepad;
        }

        private void OnDisable()
        {
            mouseAimInput.performed -= AimFromMouse;
            gamepadAimInput.performed -= AngleFromGamepad;
        }

        private void FixedUpdate()
        {
            rb.SetRotation(currentAimAngle);
        }
        #endregion

        #region //Aiming
        private void AimFromMouse(InputAction.CallbackContext context)
        {
            Vector2 mouseAim = cam.ScreenToWorldPoint(Input.mousePosition);
            Vector2 aimVector = mouseAim - (Vector2)transform.position;
            SetAimAngle(aimVector);
        }

        private void AngleFromGamepad(InputAction.CallbackContext context)
        {
            SetAimAngle(gamepadAimInput.ReadValue<Vector2>());
        }

        private void SetAimAngle(Vector2 _aimVector)
        {
            currentAimAngle = Vector2.SignedAngle(Vector2.up, _aimVector);
        }
        #endregion
    }
}