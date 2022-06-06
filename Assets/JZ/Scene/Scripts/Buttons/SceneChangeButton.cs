using UnityEngine;

namespace JZ.SCENE.BUTTON
{
    /// <summary>
    /// Changes to player specified scene on press
    /// </summary>
    public class SceneChangeButton : SceneButtonFunction
    {
        [SerializeField] string targetScene;

        protected override void Awake()
        {
            base.Awake();

            if(gameObject.scene.name != "Main Menu" && GameSettings.inversion)
            {
                if(targetScene.Contains("Enemy"))
                    targetScene += " Inverted";
            }

            if(gameObject.scene.name != "Main Menu" && GameSettings.timed)
            {
                if(targetScene.Contains("Enemy"))
                    targetScene += " Infinite";
            }

            transitionData.sceneIndex = JZ.SCENE.Utils.GetSceneIndexFromName(targetScene);
        }
    }
}