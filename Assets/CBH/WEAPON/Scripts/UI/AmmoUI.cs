using CBH.SHAPE;
using UnityEngine;
using UnityEngine.UI;

namespace CBH.WEAPON.UI
{
    /// <summary>
    /// UI for specific ammo
    /// Necessary due to colors of a shape being changeable at any time
    /// </summary>
    public class AmmoUI : MonoBehaviour
    {
        #region //variables
        [SerializeField] private ShapeTypeSO myShape = null;
        [SerializeField] private Image image = null;
        #endregion


        #region //Monobehaviour
        private void OnValidate()
        {
            SetImage();
        }

        private void Start()
        {
            SetImage();
        }

        private void OnEnable()
        {
            GameSettings.OnColorModeChanged += SetImage;
            SetImage();
        }

        private void OnDisable()
        {
            GameSettings.OnColorModeChanged -= SetImage;
        }
        #endregion

        #region //Image modification
        private void SetImage()
        {
            if(myShape == null) return;
            image.color = myShape.GetColor();
            image.sprite = myShape.GetProjectileSprite();
        }

        public void ReplenishAmmo()
        {
            gameObject.SetActive(true);
        }

        public void DepleteAmmo()
        {
            gameObject.SetActive(false);
        }
        #endregion
    }
}
