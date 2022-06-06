using UnityEngine;
using UnityEngine.InputSystem;

namespace JZ.INPUT
{
    /// <summary>
    /// Parent class for specific input system types
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class MyInputSystem<T> where T : IInputActionCollection
    {
        protected T inputs = default(T);
        private bool active = false;

        #region//Constructor
        public MyInputSystem(T _inputs) { }
        #endregion

        #region//Startup shutdown
        public void Activate()
        {
            if (active) return;
            SubscribeEvents();
            EnableActions();
            active = true;
        }

        public void Deactivate()
        {
            if (!active) return;
            UnsubscribeEvents();
            DisableActions();
            active = false;
        }

        public abstract void ReinitializeInputs();
        protected abstract void SubscribeEvents();
        protected abstract void UnsubscribeEvents();
        protected abstract void EnableActions();
        protected abstract void DisableActions();
        #endregion
    }
}