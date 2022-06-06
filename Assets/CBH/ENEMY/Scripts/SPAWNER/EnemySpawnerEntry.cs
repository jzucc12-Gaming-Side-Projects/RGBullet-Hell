using UnityEngine;

namespace CBH.ENEMY.SPAWNER
{
    /// <summary>
    /// Holds info for possible enemies in an enemy spawner
    /// </summary>
    [System.Serializable]
    public class EnemySpawnerEntry
    {
        [SerializeField] private EnemyCore enemyPrefab = null;
        [SerializeField] private int maxCount = 4;
        private int currentCount = 0;


        public bool CanSpawnEntry()
        {
            return currentCount < maxCount;
        }

        public EnemyCore SpawnEnemy()
        {
            currentCount++;
            return GameObject.Instantiate(enemyPrefab);
        }
    }
}