using UnityEngine;

namespace JZ.CORE
{
    /// <summary>
    /// Changes object activation depending upon game build type
    /// </summary>
    public class DeactivateIfBuildType : MonoBehaviour
    {
        [SerializeField] private bool standaloneOnly = true;

        private void Awake()
        {
            bool shouldDeactivate = false;
            #if UNITY_WEBGL
            shouldDeactivate = standaloneOnly;
            #else
            shouldDeactivate = !standaloneOnly;
            #endif

            if(shouldDeactivate)
                gameObject.SetActive(false);
        }
    }
}
