using UnityEngine;
using UnityEngine.UI;

namespace CBH.CORE
{
    /// <summary>
    /// Used to set the game background color according to the color settings
    /// </summary>
    public class BackgroundColor : MonoBehaviour
    {
        [SerializeField] private Camera cam = null;
        [SerializeField] private Image image = null;
        [SerializeField] private ColorModeUser colors = new ColorModeUser();

    
        private void OnValidate()
        {
            Start();
        }

        private void OnEnable()
        {
            GameSettings.OnColorModeChanged += Start;
        }

        private void OnDisable()
        {
            GameSettings.OnColorModeChanged -= Start;
        }

        private void Start()
        {
            if(cam != null) cam.backgroundColor = colors.GetActiveColor();
            if(image != null) image.color = colors.GetActiveColor();
        }
    }
}
