using JZ.BUTTONS.FUNCTION;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace JZ.SCENE.BUTTON
{
    /// <summary>
    /// Parent function for all scene changing button types
    /// </summary>
    public abstract class SceneButtonFunction : ButtonFunction
    {
        private SceneTransitioner sceneTransitioner => FindObjectOfType<SceneTransitioner>();
        [SerializeField] protected SceneTransitionData transitionData;
        private bool animateTransition => transitionData.animationType != AnimType.none;
        private int currentIndex => SceneManager.GetActiveScene().buildIndex;


        protected override void Awake()
        {
            base.Awake();
            transitionData.activeSceneIndex = currentIndex;
        }

        protected override void Start()
        {
            base.Start();
            if(GameSettings.inversion)
            {
                for(int ii = 0; ii < transitionData.scenesToUnload.Length; ii++)
                {
                    if(!transitionData.scenesToUnload[ii].Contains("Enemy")) continue;
                    transitionData.scenesToUnload[ii] += " Inverted";
                }
            }

            if(GameSettings.timed)
            {
                for(int ii = 0; ii < transitionData.scenesToUnload.Length; ii++)
                {
                    if(!transitionData.scenesToUnload[ii].Contains("Enemy")) continue;
                    transitionData.scenesToUnload[ii] += " Infinite";
                }
            }
        }

        #region //Loading
        public override void OnClick()
        {
            if(animateTransition)
            {
                sceneTransitioner.TransitionToScene(transitionData);
            }
            else
            {
                NonTransitionLoad();
            }
        }

        private void NonTransitionLoad()
        {
            if(!transitionData.additiveLoad) 
                SceneManager.LoadScene(transitionData.sceneIndex);
            else
            {
                foreach(var scene in transitionData.scenesToUnload)
                {
                    SceneManager.UnloadSceneAsync(scene);
                }

                SceneManager.LoadSceneAsync(transitionData.sceneIndex, LoadSceneMode.Additive);
            }
        }
        #endregion
    }
}