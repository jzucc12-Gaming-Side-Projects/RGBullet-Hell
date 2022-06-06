using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace JZ.INPUT.REBIND
{
    /// <summary>
    /// Controls with this script can be rebinded
    /// </summary>
    public class RebindableControl : MonoBehaviour
    {
        #region //Input info
        private InputAction onInput = null;
        private IControlInfo controlInfo = null;
        private InputAction action = null;
        private int bindingIndex = 0;
        private bool listening = true;
        private RebindManager rebindManager = null;
        #endregion


        #region //Monobehaviour
        private void Awake()
        {
            onInput = new InputAction(binding: "/*/<button>");
            rebindManager = FindObjectOfType<RebindManager>();
        }

        private void OnEnable()
        {
            RebindManager.RebindingStarted += TurnOffListening;
            RebindManager.RebindingFinished += TurnOnListening;
            RebindManager.RebindingComplete += CheckRebindSwap;
            listening = true;
            onInput.performed += TryRebind;
            onInput.Enable();
        }

        private void OnDisable()
        {
            RebindManager.RebindingStarted -= TurnOffListening;
            RebindManager.RebindingFinished -= TurnOnListening;
            RebindManager.RebindingComplete -= CheckRebindSwap;
            listening = false;
            onInput.performed -= TryRebind;
            onInput.Disable();
        }

        private void Start()
        {
            controlInfo = GetComponent<IControlInfo>();
            action = controlInfo.GetAction();
            bindingIndex = controlInfo.GetBindingIndex();
            enabled = false;
        }
        #endregion

        #region //Rebinding
        private void TryRebind(InputAction.CallbackContext context)
        {
            if(!listening) return;
            
            //Abort if there is not enough actuation
            //Needed because gamepad triggers are weird
            if(context.control.device is Gamepad && context.ReadValue<float>() < 1f) return;

            //Abort if this action does not contain this binding
            var binding = action.GetBindingForControl(context.control);
            if(binding == null) return;

            //Abort if this binding does not apply to this specific control
            var bind = (InputBinding)binding;
            if(bind.id != controlInfo.GetBinding().id) return;
            rebindManager.Rebind(action, bindingIndex);
        }

        //Swaps the binding of this control if it conflicts with a rebind that just took place
        private void CheckRebindSwap(RebindData _data)
        {
            //Abort this is the binding that was rebinded
            if(_data.action == action && _data.bindingIndex == bindingIndex) return;

            //Abort if this action does not match the newly rebinded path
            if(_data.newBinding.overridePath != controlInfo.GetBinding().effectivePath) return;

            //Swap paths
            action.ApplyBindingOverride(bindingIndex, _data.oldBinding.effectivePath);
        }
        #endregion
    
        #region //Turning on and off reading rebinds
        private void TurnOffListening(InputAction _action)
        {
            listening = false;
        }

        private void TurnOnListening()
        {
            StartCoroutine(TurnOnDelay());
        }

        private IEnumerator TurnOnDelay()
        {
            //this ensures double presses from controls can't occur
            for(int ii = 0; ii < 5; ii++)
            {
                yield return null;
            }
            listening = true;
        }
        #endregion
    }
}