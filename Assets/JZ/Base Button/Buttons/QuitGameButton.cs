using UnityEngine;

namespace JZ.BUTTONS.FUNCTION
{
    /// <summary>
    /// Closes the application
    /// Not available in web builds
    /// </summary>
    public class QuitGameButton : ButtonFunction
    {
        protected override void Awake()
        {
            #if UNITY_WEBGL
            gameObject.SetActive(false);
            #else
            base.Awake();
            #endif
        }

        public override void OnClick()
        {
            Application.Quit();
        }
    }
}