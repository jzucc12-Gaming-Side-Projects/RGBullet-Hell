using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace CBH.PLAYER.UI
{
    /// <summary>
    /// UI for an individual health type
    /// </summary>
    public class PlayerHealthEntryUI : MonoBehaviour
    {
        [SerializeField] private Image background = null;
        [SerializeField] private Sprite defaultBackground = null;
        
        [Tooltip("Background sprite for square in monochromacy mode")] 
        [SerializeField] private Sprite squareMonoBackground = null;
        private Image[] healthBars = new Image[0];
        private ColorModeUser colors = new ColorModeUser();


        #region //Monobehaviour
        private void Awake()
        {
            var images = GetComponentsInChildren<Image>();
            healthBars = images.Where(x => x.gameObject != gameObject).ToArray();
        }

        private void OnEnable()
        {
            GameSettings.OnColorModeChanged += SetColors;
        }

        private void OnDisable()
        {
            GameSettings.OnColorModeChanged -= SetColors;
        }
        #endregion

        #region //Setting entries
        //Public
        public void SetupEntry(int _count, ColorModeUser _colors)
        {
            colors = _colors;
            SetColors();
            ChangeHealth(_count);
        }

        public void ChangeHealth(int _newHealth)
        {
            for(int ii = 1; ii <= healthBars.Length; ii++)
            {
                if(ii <= _newHealth)
                    healthBars[healthBars.Length - ii].enabled = true;
                else
                    healthBars[healthBars.Length - ii].enabled = false;
            }
        }

        //Private
        private void SetColors()
        {
            foreach(var healthBar in healthBars)
                healthBar.color = colors.GetActiveColor();

            //Swap squares background to white on monochromancy
            if(GameSettings.GetColorMode() != ColorMode.monochromacy ||
               colors.GetColor(ColorMode.standard).b != 1)
            {
                background.sprite = defaultBackground;
            }
            else
            {
                background.sprite = squareMonoBackground;
            }
            #endregion
        }
    }
}
