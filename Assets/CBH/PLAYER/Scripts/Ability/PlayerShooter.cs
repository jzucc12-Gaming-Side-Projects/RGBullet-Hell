using System.Collections;
using CBH.WEAPON.PLAYER;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CBH.PLAYER.ABILITY
{
    /// <summary>
    /// Fire weapon from player input
    /// </summary>
    public class PlayerShooter : PlayerAbilityDataContainer
    {
        #region //Cached components
        private ActiveWeapon activeWeapon = null;
        private PlayerWeaponData weaponData = null;
        #endregion

        #region //Input actions
        private InputAction fireInput = null;
        private InputAction reloadInput = null;
        #endregion


        #region //Monobehaviour
        protected override void Awake()
        {
            base.Awake();
            fireInput = inputSystem.fireInput;
            reloadInput = inputSystem.reloadInput;
            activeWeapon = GetComponentInChildren<ActiveWeapon>();
        }

        private void OnEnable()
        {
            fireInput.performed += StartFireCallback;
            fireInput.canceled += StopFireCallback;
            reloadInput.performed += Reload;
            activeWeapon.activeWeaponChanged += WeaponChanged;
        }

        private void OnDisable()
        {
            fireInput.performed -= StartFireCallback;
            fireInput.canceled -= StopFireCallback;
            reloadInput.performed -= Reload;
            activeWeapon.activeWeaponChanged -= WeaponChanged;
        }
        #endregion

        #region //Input callbacks
        private void StartFireCallback(InputAction.CallbackContext context)
        {
            StartFire();
        }

        private void StopFireCallback(InputAction.CallbackContext context)
        {
            StopFire();
        }

        private void Reload(InputAction.CallbackContext context)
        {
            activeWeapon.ReloadWeapon();
        }
        #endregion

        #region //Weapon firing
        private void StartFire()
        {
            if (!activeWeapon.CanFireWeapon()) return;
            StartCoroutine(Firing());
        }

        private IEnumerator Firing()
        {
            //Initial shot
            activeWeapon.TryFireWeapon();

            //Continuous fire for automatic weapons
            while(weaponData.CanAutoFire())
            {
                //Fire delay between automatic shots
                yield return new WaitUntil(() =>
                {
                    return activeWeapon.CanFireWeapon();
                });

                activeWeapon.TryFireWeapon();
            }
        }

        private void StopFire()
        {
            StopAllCoroutines();
        }
        #endregion

        #region //Weapon Changing
        private void WeaponChanged(PlayerWeaponData _newData)
        {
            weaponData = _newData;
            StopAllCoroutines();
        }
        #endregion
    }
}