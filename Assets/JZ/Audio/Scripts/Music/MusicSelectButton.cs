using JZ.BUTTONS.FUNCTION;
using TMPro;
using UnityEngine;

namespace JZ.AUDIO.MUSIC
{
    public class MusicSelectButton : ButtonFunction
    {
        [SerializeField] private TextMeshProUGUI currentlyPlayingHeader = null;
        [SerializeField] private TextMeshProUGUI trackTitleDisplay = null;
        [SerializeField] string trackTitle = "";
        private BackgroundMusicSetter bgSetter;

        protected override void Awake()
        {
            bgSetter = GetComponent<BackgroundMusicSetter>();
        }

        protected override void Start()
        {
            trackTitleDisplay.text = trackTitle;
            if(bgSetter.IsPlayingMine())
                currentlyPlayingHeader.text = "Currently Playing: " + trackTitle;
        }

        public override void OnClick()
        {
            bgSetter.OverridePlayer(true);
            currentlyPlayingHeader.text = "Currently Playing: " + trackTitle;
        }

        public void ResetToMe()
        {
            bgSetter.OverridePlayer(false);
            currentlyPlayingHeader.text = "Currently Playing: " + trackTitle;
        }
    }
}
