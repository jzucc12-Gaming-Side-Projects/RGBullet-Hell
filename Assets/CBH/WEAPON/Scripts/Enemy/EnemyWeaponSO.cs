using UnityEngine;

namespace CBH.WEAPON.ENEMY
{
    /// <summary>
    /// Weapons specifically for enemy use
    /// </summary>
    [CreateAssetMenu(fileName = "EnemyWeaponSO", menuName = "CBH/Weapons/EnemyWeaponSO", order = 0)]
    public class EnemyWeaponSO : WeaponSO
    {
        #region //Enemy firing variables
        [Tooltip("Maximum number projectiles this weapon can have onscreen at once")] 
        [SerializeField] private int maxProjectiles = 10;
        #endregion

        #region //Getters
        public int GetMaxProjectiles()
        {
            return maxProjectiles;
        }
        #endregion
    }
}
