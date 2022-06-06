using System;
using UnityEngine;

namespace JZ.SCENE
{
    /// <summary>
    /// Alerts scene transitioner of finished animations
    /// </summary>
    public class TransitionAnimation : MonoBehaviour
    {
        public event Action OnTransitionFinished;

        public void TransitionFinished()
        {
            OnTransitionFinished?.Invoke();
        }
    }
}
