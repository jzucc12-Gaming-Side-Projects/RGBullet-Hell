using JZ.AUDIO;
using UnityEngine;

namespace CBH.CORE
{
    /// <summary>
    /// Used to set the starting song based off the game settings
    /// </summary>
    public class StartingSong : MonoBehaviour
    {
        private SoundPlayer musicPlayer = null;


        private void Awake()
        {
            musicPlayer = GetComponent<SoundPlayer>();
        }

        private void Start()
        {
            int songNumber = PlayerPrefs.GetInt(PlayerPrefKeys.startingSong, 0);
            AudioClip clip = Resources.Load<AudioClip>($"Music/Song 0{songNumber+1}");
            musicPlayer.ChangeClip("Background Track", clip);
            musicPlayer.Play("Background Track");
        }
    }
}
