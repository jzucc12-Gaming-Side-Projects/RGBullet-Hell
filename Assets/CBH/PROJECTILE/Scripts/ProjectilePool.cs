using System.Collections.Generic;
using JZ.CORE.POOL;
using UnityEngine;

namespace CBH.PROJECTILE
{
    /// <summary>
    /// Projectile pool specific to weapon projectiles.
    /// Useful for setting up projectiles upon activation
    /// </summary>
    public class ProjectilePool : ObjectPool<BaseProjectile>
    {
        #region //Constructor
        public ProjectilePool(int _poolSize, BaseProjectile _poolObject, Transform _poolContainer) : base(_poolSize, _poolObject, _poolContainer)
        {
        }
        #endregion

        public IEnumerable<BaseProjectile> GetProjectiles(Transform _spawnPoint, int _count)
        {
            var projectiles = GetObjects(_count);
            foreach(var projectile in projectiles)
            {
                projectile.OrientProjectile(_spawnPoint);
            }
            return projectiles;
        }

        public BaseProjectile GetProjectile(Transform _spawnPoint)
        {
            var projectile = GetObject();
            projectile.OrientProjectile(_spawnPoint);
            return projectile;
        }
    }
}
