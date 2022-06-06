using UnityEngine;

namespace JZ.CORE.STARTUP
{
    /// <summary>
    /// Changes object activation in relation to dev mode
    /// </summary>
    public class DevModeCheck : MonoBehaviour
    {
        [SerializeField] private bool matchDev = true;

        private void Awake() 
        {
            gameObject.SetActive(!(GameSettings.InDevMode() ^ matchDev));    
        }
    }
}