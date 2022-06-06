using UnityEngine.InputSystem;

namespace JZ.INPUT.REBIND
{
    /// <summary>
    /// Info for a given rebinding event
    /// </summary>
    public struct RebindData
    {
        public InputAction action;
        public InputBinding oldBinding;
        public InputBinding newBinding;
        public int bindingIndex;
    }
}
