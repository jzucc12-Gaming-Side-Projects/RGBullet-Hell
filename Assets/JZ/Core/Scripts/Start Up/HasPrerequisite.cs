using UnityEngine;

namespace JZ.CORE.STARTUP
{
    /// <summary>
    /// Changes object activation if a certain prerequesite is true
    /// </summary>
    public class HasPrerequisite : MonoBehaviour
    {
        [SerializeField] private bool ignoreInDevMode = true;
        [SerializeField] private bool showByDefault = true;

        protected void SetActive(bool _show)
        {
            if (ignoreInDevMode && GameSettings.InDevMode())
                gameObject.SetActive(showByDefault);
            else
                gameObject.SetActive(_show);
        }
    }

}