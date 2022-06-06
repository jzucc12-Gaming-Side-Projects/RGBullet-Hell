using CBH.WEAPON.PLAYER;
using JZ.INPUT;
using JZ.INPUT.UI;
using UnityEngine;
using UnityEngine.UI;

namespace CBH.WEAPON.UI
{
    /// <summary>
    /// Displays one of the weapons for the player
    /// </summary>
    public class WeaponCacheEntryUI : MonoBehaviour
    {
        #region //Cached components
        [SerializeField] private Image[] borders = new Image[0];
        [SerializeField] private Image weaponImage = null;
        [SerializeField] private ControlDisplayV1 numberDisplay = null;
        [SerializeField] private Image freezeCross = null;
        private PlayerWeaponSO myWeapon = null;
        #endregion

        #region //Display variables
        [SerializeField] private Color activeColor = Color.white;
        [SerializeField] private Color defaultColor = Color.white;
        private ControlDisplayData numberControl = new ControlDisplayData(14, Color.white);
        #endregion


        #region //Monobehaviour
        private void OnEnable()
        {
            GameSettings.OnColorModeChanged += ChangeColor;
        }

        private void OnDisable()
        {
            GameSettings.OnColorModeChanged -= ChangeColor;
        }
        #endregion

        #region //Entry updating
        public void AddEntry(PlayerWeaponSO _weapon, int _controlNumber)
        {
            UnfreezeEntry();

            //Display entry
            myWeapon = _weapon;
            gameObject.SetActive(true);
            weaponImage.sprite = myWeapon.GetShapeType().GetProjectileSprite();
            ChangeColor();

            //Update hotkey UI for weapon
            numberControl.controlName = _controlNumber.ToString();
            numberDisplay.ChangeControl(GamepadType.none, numberControl);
            numberDisplay.UpdateControl();
        }

        public void ActivateEntry()
        {
            ChangeBorderColor(activeColor);
        }
        public void DeactivateEntry()
        {
            ChangeBorderColor(defaultColor);
        }
        #endregion

        #region //Freezing
        public void FreezeEntry(PlayerWeaponSO _weapon)
        {
            if(_weapon == myWeapon)
                UnfreezeEntry();
            else
                freezeCross.enabled = true;
        }

        public void UnfreezeEntry()
        {
            freezeCross.enabled = false;
        }
        #endregion

        #region //Color changing
        private void ChangeBorderColor(Color _color)
        {
            foreach(var border in borders)
                border.color = _color;
        }

        private void ChangeColor()
        {
            weaponImage.color =  myWeapon.GetShapeType().GetColor();
        }
        #endregion
    }
}
