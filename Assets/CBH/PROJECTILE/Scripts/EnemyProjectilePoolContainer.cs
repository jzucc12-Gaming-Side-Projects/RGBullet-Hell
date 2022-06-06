using System.Collections.Generic;
using CBH.SHAPE;
using UnityEngine;

namespace CBH.PROJECTILE
{
    /// <summary>
    /// Container holding enemy projectiles of a given type.
    /// Enemy shooters will reference these when firing
    /// </summary>
    public class EnemyProjectilePoolContainer : MonoBehaviour
    {
        #region //Variables
        [Tooltip("Entry indices coincide with indices in the size array below")]
        [SerializeField] BaseProjectile[] enemyProjectilePrefabs = new BaseProjectile[0];
        [Tooltip("Entry indices coincide with indices in the prefab array above")]
        [SerializeField] int[] poolSizes = new int[0];
        private Dictionary<ShapeTypeSO, ProjectilePool> projectilePools = new Dictionary<ShapeTypeSO, ProjectilePool>();
        #endregion


        #region //Monobehaviour
        private void Awake()
        {
            for(int ii = 0; ii < enemyProjectilePrefabs.Length; ii++)
            {
                GeneratePool(enemyProjectilePrefabs[ii], poolSizes[ii]);
            }
        }
        #endregion

        #region //Pool creation
        private void GeneratePool(BaseProjectile _prefab, int _poolSize)
        {
            var poolContainer = new GameObject();
            poolContainer.transform.parent = transform;
            poolContainer.name = $"{_prefab.name} projectile pool";
            var pool = new ProjectilePool(_poolSize, _prefab, poolContainer.transform);
            projectilePools.Add(_prefab.GetShapeType(), pool);
        }
        #endregion

        #region //Getters
        public ProjectilePool GetPool(ShapeTypeSO _shapeType)
        {
            return projectilePools[_shapeType];
        }
        #endregion
    }
}