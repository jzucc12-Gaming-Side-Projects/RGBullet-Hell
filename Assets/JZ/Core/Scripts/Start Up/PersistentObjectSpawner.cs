using UnityEngine;

namespace JZ.CORE.STARTUP
{
    /// <summary>
    /// Originally from GameDev.TV
    /// Creates an object that is to persist across all scenes
    /// </summary>
    public class PersistentObjectSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject persistentObjectPrefab = null;
        private static bool spawned = false;

        private void Awake()
        {
            if(spawned) return;
            GameObject persistentObject = Instantiate(persistentObjectPrefab);
            DontDestroyOnLoad(persistentObject);
            spawned = true;
        }
    }
}
