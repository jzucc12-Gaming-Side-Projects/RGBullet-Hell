using UnityEngine;

/// <summary>
/// Settings for colorblindness
/// </summary>
public enum ColorMode
{
    standard = 0,
    protanopia = 1,
    dueteranopia = 2,
    tritanopia = 3,
    monochromacy = 4,
    custom = 5
}

/// <summary>
/// Grabs proper colors relating to current color settings
/// </summary>
[System.Serializable]
public class ColorModeUser
{
    [SerializeField] private Color standardColor = Color.white;
    [SerializeField] private Color protanopiaColor = Color.white;
    [SerializeField] private Color dueteranopiaColor = Color.white;
    [SerializeField] private Color tritanopiaColor = Color.white;
    [SerializeField] private Color monochromacyColor = Color.white;
    [SerializeField] private string customColorKey = "";
    private string defaultKey => $"{customColorKey}Default";


    #region //Getting colors
    //Public
    public Color GetActiveColor()
    {
        return GetColor(GameSettings.GetColorMode());
    }

    public Color GetColor(ColorMode _mode)
    {
        if(_mode == ColorMode.custom)
        {
            return GetCustomColor();
        }
        else
        {
            return ColorArray()[(int)_mode];
        }
    }

    //Private
    private Color[] ColorArray() 
    {
        Color[] colorArray = new Color[5];
        colorArray[0] = standardColor;
        colorArray[1] = protanopiaColor;
        colorArray[2] = dueteranopiaColor;
        colorArray[3] = tritanopiaColor;
        colorArray[4] = monochromacyColor;
        return colorArray;
    }

    private Color GetCustomColor()
    {
        Color customColor = Color.white;
        string custom = PlayerPrefs.GetString(customColorKey, "");
        if (custom != "") 
        {
            ColorUtility.TryParseHtmlString(custom, out customColor);
        }
        else
        {
            Color defaultColor;
            string defaultString = PlayerPrefs.GetString(defaultKey, "");
            ColorUtility.TryParseHtmlString(defaultString, out defaultColor);
            return defaultColor;
        }
        return customColor;
    }
    #endregion
}