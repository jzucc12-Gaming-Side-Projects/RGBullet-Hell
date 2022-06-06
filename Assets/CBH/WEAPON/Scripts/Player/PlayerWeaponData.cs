using CBH.PROJECTILE;
using UnityEngine;

namespace CBH.WEAPON.PLAYER
{
    /// <summary>
    /// Holds dynamic data, such as ammo counts, for the player weapon SO's
    /// </summary>
    public class PlayerWeaponData
    {
        #region //Weapon information
        public PlayerWeaponSO weapon = null;
        public ProjectilePool pool = null;
        public int currentClipAmmo = 0;
        public int currentReserveAmmo = 0;
        #endregion


        #region //Constructors
        public PlayerWeaponData(PlayerWeaponSO _weapon, ProjectilePool _pool)
        {
            weapon = _weapon;
            pool = _pool;
            currentClipAmmo = _weapon.GetClipSize();
            currentReserveAmmo = _weapon.GetMaxReserveAmmo();
        }

        public PlayerWeaponData()
        {
            weapon = null;
            pool = null;
        }
        #endregion

        #region //Weapon States
        public bool NeedReload()
        {
            return currentClipAmmo < weapon.GetShotsPerFire();
        }

        public bool OutOfAmmo()
        {
            return currentReserveAmmo <= 0;
        }

        public bool ClipIsFull()
        {
            return currentClipAmmo == weapon.GetClipSize();
        }

        public bool CanAutoFire()
        {
            return weapon.IsAutomatic() && !NeedReload();
        }
        #endregion

        #region //Ammo modification
        public void ReduceAmmo()
        {
            currentClipAmmo--;
        }

        public void ReloadAmmo()
        {
            if(GameSettings.inversion)
            {
                currentClipAmmo = weapon.GetClipSize();
                return;
            }

            int ammoMissing = weapon.GetClipSize() - currentClipAmmo;
            int amountToReload = Mathf.Min(ammoMissing, currentReserveAmmo);
            currentClipAmmo += amountToReload;
            currentReserveAmmo -= amountToReload;
        }

        public void PickUpAmmo()
        {
            int newReserves = currentReserveAmmo + weapon.GetAmmoPickUpAmmount();
            currentReserveAmmo = Mathf.Min(newReserves, weapon.GetMaxReserveAmmo());
        }
        #endregion
    }
}
