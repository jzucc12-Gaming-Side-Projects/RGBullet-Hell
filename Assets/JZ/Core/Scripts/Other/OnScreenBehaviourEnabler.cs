using UnityEngine;

namespace JZ.CORE
{
    /// <summary>
    /// Disables and enables behaviours when moving on and off screen
    /// </summary>
    public class OnScreenBehaviourEnabler : MonoBehaviour
    {
        #region //Cached components
        [SerializeField] private Behaviour[] behavioursToEnable = new Behaviour[0];
        private Renderer[] renderers = new Renderer[0];
        #endregion


        #region //Monobehaviour
        private void Awake()
        {
            renderers = GetComponentsInChildren<Renderer>();
        }

        private void Update()
        {
            bool visible = IsVisible();
            if(behavioursToEnable[0].enabled != visible)
            {
                EnableBehaviours(visible);
            }
        }
        #endregion

        #region //Visibility
        private bool IsVisible()
        {
            foreach (var spriteRenderer in renderers)
            {
                if (!spriteRenderer.isVisible) continue;
                return true;
            }
            return false;
        }
        #endregion

        #region //Enabling
        public void EnableBehaviours(bool _enable)
        {
            foreach (var behaviour in behavioursToEnable)
                behaviour.enabled = _enable;
        }
        #endregion
    }
}