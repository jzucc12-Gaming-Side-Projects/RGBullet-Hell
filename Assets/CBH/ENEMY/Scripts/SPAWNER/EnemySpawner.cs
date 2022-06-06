using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CBH.ENEMY.SPAWNER
{
    /// <summary>
    /// Randomly spawns a set number of enemies from a given prefab pool
    /// </summary>
    public class EnemySpawner : MonoBehaviour
    {
        #region //Cached Components
        [Header("Spawning pool")]
        [SerializeField] private EnemySpawnerEntry[] spawnEntries = new EnemySpawnerEntry[0];
        private List<EnemyCore> spawnedEnemies = new List<EnemyCore>();
        private int lastIndex = -1;
        #endregion

        #region //Spawner variables
        [Header("Spawning variables")]
        [SerializeField] private int amountToSpawn = 10;
        [SerializeField] private int maxEnabledAtATime = 2;
        [SerializeField] private float enableDelay = 5f;
        private bool inEnableDelay = false;
        #endregion

        #region //Infinite spawner
        [Header("Infinite Spawner")]
        [SerializeField] private bool isInfinite = false;

        [Tooltip("Will spawn another group once remaining enemies drop below threshold")] 
        [SerializeField, ShowIf("isInfinite")] private int infiniteSpawnThreshold = 3;
        #endregion

        #region //Events
        public event System.Action<EnemySpawner> OnSpawnerEmpty;
        #endregion


        #region //Monobehaviour
        private void Awake()
        {
            StartCoroutine(SpawnWave());
        }

        private void OnEnable()
        {
            StartCoroutine(EnableDelay());
        }

        private void Update()
        {
            if(GetEnabledCount() >= maxEnabledAtATime) return;
            if(inEnableDelay) return;
            EnableNextEnemy();
        }
        #endregion

        #region //Spawning enemies
        private IEnumerator SpawnWave()
        {
            for(int ii = 0; ii < amountToSpawn; ii++)
            {
                var enemyChosen = SpawnRandomEnemy();
                SetupEnemy(enemyChosen);
                yield return null;
            }
        }
        private EnemyCore SpawnRandomEnemy()
        {
            //Ensures that the spawner does not produce too many of a single enemy type
            List<int> validIndices = new List<int>();
            for(int ii = 0; ii < spawnEntries.Length; ii++)
            {
                if(!isInfinite && !spawnEntries[ii].CanSpawnEntry()) continue;
                validIndices.Add(ii);
            }

            //Prevent same enemy from spawning twice in a row unless there is no other option.
            int index = validIndices[Random.Range(0, validIndices.Count)];
            if(index == lastIndex && validIndices.Count > 1)
            {
                validIndices.Remove(index);
                index = validIndices[Random.Range(0, validIndices.Count)];
            }

            //Record decision
            lastIndex = index;
            return spawnEntries[index].SpawnEnemy();
        }
        private void SetupEnemy(EnemyCore _enemy)
        {
            AddEnemy(_enemy);
            PositionEnemy(_enemy);
        }
        private void AddEnemy(EnemyCore _enemy)
        {
            _enemy.OnCoreDestroyed += RemoveEnemy;
            spawnedEnemies.Add(_enemy);
        }
        private void PositionEnemy(EnemyCore _enemy)
        {
            _enemy.transform.position = transform.position;
            _enemy.transform.parent = transform;
            _enemy.gameObject.SetActive(false);
        }
        #endregion

        #region //Enabling
        private void EnableNextEnemy()
        {
            foreach(var enemy in spawnedEnemies)
            {
                if(enemy.gameObject.activeInHierarchy) continue;
                enemy.gameObject.SetActive(true);
                break;
            }

            StartCoroutine(EnableDelay());
        }

        private IEnumerator EnableDelay()
        {
            inEnableDelay = true;
            yield return new WaitForSeconds(enableDelay);
            inEnableDelay = false;
        }

        private int GetEnabledCount()
        {
            int count = 0;
            foreach(var enemy in spawnedEnemies)
            {
                if(!enemy.gameObject.activeInHierarchy) continue;
                count++;
            }

            return count;
        }

        private void RemoveEnemy(EnemyCore _enemy)
        {
            //Remove from spawner
            _enemy.OnCoreDestroyed -= RemoveEnemy;
            spawnedEnemies.Remove(_enemy);

            //Ensures parts made members of the spawner
            foreach(var part in _enemy.GetKeptComboParts())
                AddEnemy(part);

            //Check if spawner is empty
            if(spawnedEnemies.Count == 0)
                OnSpawnerEmpty?.Invoke(this);
            
            //Check for infinite spawner spawning a new wave
            if(isInfinite && spawnedEnemies.Count < infiniteSpawnThreshold)
            {
                if(!gameObject.activeInHierarchy) return;
                StartCoroutine(SpawnWave());
            }
        }
        #endregion
    }
}