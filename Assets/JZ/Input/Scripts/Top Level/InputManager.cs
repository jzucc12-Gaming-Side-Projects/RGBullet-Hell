using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace JZ.INPUT
{
    /// <summary>
    /// Holds references to all available player inputs
    /// </summary>
    public class InputManager : MonoBehaviour
    {
        #region //Input Action Assets
        public static PlayerInputActions playerInputs = null;
        public static GeneralInputs generalInputs = null;
        private static Dictionary<string, InputActionAsset> actionAssets = new Dictionary<string, InputActionAsset>();
        #endregion


        #region //Monobehaviour
        public void Awake()
        {
            playerInputs = new PlayerInputActions();
            actionAssets.Add(playerInputs.asset.name, playerInputs.asset);

            generalInputs = new GeneralInputs();
            actionAssets.Add(generalInputs.asset.name, generalInputs.asset);
        }

        private void OnEnable()
        {
            generalInputs.Map.Pause.Enable();
            generalInputs.Map.ToggleFullscreen.Enable();  
        }

        private void OnDisable()
        {
            generalInputs.Map.Pause.Disable();
            generalInputs.Map.ToggleFullscreen.Disable();    
        }
        #endregion

        #region //Getters
        public static InputActionAsset GetAsset(string _assetName)
        {
            return actionAssets[_assetName];
        }

        public static IEnumerable<InputActionAsset> GetAssets()
        {
            foreach(var entry in actionAssets.Values)
                yield return entry;
        }
        #endregion
    }
}