using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.DualShock;
#if !UNITY_WEBGL
using UnityEngine.InputSystem.Switch;
#endif
using UnityEngine.InputSystem.Users;

namespace JZ.INPUT
{
    /// <summary>
    /// Keeps track of currently used player input device
    /// </summary>
    public class DeviceChecker : MonoBehaviour
    {
        [SerializeField] private PlayerInput playerInput = null;
        public static string currentScheme;
        public static bool isUsingGamepad => currentScheme == "Gamepad";
        private static GamepadType lastType = GamepadType.xbox;


        #region //Monobehaviour
        private void OnEnable()
        {
            InputUser.onChange += OnUserChange;
            InputSystem.onDeviceChange += NewDevice;
        }

        private void OnDisable()
        {
            InputUser.onChange -= OnUserChange;
            InputSystem.onDeviceChange -= NewDevice;
        }

        private void Start()
        {
            currentScheme = playerInput.currentControlScheme;

            if(Gamepad.all.Count > 0)
                SetGamepad(Gamepad.all[0]);
        }
        #endregion

        public static GamepadType GetCurrentGamepadType()
        {
            if (!isUsingGamepad)
                return lastType;

            SetGamepad(Gamepad.current);

            return lastType;
        }

        private static void SetGamepad(Gamepad _pad)
        {
            if (_pad is DualShockGamepad)
                lastType = GamepadType.sony;
            #if !UNITY_WEBGL
            else if (_pad is SwitchProControllerHID)
                lastType = GamepadType.nSwitch;
            #endif
            else
                lastType = GamepadType.xbox;
        }

        private void OnUserChange(InputUser user, InputUserChange change, InputDevice dvc)
        {
            if (change == InputUserChange.ControlSchemeChanged)
            {
                currentScheme = playerInput.currentControlScheme;
            }
        }

        private void NewDevice(InputDevice _device, InputDeviceChange _change)
        {
            switch(_change)
            {
                case InputDeviceChange.Added:
                    if(_device is Gamepad)
                        SetGamepad((Gamepad)_device);
                        break;
                default:
                    return;
            }
        }
    }
}

