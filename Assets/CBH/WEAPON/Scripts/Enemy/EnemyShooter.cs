using System.Collections;
using System.Collections.Generic;
using CBH.PROJECTILE;
using JZ.AUDIO;
using UnityEngine;

namespace CBH.WEAPON.ENEMY
{
    /// <summary>
    /// Handles firing of enemy weapons
    /// </summary>
    public class EnemyShooter : MonoBehaviour, IWeaponUser
    {
        #region //Cached components
        private Transform player = null;
        private ProjectilePool pool = null;
        private SoundPlayer sfxPlayer = null;
        [SerializeField] private EnemyWeaponSO myWeapon = null;
        #endregion

        #region //Firing variables
        [SerializeField, Min(0)] private float timeBetweenFires = 0f;
        private bool currentlyFiring = false;
        #endregion

        #region //States
        private int maxProjectiles => myWeapon.GetMaxProjectiles();
        private List<BaseProjectile> myProjectiles = new List<BaseProjectile>();
        private int activeProjectiles => myProjectiles.Count;
        #endregion


        #region //Monbehaviour
        private void Awake()
        {
            sfxPlayer = GetComponent<SoundPlayer>();
        }

        private void Start()
        {
            FindProjectilePool();
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        private void OnEnable()
        {
            StartCoroutine(FireDelay());
        }

        private void OnDisable()
        {
            List<BaseProjectile> currentProjectiles = new List<BaseProjectile>(myProjectiles);
            foreach(var projectile in currentProjectiles)
            {
                OnProjectileDeactivate(projectile);
            }
        }

        private void Update()
        {
            if(CanFire())
            {
                StartCoroutine(Fire());
            }
        }
        #endregion

        #region //Set up and deletion
        private void FindProjectilePool()
        {
            var poolContainer = FindObjectOfType<EnemyProjectilePoolContainer>();
            pool = poolContainer.GetPool(myWeapon.GetShapeType());
        }

        private void CheckToDelete()
        {
            if(pool.GetActiveCount() == 0)
                Destroy(gameObject);
        }
        #endregion

        #region //Firing 
        private IEnumerator Fire()
        {
            currentlyFiring = true;
            List<IEnumerator> fireRoutines = new List<IEnumerator>();
            IEnumerator fireRoutine = myWeapon.FireWeapon(this);

            sfxPlayer.Play("Fire");

            //Allow for logic between the shots in a firing instance
            while(fireRoutine.MoveNext())
            {
                float waitTime = (float)fireRoutine.Current;
                yield return StartCoroutine(GameSettings.GameSpeedScaledTimer(waitTime));
                sfxPlayer.Play("Fire");
            }

            StartCoroutine(FireDelay());
        }

        private IEnumerator FireDelay()
        {
            currentlyFiring = true;
            yield return StartCoroutine(GameSettings.GameSpeedScaledTimer(timeBetweenFires));
            currentlyFiring = false;
        }

        private bool CanFire()
        {
            if(currentlyFiring) return false;
            if(activeProjectiles >= maxProjectiles) return false;
            return true;
        }
        #endregion

        #region //Projectile Management
        private void OnProjectileDeactivate(BaseProjectile _projectile)
        {  
            myProjectiles.Remove(_projectile);
            _projectile.OnDeactivate -= OnProjectileDeactivate;
        }
        #endregion

        #region //IWeaponUser
        public BaseProjectile GetProjectile()
        {
            var projectile = pool.GetProjectile(transform);
            myProjectiles.Add(projectile);
            projectile.OnDeactivate += OnProjectileDeactivate;
            return projectile;
        }

        public GameObject GetUser()
        {
            return gameObject;
        }

        public Coroutine Routine(IEnumerator _e)
        {
            return StartCoroutine(_e);
        }
        #endregion
    }
}