using JZ.INPUT.REBIND;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace JZ.INPUT.UI
{
    /// <summary>
    /// Parent class for control displays that deal with rebindable controls
    /// </summary>
    public abstract class RebindableControlDisplay : MonoBehaviour, IControlInfo
    {
        #region //Display components
        [Header("Display Components")]
        [SerializeField] protected Image myImage = null;
        [SerializeField] protected TextMeshProUGUI myText = null;
        #endregion

        #region //Device type
        [Header("Device Type")]
        [SerializeField, ReadOnly] protected GamepadType deviceType = GamepadType.xbox;
        
        [Tooltip("Set true if you want the display to change with the active input method")] 
        [SerializeField] private bool liveUpdate = false;
        [SerializeField] protected bool isGamepad = false;
        #endregion

        #region //Action Reference
        [Header("Input Action")]
        [SerializeField] protected InputActionReference actionRef = null;
        protected InputActionAsset actionAsset => GetAsset();
        protected InputAction action => GetAction();
        [SerializeField, ReadOnly] private InputBinding bindingInspector = new InputBinding();
        #endregion


        #region //Monobehaviour
        private void OnValidate()
        {
            if(Application.isPlaying) return;
            if(actionRef == null) return;
            Start();
            bindingInspector = GetBinding();
        }

        protected virtual void OnEnable()
        {
            UpdateUI();
            RebindManager.RebindingFinished += UpdateUI;
        }

        protected virtual void OnDisable()
        {
            RebindManager.RebindingFinished -= UpdateUI;
        }

        protected virtual void Start()
        {
            UpdateUI();
        }

        protected virtual void LateUpdate()
        {
            UpdateDeviceType();
        }
        #endregion

        #region //Updating UI
        //Public
        public void UpdateUI()
        {
            UpdateDeviceType();
            UpdateDisplay();
            bindingInspector = GetBinding();
        }

        public void Show()
        {
            foreach(Transform child in transform)
            {
                child.gameObject.SetActive(true);
            }
        }

        public void Hide()
        {
            foreach(Transform child in transform)
            {
                child.gameObject.SetActive(false);
            }
        }

        //Protected
        protected void UpdateDeviceType()
        {
            //Update as input method changes
            if (liveUpdate && isGamepad != DeviceChecker.isUsingGamepad)
            {
                isGamepad = DeviceChecker.isUsingGamepad;
                deviceType = DeviceChecker.GetCurrentGamepadType();
                UpdateDisplay();
            }

            //Update if gamepad type changes
            if (isGamepad && deviceType != DeviceChecker.GetCurrentGamepadType())
            {
                deviceType = DeviceChecker.GetCurrentGamepadType();
                UpdateDisplay();
            }
        }

        protected abstract void UpdateDisplay();
        #endregion

        #region //Rebinding
        public bool EnableRebind(bool _enable)
        {
            var rebind = GetComponent<RebindableControl>();
            if(rebind == null) return false;
            rebind.enabled = _enable;
            return true;
        }
        #endregion

        #region //Control Info
        public abstract InputActionAsset GetAsset();
        public abstract InputAction GetAction();
        public abstract InputBinding GetBinding();
        public abstract int GetBindingIndex();
        #endregion
    }
}
