using UnityEngine;
using UnityEngine.UI;

namespace CBH.SHAPE
{
    /// <summary>
    /// Displays shape type color appropriate to the current color mode setting
    /// </summary>
    public class ColorDisplayer : MonoBehaviour
    {
        [SerializeField] private ShapeTypeSO shapeType = null;
        [SerializeField] private Image myImage = null;


        private void OnValidate()
        {
            if(shapeType == null) return;
            Start();
        }

        private void OnEnable()
        {
            GameSettings.OnColorModeChanged += ChangeColor;
            ChangeColor();
        }

        private void OnDisable()
        {
            GameSettings.OnColorModeChanged -= ChangeColor;
        }

        private void Start()
        {
            ChangeColor();
        }

        public void ChangeColor()
        {
            if(myImage.color == Color.clear) return;
            myImage.color = shapeType.GetColor();
        }
    }
}
