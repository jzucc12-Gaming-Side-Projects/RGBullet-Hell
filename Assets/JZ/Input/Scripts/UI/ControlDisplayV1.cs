using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace JZ.INPUT.UI
{
    /// <summary>
    /// Old, static method for displaying a control
    /// </summary>
    public class ControlDisplayV1 : MonoBehaviour
    {
        #region//Cached variables
        [SerializeField] private Image controlImage = null;
        [SerializeField] private TextMeshProUGUI controlText = null;
        [SerializeField] private bool liveUpdate = false;
        #endregion

        #region//Other
        public bool isController = false;
        private GamepadType deviceType = GamepadType.xbox;
        #endregion

        #region//Controls
        [SerializeField] private ControlDisplayData computerControl = new ControlDisplayData();
        [SerializeField] private ControlDisplayData xboxControl = new ControlDisplayData();
        [SerializeField] private ControlDisplayData sonyControl = new ControlDisplayData();
        [SerializeField] private ControlDisplayData switchControl = new ControlDisplayData();
        #endregion


        #region//Monobehaviour
        private void OnEnable() 
        {
            deviceType = DeviceChecker.GetCurrentGamepadType();
            UpdateControl();
        }

        private void LateUpdate()
        {
            if(liveUpdate && isController != DeviceChecker.isUsingGamepad)
            {
                isController = DeviceChecker.isUsingGamepad;
                deviceType = DeviceChecker.GetCurrentGamepadType();
                UpdateControl();
            }

            if (isController && deviceType != DeviceChecker.GetCurrentGamepadType())
            {
                deviceType = DeviceChecker.GetCurrentGamepadType();
                UpdateControl();
            }
        }
        #endregion

        #region//Update display
        public void ChangeControl(GamepadType _type, ControlDisplayData _control)
        {
            switch (_type)
            {
                case GamepadType.sony:
                    sonyControl = _control;
                    break;
                case GamepadType.nSwitch:
                    switchControl = _control;
                    break;
                case GamepadType.xbox:
                    xboxControl = _control;
                    break;
                case GamepadType.none:
                default:
                    computerControl = _control;
                    break;
            }
            UpdateControl();
        }
        public void UpdateControl()
        {
            Hide();

            if (!isController)
                UpdateDisplay(computerControl);
            else
            {
                switch (DeviceChecker.GetCurrentGamepadType())
                {
                    case GamepadType.sony:
                        UpdateDisplay(sonyControl);
                        break;
                    case GamepadType.nSwitch:
                        UpdateDisplay(switchControl);
                        break;
                    case GamepadType.xbox:
                    case GamepadType.none:
                    default:
                        UpdateDisplay(xboxControl);
                        break;
                }
            }
        }

        private void UpdateDisplay(ControlDisplayData _control)
        {
            if(!string.IsNullOrEmpty(_control.controlName))
                ShowText(_control);
            else if (_control.controlSprite != null)
                ShowImage(_control);
            else
                Hide();
        }

        private void ShowImage(ControlDisplayData _control)
        {
            controlImage.color = _control.controlColor;
            controlImage.sprite = _control.controlSprite;
            controlImage.gameObject.SetActive(true);
            controlImage.enabled = true;
        }

        private void ShowText(ControlDisplayData _control)
        {
            controlText.gameObject.SetActive(true);
            controlText.text = _control.controlName;
            controlText.color = _control.controlColor;
            controlText.fontSize = _control.fontSize;
        }

        public void Hide()
        {
            controlImage.gameObject.SetActive(false);
            controlText.gameObject.SetActive(false);
        }
        #endregion
    }
}