using System.Collections.Generic;
using UnityEngine;

namespace JZ.CORE.POOL
{
    /// <summary>
    /// Object pool on its own Monobehviour
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ObjectPoolContainer<T> : MonoBehaviour where T : Component
    {
        [SerializeField] protected T poolPrefab = null;
        [SerializeField] protected int poolSize = 0;
        protected ObjectPool<T> pool = null;


        #region //Monobehaviour
        protected virtual void Awake()
        {
            pool = GeneratePool(poolPrefab, poolSize);
        }
        protected virtual void OnEnable() { }
        protected virtual void OnDisable() { }
        protected virtual void Start() { }
        #endregion

        #region //Pool methods
        protected virtual ObjectPool<T> GeneratePool(T _prefab, int _poolSize)
        {
            return new ObjectPool<T>(_poolSize, _prefab, transform);
        }

        public virtual T GetOjbectFromPool()
        {
            return pool.GetObject();
        }

        public virtual IEnumerable<T> GetObjectsFromPool(int _count)
        {
            return pool.GetObjects(_count);
        }
        #endregion
    }

}