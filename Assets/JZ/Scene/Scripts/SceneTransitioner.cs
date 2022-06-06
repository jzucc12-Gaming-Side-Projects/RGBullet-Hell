using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace JZ.SCENE
{
    /// <summary>
    /// Loads and unloads scenes fed to the transitioner
    /// </summary>
    public class SceneTransitioner : MonoBehaviour
    {
        #region //Animation Variables
        private TransitionAnimation[] animations = new TransitionAnimation[0];
        private List<Animator> animators = new List<Animator>();
        private Animator currentAnimator = null;
        #endregion

        #region //Transition Variables
        public static event Action StartTransitionOut;
        public static event Action StartTransitionIn;
        float preLoadBuffer = 0.35f;
        float postLoadBuffer = 0.35f;
        private bool transitioning = false;
        #endregion


        #region //Monobehaviour
        private void Awake() 
        {
            animations = GetComponentsInChildren<TransitionAnimation>(); 
            foreach(var animation in animations)
                animators.Add(animation.GetComponent<Animator>());
        }

        private void OnEnable() 
        {
            foreach(var animation in animations)
                animation.OnTransitionFinished += TransitionFinished;
        }

        private void OnDisable() 
        {
            foreach(var animation in animations)
                animation.OnTransitionFinished -= TransitionFinished;
        }
        #endregion

        #region //Transitions
        //Public
        public bool IsTransitioning()
        {
            return transitioning;
        }

        public void TransitionToScene(SceneTransitionData _data)
        {
            if(IsTransitioning()) return;
            currentAnimator = animators[(int)_data.animationType];
            StartCoroutine(TransitionCoroutine(_data));
        }

        //Private
        private IEnumerator TransitionCoroutine(SceneTransitionData _data)
        {
            //Transition out
            transitioning = true;
            currentAnimator.SetTrigger("TransitionOut");
            StartTransitionOut?.Invoke();

            //Transition
            while(currentAnimator.GetCurrentAnimatorClipInfoCount(0) == 0) yield return null;
            float waitDuration = currentAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.averageDuration;
            yield return new WaitForSecondsRealtime(waitDuration + preLoadBuffer);
            yield return StartCoroutine(LoadNextScene(_data));
            yield return new WaitForSecondsRealtime(postLoadBuffer);

            //Transition in
            currentAnimator.SetTrigger("TransitionIn");
            StartTransitionIn?.Invoke();
            transitioning = false;
            Time.timeScale = 1;
        }

        private IEnumerator LoadNextScene(SceneTransitionData _data)
        {
            if(!_data.additiveLoad)
            {
                yield return SceneManager.LoadSceneAsync(_data.sceneIndex);
            }
            else
            {
                foreach(var scene in _data.scenesToUnload)
                    yield return SceneManager.UnloadSceneAsync(scene);

                yield return SceneManager.LoadSceneAsync(_data.sceneIndex, LoadSceneMode.Additive);
            }
        }

        private void TransitionFinished()
        {
            currentAnimator = null;
        }
        #endregion
    }
}