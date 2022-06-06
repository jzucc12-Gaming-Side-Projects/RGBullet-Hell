using UnityEngine;

namespace CBH.PROJECTILE
{
    /// <summary>
    /// Modular way to allow for unique projectile spawning amongst different weapons
    /// </summary>
    public abstract class ShotTypeSO : ScriptableObject
    {
        [Header("General Shot Variables")]
        [SerializeField, Min(1)] protected int projectilesPerShot = 1;
        [Tooltip("Projectile speed the frame it enables")]
        [SerializeField, Min(0)] protected float exitSpeed = 0f;



        public abstract void FireShot(BaseProjectile _projectile, int _shotNumber);
        public int GetProjectilesPerShot()
        {
            return projectilesPerShot;
        }
    }
}