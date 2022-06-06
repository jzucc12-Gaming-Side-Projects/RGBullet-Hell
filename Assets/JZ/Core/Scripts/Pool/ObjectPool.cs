using System.Collections.Generic;
using UnityEngine;

namespace JZ.CORE.POOL
{
    /// <summary>
    /// Base object pool class
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ObjectPool<T> where T : Component
    {
        #region //Cached components
        protected T poolObject = null;
        protected List<T> pooledObjects = new List<T>();
        protected Transform poolContainer = null;
        #endregion

        #region //Pool variables
        protected int poolSize = 1;
        #endregion


        #region //Constructor
        public ObjectPool(int _poolSize, T _poolObject, Transform _poolContainer)
        {
            poolSize = _poolSize;
            poolObject = _poolObject;
            poolContainer = _poolContainer;
            for(int ii = 0; ii < poolSize; ii++)
            {
                AddPoolObject();
            }
        }
        #endregion

        #region //Enabling objects
        //Public
        public T GetObject()
        {
            foreach(var po in pooledObjects)
            {
                if(IsInUse(po)) continue;
                Activate(po);
                return po;
            }

            T newGO = AddPoolObject();
            Activate(newGO);
            return newGO;
        }

        public IEnumerable<T> GetObjects(int _count)
        {
            for(int ii = 0; ii < _count; ii++)
            {
                yield return GetObject();
            }
        }

        //Private
        protected T AddPoolObject()
        {
            T po = GameObject.Instantiate(poolObject, poolContainer);
            pooledObjects.Add(po);
            Deactivate(po);
            return po;
        }
        #endregion
    
        #region //Object pool states
        protected virtual bool IsInUse(T _object)
        {
            return _object.gameObject.activeInHierarchy;
        }

        public int GetActiveCount()
        {
            int enabled = 0;
            foreach(T po in pooledObjects)
            {
                if(!IsInUse(po)) continue;
                enabled++;
            }
            return enabled;
        }

        public int GetInactiveCount()
        {
            int enabled = GetActiveCount();
            return pooledObjects.Count - enabled;
        }
        #endregion
    
        #region //Disabling and Destroying
        public void Destroy()
        {
            if(poolContainer == null) return;
            GameObject.Destroy(poolContainer.gameObject);
        }

        public void Disable()
        {
            if(poolContainer == null) return;
            poolContainer.gameObject.SetActive(false);
        }
        #endregion

        #region //Object activateion
        protected virtual void Activate(T _object)
        {
            _object.gameObject.SetActive(true);
        }

        protected virtual void Deactivate(T _object)
        {
            _object.gameObject.SetActive(false);
        }
        #endregion
    }
}