using UnityEngine;

namespace JZ.SCENE
{
    /// <summary>
    /// Keeps track of the currently active scene type
    /// </summary>
    public class ActiveSceneType : MonoBehaviour
    {
        [SerializeField] private SceneType mySceneType = SceneType.menu;
        private static SceneType lastType = SceneType.menu;
        private static SceneType activeType = SceneType.menu;

        private void Awake()
        {
            lastType = activeType;
            activeType = mySceneType;
        }

        public static SceneType GetActiveSceneType()
        {
            return activeType;
        }

        public static SceneType GetLastType()
        {
            return lastType;
        }

        public static bool DidTypeChange()
        {
            return lastType != activeType;
        }
    }
}