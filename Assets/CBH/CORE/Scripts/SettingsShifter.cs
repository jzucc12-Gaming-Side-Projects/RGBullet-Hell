using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JZ.INPUT;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace CBH.CORE
{
    public class SettingsShifter : MonoBehaviour
    {
        private PlayerInputActions playerInputs = null;
        private InputAction shiftInput = null;
        public UnityEvent onNext;
        public UnityEvent onPrevious;


        private void Awake()
        {
            playerInputs = InputManager.playerInputs;
            shiftInput = playerInputs.Player.ShiftWeaponIndex;
        }

        private void OnEnable()
        {
            shiftInput.performed += Shift;
            shiftInput.Enable();
        }

        private void OnDisable()
        {
            shiftInput.performed -= Shift;
            shiftInput.Disable();
        }

        private void Shift(InputAction.CallbackContext context)
        {
            if(context.ReadValue<float>() < 0)
            {
                onPrevious?.Invoke();
            }
            else
            {
                onNext?.Invoke();
            }
        }
    }
}
