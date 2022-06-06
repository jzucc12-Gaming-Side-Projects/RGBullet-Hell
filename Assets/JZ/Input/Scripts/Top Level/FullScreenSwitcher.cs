using UnityEngine;
using UnityEngine.InputSystem;

namespace JZ.INPUT
{
    /// <summary>
    /// Toggles between fullscreen mode
    /// </summary>
    public class FullScreenSwitcher : MonoBehaviour
    {
        private GeneralInputs inputs = null;
        private void Awake() 
        {
            inputs = InputManager.generalInputs;
        }

        private void OnEnable() 
        {
            inputs.Map.ToggleFullscreen.performed += ToggleFS;  
        }

        private void OnDisable() 
        {
            inputs.Map.ToggleFullscreen.performed -= ToggleFS;  
        }

        private void ToggleFS(InputAction.CallbackContext _context)
        {
            Screen.fullScreen = !Screen.fullScreen;
        }
    }

}