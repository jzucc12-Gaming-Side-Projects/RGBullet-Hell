using UnityEngine;

namespace CBH.TIMER
{
    /// <summary>
    /// Displays timer UI in timer mode only
    /// </summary>
    public class TimeBox : MonoBehaviour
    {
        private void Awake()
        {
            if(GameSettings.timed) return;
            gameObject.SetActive(false);
        }
    }
}