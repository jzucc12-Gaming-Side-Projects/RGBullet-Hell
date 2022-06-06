using System;
using System.Collections;
using CBH.PROJECTILE;
using CBH.SHAPE;
using JZ.AUDIO;
using UnityEngine;

namespace CBH.WEAPON.PLAYER
{
    /// <summary>
    /// Handles the logic for the weapon the
    /// player currently has equipped
    /// </summary>
    public class ActiveWeapon : MonoBehaviour, IWeaponUser
    {
        #region //Cached components
        private PlayerWeaponData weaponData = new PlayerWeaponData();
        private PlayerWeaponSO activeWeapon => weaponData.weapon;
        private SoundPlayer sfxPlayer = null;
        private GlobalShapeManager globalShapeManager = null;
        #endregion

        #region //Firing state variables
        private bool isReloading = false;
        private bool isBetweenShots = false;
        #endregion

        #region //Events
        public event Action ammoUpdated;
        public event Action<PlayerWeaponData> activeWeaponChanged;
        #endregion

        #region //Monobehaviour
        private void Awake()
        {
            sfxPlayer = GetComponent<SoundPlayer>();
            globalShapeManager = Resources.Load<GlobalShapeManager>("Global Shape Manager");
        }
        #endregion


        #region //Firing weapon
        //Public
        public void TryFireWeapon()
        {
            if(!weaponData.NeedReload())
            {
                StartCoroutine(FireWeapon());
            }
            else if(!weaponData.OutOfAmmo())
            {
                ReloadWeapon();
            }
        }

        //Private
        private IEnumerator FireWeapon()
        {
            isBetweenShots = true;

            IEnumerator fireRoutine = activeWeapon.FireWeapon(this);

            //Allow for logic between the shots in a firing instance
            while(fireRoutine.MoveNext())
            {
                sfxPlayer.Play("Fire");
                weaponData.ReduceAmmo();
                ammoUpdated?.Invoke();
                float waitTime = (float)fireRoutine.Current;
                yield return new WaitForSeconds(waitTime);
            }

            isBetweenShots = false;
        }
        #endregion

        #region //Reloading
        //Public
        public void ReloadWeapon()
        {
            if(isReloading) return; 
            if(weaponData.ClipIsFull()) return;
            if(weaponData.currentReserveAmmo <= 0) return;

            sfxPlayer.Play("Reload");
            weaponData.ReloadAmmo();
            ammoUpdated?.Invoke();
            StartCoroutine(ReloadTimer());
        }

        public void PickUpAmmo()
        {
            sfxPlayer.Play("Ammo Pickup");
            weaponData.PickUpAmmo();
            ammoUpdated?.Invoke();
        }

        //Private
        private IEnumerator ReloadTimer()
        {
            isReloading = true;
            yield return new WaitForSeconds(activeWeapon.GetReloadTime());
            isReloading = false;
        }
        #endregion

        #region //Weapon changing
        //Public
        public void ChangeWeapon(PlayerWeaponData _weaponData)
        {
            WeaponChangeSFXHandling(_weaponData.weapon);
            ResetWeaponState();
            weaponData = _weaponData;
            globalShapeManager.SetGlobalShape(GetActiveShape());
            activeWeaponChanged?.Invoke(_weaponData);
        }
        
        //Private
        private void WeaponChangeSFXHandling(PlayerWeaponSO _weapon)
        {
            sfxPlayer.Play("Change Weapon");
            sfxPlayer.ChangeClip("Fire", _weapon.GetFireSFX());
            sfxPlayer.ChangeClip("Reload", _weapon.GetReloadSFX());
        }

        private void ResetWeaponState()
        {
            StopAllCoroutines();
            isReloading = false;
            isBetweenShots = false;
        }
        #endregion

        #region //Firing states
        public bool CanFireWeapon()
        {
            return !isReloading && !isBetweenShots;
        }
        #endregion

        #region //Getters
        //Public
        public ShapeTypeSO GetActiveShape() 
        {
            if(activeWeapon != null) 
                return activeWeapon.GetShapeType(); 
            else
                return null;
        }
        private ProjectilePool GetPool()
        {
            return weaponData.pool;
        }
        #endregion

        #region //IWeaponUser
        public BaseProjectile GetProjectile()
        {
            return GetPool().GetProjectile(transform);
        }

        public GameObject GetUser()
        {
            return gameObject;
        }

        public Coroutine Routine(IEnumerator _e)
        {
            return StartCoroutine(_e);
        }
        #endregion
    }
}