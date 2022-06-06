using TMPro;
using UnityEngine;

namespace CBH.TIMER
{
    /// <summary>
    /// Updates timer display for either
    /// current or best times
    /// </summary>
    public class TimeDisplay : MonoBehaviour
    {
        private enum TimeType
        {
            current = 0,
            best = 1
        }
        [SerializeField] private TimeType myType = TimeType.current;
        [SerializeField] private TextMeshProUGUI display = null;
        private InfiniteTimer infiniteTimer = null;
        private float timeToDisplay = 0f;
        private string bestTimeKey => GameSettings.inversion ? PlayerPrefKeys.bestTimeInverted : PlayerPrefKeys.bestTime;


        private void Awake()
        {
            infiniteTimer = FindObjectOfType<InfiniteTimer>();
        }

        private void OnEnable()
        {
            SetDisplay();
            infiniteTimer.newBestTime += SetDisplay;
        }

        private void OnDisable()
        {
            infiniteTimer.newBestTime -= SetDisplay;
        }

        private void SetDisplay()
        {
            if (myType == TimeType.current)
            {
                timeToDisplay = infiniteTimer.GetCurrentTime();
                display.text = $"Current: {timeToDisplay.ToString("F2")}s";
            }
            else
            {
                timeToDisplay = PlayerPrefs.GetFloat(bestTimeKey, 0f);
                display.text = $"Best: {timeToDisplay.ToString("F2")}s";
            }
        }
    }
}
