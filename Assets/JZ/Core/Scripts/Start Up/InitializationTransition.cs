using UnityEngine;
using UnityEngine.UI;

namespace JZ.CORE.STARTUP
{
    /// <summary>
    /// Transitions out of the initialization screen on game builds only
    /// </summary>
    public class InitializationTransition : MonoBehaviour
    {
        [SerializeField] private Button transitionButton = null;

        #if !UNITY_EDITOR
        private void Start()
        {
            transitionButton.onClick?.Invoke();
        }
        #endif
    }
}