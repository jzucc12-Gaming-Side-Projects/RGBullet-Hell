using UnityEngine;
using UnityEngine.InputSystem;

namespace  JZ.INPUT
{
    /// <summary>
    /// Inputs for cutscenes
    /// </summary>
    public class CutsceneInputSystem : MyInputSystem<CutsceneInputs>
    {
        #region //Input actions
        private InputAction skipSceneAction = null;
        private InputAction nextLineAction = null;
        #endregion

        #region //Inputs
        public float startHoldTime { get; private set; }
        public bool nextLineInput { get; private set; }
        #endregion


        #region //Constructors
        public CutsceneInputSystem(CutsceneInputs _inputs) : base(_inputs) 
        { 
            skipSceneAction = _inputs.Map.SkipScene;
            nextLineAction = _inputs.Map.NextLine;
        }
        #endregion

        #region //Set Up
        //Public
        public override void ReinitializeInputs()
        {
            startHoldTime = -1;
            nextLineInput = false;
        }

        //Protected
        protected override void SubscribeEvents()
        {
            skipSceneAction.started += StartSkipSceneHold;
            skipSceneAction.canceled += StopSkipSceneHold;
            nextLineAction.started += OnNextLineInput;
        }

        protected override void UnsubscribeEvents()
        {
            skipSceneAction.started -= StartSkipSceneHold;
            skipSceneAction.canceled -= StopSkipSceneHold;
            nextLineAction.started -= OnNextLineInput;
        }

        protected override void EnableActions()
        {
            skipSceneAction.Enable();
            nextLineAction.Enable();
        }

        protected override void DisableActions()
        {
            skipSceneAction.Disable();
            nextLineAction.Disable();
        }
        #endregion

        #region //Callbacks
        private void StartSkipSceneHold(InputAction.CallbackContext _context)
        {
            startHoldTime = (float)_context.startTime;
        }

        private void StopSkipSceneHold(InputAction.CallbackContext _context)
        {
            startHoldTime = -1;
        }
        private void OnNextLineInput(InputAction.CallbackContext _context)
        {
            nextLineInput = true;
        }
        #endregion

        #region //Input State
        public float GetHoldTime() => Time.timeSinceLevelLoad - startHoldTime;
        public bool GetIsHolding() => startHoldTime != -1;
        public bool GetNextLineInput() => nextLineInput;
        #endregion

        #region //Expend Inputs
        public void ExpendNextLineInput()
        {
            nextLineInput = false;
        }

        public void ExpendSkipSceneHold()
        {
            startHoldTime = -1;
        }
        #endregion
    }
}