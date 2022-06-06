using UnityEngine;

namespace JZ.CORE.STARTUP
{
    /// <summary>
    /// Changes object activation depending on player pref values
    /// </summary>
    public class PlayerPrefIntPrereq : HasPrerequisite
    {
        [SerializeField] private string[] keys = new string[0];
        [SerializeField] private int[] values = new int[0];
        private void Awake()
        {
            bool show = true;
            for(int ii = 0; ii < keys.Length; ii++)
            {
                if(PlayerPrefs.GetInt(keys[ii], 0) == values[ii]) continue;
                show = false;
                break;
            }
            SetActive(show);
        }
    }
}