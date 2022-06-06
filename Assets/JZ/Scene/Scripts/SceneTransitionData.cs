using System;
using UnityEngine;

namespace JZ.SCENE
{
    /// <summary>
    /// Information regarding how the scene transitioner should behave
    /// for a given transition
    /// </summary>
    [Serializable]
    public struct SceneTransitionData
    {
        [Tooltip("Set to none for an instant transition")] public AnimType animationType;
        public bool additiveLoad;
        [Tooltip("Only relevant if additive loading.")] public string[] scenesToUnload;
        [HideInInspector] public int sceneIndex;
        [HideInInspector] public int activeSceneIndex;
    }
}
