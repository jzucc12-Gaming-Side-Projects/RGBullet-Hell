using UnityEngine;

namespace JZ.AUDIO.MUSIC
{
    public class BackgroundMusicPlayer : SoundPlayer
    {
        private static bool isLocked = false;

        public void SetLock(bool _lock) { isLocked = _lock; }

        public void ChangeBackgroundTrack(string _name, AudioClip _clip)
        {
            if(isLocked) return;
            ChangeClip(_name, _clip);
        }
    }
}