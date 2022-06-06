using TMPro;
using UnityEngine;

namespace CBH.SHAPE
{
    /// <summary>
    /// Sets title screen text. Necessary because the "R, G, and B" are to match the colors set in the color settings.
    /// </summary>
    public class TitleText : MonoBehaviour
    {
        #region //Variables
        [SerializeField] private TextMeshProUGUI title = null;
        [SerializeField] private ShapeTypeSO shapeR = null;
        [SerializeField] private ShapeTypeSO shapeG = null;
        [SerializeField] private ShapeTypeSO shapeB = null;
        #endregion


        #region //Monobehaviour
        private void OnValidate()
        {
            Start();
        }

        private void OnEnable()
        {
            GameSettings.OnColorModeChanged += Start;
        }

        private void OnDisable()
        {
            GameSettings.OnColorModeChanged -= Start;
            ColorUtility.ToHtmlStringRGB(shapeR.GetColor());
        }

        private void Start()
        {
            string rHex = ColorUtility.ToHtmlStringRGB(shapeR.GetColor());
            string gHex = ColorUtility.ToHtmlStringRGB(shapeG.GetColor());
            string bHex = ColorUtility.ToHtmlStringRGB(shapeB.GetColor());
            title.text = $"<color=#{rHex}>R</color><color=#{gHex}>G</color><color=#{bHex}>B</color>ullet Hell";
        }
        #endregion
    }
}
