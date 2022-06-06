using System;
using System.Collections.Generic;
using System.Linq;
using JZ.CORE;
using UnityEngine;

namespace CBH.ENEMY.SPAWNER
{
    /// <summary>
    /// Enables and disables groups of spawners to
    /// allow enemies to arrive in waves
    /// </summary>
    public class EnemySpawnerGroup : MonoBehaviour
    {
        [Tooltip("If so, triggers victory when empty")] 
        [SerializeField] private bool finalGroup = false;

        [Tooltip("If so, will activate once parent group is empty")]
        [SerializeField] private bool isChildGroup = true;

        [SerializeField, ShowIf("isChildGroup")] private EnemySpawnerGroup parentGroup = null;
        private EnemySpawner[] spawners = new EnemySpawner[0];
        private List<EnemySpawner> activeSpawners = new List<EnemySpawner>();
        private event Action OnAllEmpty = null;


        #region //Monobehaviour
        private void Awake()
        {
            spawners = GetComponentsInChildren<EnemySpawner>();
            activeSpawners = spawners.ToList();
        }

        private void OnEnable()
        {
            if(isChildGroup)
                parentGroup.OnAllEmpty += ActivateFromParent;

            foreach(var spawner in spawners)
                spawner.OnSpawnerEmpty += MySpawnerIsEmpty;
        }

        private void OnDisable()
        {
            foreach(var spawner in spawners)
                spawner.OnSpawnerEmpty -= MySpawnerIsEmpty;
        }

        private void Start()
        {
            foreach(var spawner in spawners)
                spawner.enabled = !isChildGroup;
        }
        #endregion

        #region //Group emptying
        private void ActivateFromParent()
        {
            parentGroup.OnAllEmpty -= ActivateFromParent;
            foreach(var spawner in spawners)
                spawner.enabled = true;
        }

        private void MySpawnerIsEmpty(EnemySpawner _spawner)
        {
            activeSpawners.Remove(_spawner);
            if(activeSpawners.Count == 0)
                AllEmpty();
        }

        private void AllEmpty()
        {
            OnAllEmpty?.Invoke();
            if(finalGroup) 
                FindObjectOfType<Victory>().InitiateVictory();
        }
        #endregion
    }
}
