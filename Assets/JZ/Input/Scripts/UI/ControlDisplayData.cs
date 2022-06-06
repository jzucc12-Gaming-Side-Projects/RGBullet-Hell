using System;
using UnityEngine;

namespace JZ.INPUT.UI
{
    /// <summary>
    /// Information regarding how UI should display a given control
    /// </summary>
    [Serializable]
    public struct ControlDisplayData
    {
        public string controlName;
        public Color controlColor;
        public Sprite controlSprite;
        public int fontSize;

        public ControlDisplayData(int fontSize, Color controlColor, string controlName, Sprite controlSprite)
        {
            this.fontSize = fontSize;
            this.controlColor = controlColor;
            this.controlName = controlName;
            this.controlSprite = controlSprite;
        }

        public ControlDisplayData(int fontSize, Color controlColor, string controlName)
        {
            this.fontSize = fontSize;
            this.controlColor = controlColor;
            this.controlName = controlName;
            this.controlSprite = null;
        }

        public ControlDisplayData(int fontSize, Color controlColor, Sprite controlSprite)
        {
            this.fontSize = fontSize;
            this.controlColor = controlColor;
            this.controlName = "";
            this.controlSprite = controlSprite;
        }

        public ControlDisplayData(int fontSize, Color controlColor)
        {
            this.fontSize = fontSize;
            this.controlColor = controlColor;
            this.controlName = "";
            this.controlSprite = null;
        }
    }
}