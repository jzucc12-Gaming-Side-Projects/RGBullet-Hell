using UnityEngine;
using UnityEngine.UI;

namespace JZ.INPUT
{
    /// <summary>
    /// Press specified button on any input
    /// </summary>
    public class PressAnyToContinue : MonoBehaviour
    {
        [SerializeField] private Button button = null;
        void Update()
        {
            if(JZ.INPUT.Utils.AnyKeyOrButton())
            {
                button.onClick?.Invoke();
                enabled = false;
            }
        }
    }
}
