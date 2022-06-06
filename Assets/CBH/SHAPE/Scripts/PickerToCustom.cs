using UnityEngine;

namespace CBH.SHAPE
{
    /// <summary>
    /// Updates custom color setting from the color picker
    /// </summary>
    public class PickerToCustom : MonoBehaviour
    {
        #region //Variables
        [SerializeField] private FlexibleColorPicker picker = null;
        [SerializeField] private string colorKey = "CircleColor";
        private string defaultKey => $"{colorKey}Default";
        #endregion


        #region //Monobehaviour
        private void OnEnable()
        {
            picker.color = GetCurrentColor();
            picker.onColorChange.AddListener(ColorChange);
        }

        private void OnDisable()
        {
            picker.onColorChange.RemoveListener(ColorChange);
        }
        #endregion

        #region //Color changing
        //Public
        public void ChangeKey(string _newKey)
        {
            colorKey = _newKey;

            //Temporarily remove listener to prevent unncessary call
            picker.onColorChange.RemoveListener(ColorChange);
            picker.color = GetCurrentColor();
            picker.onColorChange.AddListener(ColorChange);
        }

        public void ResetColor()
        {
            picker.color = GetDefaultColor();
        }

        //Private
        private void ColorChange(Color _color)
        {
            string custom = $"#{ColorUtility.ToHtmlStringRGBA(_color)}";
            PlayerPrefs.SetString(colorKey, custom);
            GameSettings.SetColorMode(ColorMode.custom);
        }

        private Color GetCurrentColor()
        {
            Color customColor;
            string customString = PlayerPrefs.GetString(colorKey, "");

            if(!string.IsNullOrEmpty(customString))
            {
                ColorUtility.TryParseHtmlString(customString, out customColor);
            }
            else
            {
                customColor = GetDefaultColor();
            }

            return customColor;
        }

        private Color GetDefaultColor()
        {
            Color defaultColor;
            string defaultString = PlayerPrefs.GetString(defaultKey, "");
            ColorUtility.TryParseHtmlString(defaultString, out defaultColor);
            return defaultColor;
        }
        #endregion
    }
}
