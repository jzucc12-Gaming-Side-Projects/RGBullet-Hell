using System.Collections;
using CBH.WEAPON.PLAYER;
using JZ.AUDIO;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CBH.PLAYER.ABILITY
{
    /// <summary>
    /// Change weapons from player input
    /// </summary>
    public class PlayerWeaponSwapper : PlayerAbilityDataContainer
    {
        #region //Cached components
        private SoundPlayer sfxPlayer = null;
        private WeaponCache weaponCache = null;
        #endregion

        #region //Input actions
        private InputAction shiftWeaponInput = null;
        private InputAction setWeaponInput = null;
        #endregion

        private bool frozen = false;


        #region //Monobehaviour
        protected override void Awake()
        {
            base.Awake();
            sfxPlayer = GetComponent<SoundPlayer>();
            weaponCache = GetComponentInChildren<WeaponCache>();
            shiftWeaponInput = inputSystem.shiftWeaponInput;
            // setWeaponInput = inputSystem.setWeaponInput;
        }

        private void OnEnable()
        {
            shiftWeaponInput.performed += ShiftWeapon;
            // setWeaponInput.performed += SetWeapon;
        }

        private void OnDisable()
        {
            shiftWeaponInput.performed -= ShiftWeapon;
            // setWeaponInput.performed -= SetWeapon;
        }
        #endregion

        #region //Input callbacks
        //Moves active weapon over 1 entry in the player's weapon cache
        private void ShiftWeapon(InputAction.CallbackContext context)
        {
            if(IsFrozen()) return;
            float rawValue = context.ReadValue<float>();
            int shiftValue = Mathf.RoundToInt(rawValue);
            weaponCache.ShiftActiveWeapon(shiftValue);
        }

        //Sets weapon from hotkey entry on keyboard
        private void SetWeapon(InputAction.CallbackContext context)
        {
            if(IsFrozen()) return;
            float rawValue = context.ReadValue<float>();
            int newIndex = Mathf.RoundToInt(rawValue);
            weaponCache.SetActiveWeapon(newIndex);
        }
        #endregion
    
        #region //Freezing
        public void FreezeSwapper(float _duration)
        {
            StartCoroutine(FreezeSwapperRoutine(_duration));
        }

        private IEnumerator FreezeSwapperRoutine(float _duration)
        {
            frozen = true;
            weaponCache.Frozen(true);
            yield return new WaitForSeconds(_duration);
            weaponCache.Frozen(false);
            frozen = false;
        }

        private bool IsFrozen()
        {
            if(frozen)
            {
                sfxPlayer.Play("Cant Change Weapon");
                return true;
            }
            
            return false;
        }
        #endregion
    }
}
