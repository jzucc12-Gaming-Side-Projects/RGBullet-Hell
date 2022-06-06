using System;
using JZ.CORE;
using TMPro;
using UnityEngine;

namespace CBH.TIMER
{
    /// <summary>
    /// Timer for infinite mode
    /// </summary>
    public class InfiniteTimer : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI display = null;
        private float currentTime = 0;
        public event Action newBestTime;
        private string bestTimeKey => GameSettings.inversion ? PlayerPrefKeys.bestTimeInverted : PlayerPrefKeys.bestTime;


        private void OnEnable()
        {
            EndGame.OnEndGame += SaveTime;
        }

        private void OnDisable()
        {
            EndGame.OnEndGame -= SaveTime;
        }

        private void Update()
        {
            currentTime = Mathf.Min(9999.99f, currentTime + Time.deltaTime);
            display.text = $"Current: {currentTime.ToString("F2")}s";
        }

        public float GetCurrentTime()
        {
            return currentTime;
        }

        private void SaveTime()
        {
            float best = PlayerPrefs.GetFloat(bestTimeKey, 0);
            if(currentTime > best)
            {
                PlayerPrefs.SetFloat(bestTimeKey, currentTime);
                newBestTime?.Invoke();
            }
        }
    }
}