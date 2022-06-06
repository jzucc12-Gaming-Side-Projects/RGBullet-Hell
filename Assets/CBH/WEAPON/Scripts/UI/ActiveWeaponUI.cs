using UnityEngine;
using TMPro;
using CBH.WEAPON.PLAYER;
using System.Collections.Generic;
using CBH.SHAPE;

namespace CBH.WEAPON.UI
{
    /// <summary>
    /// Displays all information regarding the active weapon
    /// </summary>
    public class ActiveWeaponUI : MonoBehaviour
    {
        #region //UI components
        [SerializeField] private TextMeshProUGUI activeWeaponText = null;
        [SerializeField] private TextMeshProUGUI reserveAmmoText = null;
        [SerializeField] private TextMeshProUGUI damageText = null;
        [SerializeField] private GameObject reloadDisplay = null;
        [SerializeField] private Dictionary<ShapeTypeSO, ClipUI> clipUIs = new Dictionary<ShapeTypeSO, ClipUI>();
        private ClipUI activeClipUI = null;
        #endregion

        #region //Weapon components
        private ActiveWeapon activeWeapon = null;
        private PlayerWeaponData activeWeaponData = null;
        #endregion


        #region //Monobehaviour
        private void Awake()
        {
            activeWeapon = FindObjectOfType<ActiveWeapon>();
            foreach(var clipUI in GetComponentsInChildren<ClipUI>())
            {
                clipUIs.Add(clipUI.GetShape(), clipUI);
                clipUI.gameObject.SetActive(false);
            }

            //Displays weapon info relevant to current game mode
            if(GameSettings.inversion)
            {
                reserveAmmoText.gameObject.SetActive(false);
                damageText.gameObject.SetActive(true);
            }
            else
            {
                damageText.gameObject.SetActive(false);
                reserveAmmoText.gameObject.SetActive(true);
            }
        }

        private void OnEnable()
        {
            activeWeapon.activeWeaponChanged += WeaponChanged;
            activeWeapon.ammoUpdated += RefreshAmmoUI;
        }

        private void OnDisable()
        {
            activeWeapon.activeWeaponChanged -= WeaponChanged;
            activeWeapon.ammoUpdated -= RefreshAmmoUI;
        }
        #endregion

        #region //Updating UI
        private void WeaponChanged(PlayerWeaponData _data)
        {
            activeWeaponData = _data;
            var weapon = _data.weapon;
            activeWeaponText.text = weapon.name;
            
            damageText.text = $"Damage: {weapon.GetProjectile().GetDamage()}";
            ChangeActiveClipUI(weapon);
            RefreshAmmoUI();
        }

        private void ChangeActiveClipUI(PlayerWeaponSO _weapon)
        {
            if(activeClipUI != null) activeClipUI.gameObject.SetActive(false);
            activeClipUI = clipUIs[_weapon.GetShapeType()];
            activeClipUI.gameObject.SetActive(true);
        }

        private void RefreshAmmoUI()
        {
            activeClipUI.UpdateClipUI(activeWeaponData.currentClipAmmo);
            reserveAmmoText.text = $"{activeWeaponData.currentReserveAmmo}/{activeWeaponData.weapon.GetMaxReserveAmmo()}";
            RefreshReloadDisplay();
        }

        private void RefreshReloadDisplay()
        {
            if (activeWeaponData.currentClipAmmo > 0)
            {
                reloadDisplay.SetActive(false);
            }
            else
            {
                reloadDisplay.SetActive(true);
            }
        }
        #endregion
    }
}
