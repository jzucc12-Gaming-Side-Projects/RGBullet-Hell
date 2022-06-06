using UnityEngine;

namespace JZ.CORE
{
    /// <summary>
    /// Pop up window only available in dev mode
    /// </summary>
    public class DevModeWindow : MonoBehaviour
    {
        [SerializeField] KeyCode[] keyCodes = new KeyCode[0];
        [SerializeField] GameObject devModeWindow = null;

        private void Awake()
        {
            enabled = GameSettings.InDevMode();
        }

        private void Update()
        {
            if(JZ.INPUT.Utils.CheckKeyCombo(keyCodes))
            {
                devModeWindow.SetActive(!devModeWindow.activeInHierarchy);
            }
        }
    }
}
