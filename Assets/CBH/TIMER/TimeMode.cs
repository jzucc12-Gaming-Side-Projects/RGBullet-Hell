using UnityEngine;

namespace CBH.TIMER
{
    /// <summary>
    /// Sets timed mode
    /// </summary>
    public class TimeMode : MonoBehaviour
    {
        public void SetTimeMode(bool _set)
        {
            GameSettings.timed = _set;
        }
    }
}
