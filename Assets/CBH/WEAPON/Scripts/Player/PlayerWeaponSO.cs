using System.Collections;
using System.Collections.Generic;
using CBH.PROJECTILE;
using UnityEngine;

namespace CBH.WEAPON.PLAYER
{
    /// <summary>
    /// Weapons specifically intended for player use
    /// </summary>
    [CreateAssetMenu(fileName = "PlayerWeaponSO", menuName = "CBH/Weapons/PlayerWeaponSO", order = 0)]
    public class PlayerWeaponSO : WeaponSO
    {
        
        #region //Player firing variables
        [SerializeField, Min(0)] private float reloadTime = 0.5f;

        [Tooltip("Time delay after the final shot in a firing instance")] 
        [SerializeField, Min(0f)] private float fireDelayTime = 1f;
        [SerializeField] private BaseProjectile projectilePrefab = null;
        [SerializeField] private bool isAutomatic = false;
        #endregion

        #region //Ammo variables
        [Header("Ammo variables")]
        [SerializeField, Min(1)] private int clipSize = 1;
        [SerializeField, Min(0)] private int maxReserveAmmo = 1;
        [SerializeField, Min(0)] private int ammoPickUpAmount = 1;
        #endregion

        #region //SFX
        [Header("SFX")]
        [SerializeField] private AudioClip fireSFX = null;
        [SerializeField] private AudioClip reloadSFX = null;
        #endregion


        #region //Firing
        public override IEnumerator<float> FireWeapon(IWeaponUser _user)
        {
            //Recreates base version of method
            IEnumerator original = base.FireWeapon(_user);
            while(original.MoveNext())
                yield return (float)original.Current;

            //Added delay between firing instances
            yield return fireDelayTime;
        }
        #endregion

        #region //Getters
        public int GetClipSize() { return clipSize; }
        public int GetMaxReserveAmmo() { return maxReserveAmmo; }
        public int GetAmmoPickUpAmmount() { return ammoPickUpAmount; }
        public bool IsAutomatic() { return isAutomatic; }
        public float GetReloadTime() { return reloadTime; }
        public AudioClip GetFireSFX() { return fireSFX; }
        public AudioClip GetReloadSFX() { return reloadSFX; }
        public BaseProjectile GetProjectile() { return projectilePrefab; }
        #endregion
    }
}