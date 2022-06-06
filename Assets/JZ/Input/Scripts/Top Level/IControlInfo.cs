using UnityEngine.InputSystem;

namespace JZ.INPUT
{
    /// <summary>
    /// Information relating to a specific player control
    /// </summary>
    public interface IControlInfo
    {
        InputActionAsset GetAsset();
        InputAction GetAction();
        InputBinding GetBinding();
        int GetBindingIndex();
    }
}
