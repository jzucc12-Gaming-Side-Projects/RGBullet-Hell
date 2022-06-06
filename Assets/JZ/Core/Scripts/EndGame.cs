using System;
using UnityEngine;

namespace JZ.CORE
{
    /// <summary>
    /// Base class for various end game types
    /// </summary>
    public abstract class EndGame : MonoBehaviour
    {
        [SerializeField] private GameObject endGameMenu = null;
        [SerializeField] private GamePause gamePause = null;
        public static event Action OnEndGame;


        protected void InitiateEndGame()
        {
            endGameMenu.SetActive(true);
            Time.timeScale = 0;
            OnEndGame?.Invoke();
            gamePause.enabled = false;
        }
    }
}
