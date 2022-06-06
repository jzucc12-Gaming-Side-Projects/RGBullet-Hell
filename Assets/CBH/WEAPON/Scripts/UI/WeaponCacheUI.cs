using System.Linq;
using CBH.WEAPON.PLAYER;
using UnityEngine;

namespace CBH.WEAPON.UI
{
    /// <summary>
    /// Displays all viable weapons for the player
    /// </summary>
    public class WeaponCacheUI : MonoBehaviour
    {
        #region //Cached components
        private WeaponCacheEntryUI[] entries = new WeaponCacheEntryUI[0];
        private WeaponCache weaponCache = null;
        #endregion


        #region //Monobehaviour
        private void Awake()
        {
            weaponCache = FindObjectOfType<WeaponCache>();
            entries = GetComponentsInChildren<WeaponCacheEntryUI>();
            foreach(var entry in entries)
                entry.gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            weaponCache.indexShifted += ChangeActiveWeaponEntry;
            weaponCache.OnFreeze += FreezeEntries;
            weaponCache.OnUnfreeze += UnfreezeEntries;
        }

        private void OnDisable()
        {
            weaponCache.indexShifted -= ChangeActiveWeaponEntry;
            weaponCache.OnFreeze -= FreezeEntries;
            weaponCache.OnUnfreeze -= UnfreezeEntries;
        }

        private void Start()
        {
            PlayerWeaponSO[] weapons = weaponCache.GetWeapons().ToArray();
            for(int ii = 0; ii < weapons.Length; ii++)
            {
                PlayerWeaponSO weapon = weapons[ii];
                entries[ii].AddEntry(weapon, ii+1);
            }
        }
        #endregion

        #region //Update UI
        private void ChangeActiveWeaponEntry(int _newIndex)
        {
            for(int ii = 0; ii < entries.Length; ii++)
            {
                if(ii == _newIndex)
                    entries[ii].ActivateEntry();
                else
                    entries[ii].DeactivateEntry();
            }
        }

        private void FreezeEntries(PlayerWeaponSO _weapon)
        {
            foreach(var entry in entries)
                entry.FreezeEntry(_weapon);
        }

        private void UnfreezeEntries()
        {
            foreach(var entry in entries)
                entry.UnfreezeEntry();
        }
        #endregion
    }
}
