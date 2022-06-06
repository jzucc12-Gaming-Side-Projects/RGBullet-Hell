namespace JZ.SCENE.BUTTON
{
    /// <summary>
    /// Loads the next scene in the build index on press
    /// </summary>
    public class LoadNextLevelButton : SceneButtonFunction
    {
        protected override void Awake()
        {
            base.Awake();
            transitionData.sceneIndex = gameObject.scene.buildIndex + 1;
        }
    }
}