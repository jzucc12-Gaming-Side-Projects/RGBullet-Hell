using System.Collections.Generic;
using UnityEngine;
using System;
using CBH.PROJECTILE;
using CBH.SHAPE;

namespace CBH.WEAPON.PLAYER
{
    /// <summary>
    /// Keeps a catalog of all player weapons
    /// </summary>
    public class WeaponCache : MonoBehaviour
    {
        #region //Cached components
        [SerializeField] PlayerWeaponSO[] myWeapons = new PlayerWeaponSO[0];
        private ActiveWeapon activeWeaponComponent = null;
        #endregion

        #region //Weapon dictionaries
        private Dictionary<PlayerWeaponSO, PlayerWeaponData> weaponsData = new Dictionary<PlayerWeaponSO, PlayerWeaponData>();
        #endregion

        #region //Active weapon
        [SerializeField] private int activeIndex = 0;
        private PlayerWeaponSO activeWeaponSO => myWeapons[activeIndex];
        private PlayerWeaponData activeWeaponData => weaponsData[activeWeaponSO];
        #endregion

        #region //Events
        public event Action<int> indexShifted;
        public event Action<PlayerWeaponSO> OnFreeze;
        public event Action OnUnfreeze;
        #endregion


        #region //Monobehaviour
        private void Awake()
        {
            activeWeaponComponent = GetComponent<ActiveWeapon>();
            SetUpWeapons();
        }

        private void Start()
        {
            SetActiveWeapon(activeIndex);
        }
        #endregion

        #region //Set up
        private void SetUpWeapons()
        {
            foreach (PlayerWeaponSO weapon in myWeapons)
            {
                ProjectilePool pool = SetUpPools(weapon);
                var weaponData = new PlayerWeaponData(weapon, pool);
                weaponsData[weapon] = weaponData;
            }
        }

        //Creates a pool for each weapon
        private ProjectilePool SetUpPools(PlayerWeaponSO _weapon)
        {
            var poolContainer = new GameObject();
            poolContainer.transform.parent = transform;
            poolContainer.name = $"{_weapon.name} pool";
            ProjectilePool pool = new ProjectilePool(_weapon.GetClipSize(), _weapon.GetProjectile(), poolContainer.transform);
            return pool;
        }
        #endregion

        #region //Weapon changing
        //Public
        public void ShiftActiveWeapon(int _indexShift)
        {
            int newIndex = activeIndex + _indexShift;
            if(newIndex < 0)
                newIndex = myWeapons.Length - 1;
            else if(newIndex >= myWeapons.Length)
                newIndex = 0;

            SetActiveWeapon(newIndex);
        }

        public void SetActiveWeapon(ShapeTypeSO _newShape)
        {
            for(int ii = 0; ii < myWeapons.Length; ii++)
            {
                if(myWeapons[ii].GetShapeType() != _newShape) continue;
                ChangeWeapon(ii);
            }
        }

        public void SetActiveWeapon(int _newIndex)
        {
            if (!activeWeaponComponent.CanFireWeapon()) return;
            if (_newIndex >= myWeapons.Length) return;
            ChangeWeapon(_newIndex);
        }

        public void Frozen(bool _frozen)
        {
            if(_frozen)
                OnFreeze?.Invoke(activeWeaponSO);
            else
                OnUnfreeze?.Invoke();
        }

        //Private
        private void ChangeWeapon(int _newIndex)
        {
            activeIndex = _newIndex;
            activeWeaponComponent.ChangeWeapon(activeWeaponData);
            indexShifted?.Invoke(_newIndex);
        }
        #endregion
    
        #region //Getters
        public IEnumerable<PlayerWeaponSO> GetWeapons()
        {
            return myWeapons;
        }

        public IEnumerable<ShapeTypeSO> GetShapes()
        {
            foreach(var weapon in myWeapons)
                yield return weapon.GetShapeType();
        }
        #endregion
    }
}
