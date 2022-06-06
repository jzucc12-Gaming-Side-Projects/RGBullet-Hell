using UnityEngine.SceneManagement;

namespace JZ.SCENE
{
    /// <summary>
    /// Extra scene functions
    /// </summary>
    public static class Utils
    {
        /// <summary>DoWork is a method in the TestClass class.
        /// <para>Determines scene name based off of the scene index</para>
        /// </summary>
        public static string GetSceneNameFromIndex(int _index)
        {
            string path = SceneUtility.GetScenePathByBuildIndex(_index);
            string withExtension = path.Substring(path.LastIndexOf('/') + 1);
            string withoutExtension = withExtension.Substring(0, withExtension.LastIndexOf('.'));
            return withoutExtension;
        }

        /// <summary>DoWork is a method in the TestClass class.
        /// <para>Determines scene index based off of the scene name</para>
        /// </summary>
        public static int GetSceneIndexFromName(string _name)
        {
            for(int ii = 0; ii < SceneManager.sceneCountInBuildSettings; ii++)
            {
                string sceneName = GetSceneNameFromIndex(ii);
                if(sceneName != _name) continue;
                return ii;
            }

            return -1;
        }
    }
}