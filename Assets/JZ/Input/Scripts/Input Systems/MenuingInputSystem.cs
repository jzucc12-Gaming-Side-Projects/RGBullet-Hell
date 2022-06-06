using UnityEngine;
using UnityEngine.InputSystem;

namespace JZ.INPUT
{
    /// <summary>
    /// Inputs for menu navigation
    /// </summary>
    public class MenuingInputSystem : MyInputSystem<MenuingInputs>
    {
        #region//Input actions
        private InputAction menuNavigate = null;
        private InputAction menuSelect = null;
        #endregion

        #region//Inputs
        private int xNav = 0;
        private int yNav = 0;
        private bool selected = false;
        #endregion


        #region//Constructor
        public MenuingInputSystem(MenuingInputs _inputs) : base(_inputs)
        {
            inputs = _inputs;
            menuNavigate = inputs.Menus.Navigate;
            menuSelect = inputs.Menus.Select;
        }
        #endregion

        #region//Set Up
        //Public
        public override void ReinitializeInputs()
        {
            xNav = 0;
            yNav = 0;
            selected = false;
        }

        //Protected
        protected override void SubscribeEvents()
        {
            menuNavigate.started += OnMenuNavigate;
            menuNavigate.canceled += OnMenuNavigate;
            menuSelect.started += OnMenuSelect;
        }

        protected override void UnsubscribeEvents()
        {
            menuNavigate.started -= OnMenuNavigate;
            menuNavigate.canceled -= OnMenuNavigate;
            menuSelect.started -= OnMenuSelect;
        }

        protected override void EnableActions()
        {
            menuNavigate.Enable();
            menuSelect.Enable();
        }

        protected override void DisableActions()
        {
            menuNavigate.Disable();
            menuSelect.Disable();
        }
        #endregion

        #region//Callbacks
        private void OnMenuNavigate(InputAction.CallbackContext context)
        {
            xNav = Mathf.RoundToInt(menuNavigate.ReadValue<Vector2>().x);
            yNav = Mathf.RoundToInt(menuNavigate.ReadValue<Vector2>().y);
        }

        private void OnMenuSelect(InputAction.CallbackContext context)
        {
            selected = true;
        }
        #endregion

        #region //Input State
        public int GetXNav() => xNav;
        public int GetYNav() => yNav;
        public bool XNavPressed() => GetXNav() != 0;
        public bool YNavPressed() => GetYNav() != 0;
        public bool MenuSelectPressed() => selected;
        #endregion

        #region //Expending Inputs
        public void ExpendMenuSelect() { selected = false; }
        public void ExpendXDir() { xNav = 0; }
        public void ExpendYDir() { yNav = 0; }
        public void ExpendAllDir()
        {
            ExpendXDir();
            ExpendYDir();
        }
        #endregion
    }
}