using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace JZ.INPUT.REBIND
{
    /// <summary>
    /// Rebinds controls
    /// </summary>
    public class RebindManager : MonoBehaviour
    {
        #region //Events
        public static event Action<InputAction> RebindingStarted;
        public static event Action<RebindData> RebindingComplete;
        public static event Action RebindingFinished;
        #endregion
        

        #region //Monobehaviour
        private void Start()
        {
            LoadRebindings();
        }
        #endregion

        #region //Rebinding
        //Public
        public void Rebind(InputAction _action, int _index)
        {
            if(_action == null || _index < 0) return;
            RebindingStarted?.Invoke(_action);

            //Set up rebind action
            var rebind = _action.PerformInteractiveRebinding(_index).
                                 WithMatchingEventsBeingSuppressed(true).
                                 WithMagnitudeHavingToBeGreaterThan(0.99f);

            RebindData data;
            data.oldBinding = _action.bindings[_index];
            data.bindingIndex = _index;
            
            //Saves rebind and updates UI
            rebind.OnComplete(op => 
            {
                data.action = op.action;
                data.newBinding = op.action.bindings[_index];
                RebindingComplete?.Invoke(data);
                RebindingFinished?.Invoke();
                op.Dispose();
                SaveRebind(op.action);
            });

            //Stops rebind
            rebind.OnCancel(op => 
            {
                RebindingFinished?.Invoke();
                op.Dispose();
            });

            //Input type specific restrictions
            if(_action.bindings[_index].groups.Contains("Keyboard"))
                SetRebindForPC(rebind);
            else
                SetRebindForGamepad(rebind);

            rebind.Start();
        }

        //Private
        private void SetRebindForGamepad(InputActionRebindingExtensions.RebindingOperation _rebind)
        {
            _rebind.
            WithControlsExcluding("<Gamepad>/dpad").
            WithControlsExcluding("<Gamepad>/leftStick").
            WithControlsExcluding("<Gamepad>/rightStick").
            WithControlsExcluding("Mouse").
            WithControlsExcluding("Keyboard").
            WithCancelingThrough("<Gamepad>/start");
        }

        private void SetRebindForPC(InputActionRebindingExtensions.RebindingOperation _rebind)
        {
            _rebind.
            WithControlsExcluding("<Keyboard>/leftArrow").
            WithControlsExcluding("<Keyboard>/rightArrow").
            WithControlsExcluding("<Keyboard>/upArrow").
            WithControlsExcluding("<Keyboard>/downArrow").
            WithControlsExcluding("<Keyboard>/anyKey").
            WithControlsExcluding("Gamepad").
            WithCancelingThrough("<Keyboard>/escape");
        }
        #endregion

        #region //Rebinding states
        //Public
        public void SaveRebind(InputAction action)
        {
            for(int ii = 0; ii < action.bindings.Count; ii++)
            {
                PlayerPrefs.SetString($"{action.actionMap} {action.name} {ii}", action.bindings[ii].overridePath);
            }
        }

        //Private
        private void LoadRebindings()
        {
            //Get all assets from the input manager
            foreach(var asset in InputManager.GetAssets())
            {
                //Cycle through all action maps
                foreach(var map in asset.actionMaps)
                {
                    //Cycle through all actions
                    foreach(var action in map.actions)
                    {
                        //Cycle through each binding
                        int count = 0;
                        for(int ii = 0; ii < action.bindings.Count; ii++)
                        {
                            string overridePath = PlayerPrefs.GetString($"{action.actionMap} {action.name} {ii}");
                            if(string.IsNullOrEmpty(overridePath)) continue;
                            action.ApplyBindingOverride(ii, overridePath);
                            count++;
                        }
                    }
                }
            }
        }
        #endregion
    }
}
