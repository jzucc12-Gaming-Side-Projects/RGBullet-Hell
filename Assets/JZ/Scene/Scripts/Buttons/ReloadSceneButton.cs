namespace JZ.SCENE.BUTTON
{
    /// <summary>
    /// Reloads the current scene on press
    /// </summary>
    public class ReloadSceneButton : SceneButtonFunction
    {
        protected override void Awake()
        {
            base.Awake();
            transitionData.sceneIndex = transitionData.activeSceneIndex;
        }
    }
}