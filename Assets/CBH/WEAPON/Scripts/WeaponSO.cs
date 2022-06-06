using System.Collections.Generic;
using CBH.MOVEMENT;
using CBH.PROJECTILE;
using CBH.SHAPE;
using UnityEngine;

namespace CBH.WEAPON
{
    /// <summary>
    /// Holds info for weapons used by player or the enemy
    /// </summary>
    public abstract class WeaponSO : ScriptableObject 
    {
        #region //Weapon variables
        [Header("Projectile variables")]
        [SerializeField] private ShapeTypeSO myShape = null;
        [SerializeField] private ShotTypeSO myShotType = null;
        [SerializeField] private MovementTypeSO projectileMovementType = null;
        private enum TargetType
        {
            none = 0,
            self = 1,
            player = 2
        }
        [SerializeField] private TargetType projectileTarget = TargetType.none;
        #endregion

        #region //Firing variables
        [Header("Firing variables")]

        [Tooltip("How many times the weapon is shot in a single firing instance")]
        [SerializeField, Min(1)] protected int shotsPerFire = 1;

        [Tooltip("Time delay between shots in a firing instance")]
        [SerializeField, ShowIf("shotsPerFire", ComparisonType.greaterThan, 1)] protected float shotDelayTime = 0f;
        #endregion


        #region //Firing
        //Public
        //Fires all shots in a firing instance
        public virtual IEnumerator<float> FireWeapon(IWeaponUser _user)
        {
            for(int ii = 0; ii < GetShotsPerFire(); ii++)
            {
                WeaponShot(_user);
                if(ii >= GetShotsPerFire() - 1) continue;
                yield return GetShotDelayTime();
            }
        }

        //Protected
        //Fires a single shot of the firing instance
        protected void WeaponShot(IWeaponUser _user)
        {
            for (int ii = 0; ii < myShotType.GetProjectilesPerShot(); ii++)
            {
                var projectile = _user.GetProjectile();
                SetProjectileMovement(_user.GetUser(), projectile);
                myShotType.FireShot(projectile, ii);
            }
        }

        //Private
        private void SetProjectileMovement(GameObject _caller, BaseProjectile _projectile)
        {
            if(projectileTarget == TargetType.none)
            {
                _projectile.SetUpMovement(projectileMovementType, null);
            }
            else if(projectileTarget == TargetType.player)
            {
                var player = GameObject.FindGameObjectWithTag("Player").transform;
                _projectile.SetUpMovement(projectileMovementType, player);
            }
            else
            {
                _projectile.SetUpMovement(projectileMovementType, _caller.transform);
            }
        }
        #endregion

        #region //Getters
        //Public
        public ShapeTypeSO GetShapeType() { return myShape; }
        public int GetShotsPerFire() { return shotsPerFire; }

        //Protected
        protected float GetShotDelayTime()
        {
            if(GetShotsPerFire() < 2)
                return 0;
            else
                return shotDelayTime;
        }
        #endregion
    }
}