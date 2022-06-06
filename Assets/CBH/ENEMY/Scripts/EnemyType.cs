using System;
using CBH.SHAPE;
using JZ.AUDIO;
using JZ.CORE.POOL;
using UnityEngine;

namespace CBH.ENEMY
{
    /// <summary>
    /// Handles health, death, and damage of a single enemy shape type
    /// </summary>
    public class EnemyType : MonoBehaviour, IShapeCollision
    {
        #region //Cached components
        [SerializeField] private ShapeTypeSO myShape = null;
        private SoundPlayer sfxPlayer = null;
        private Collider2D[] colliders = new Collider2D[0];
        #endregion

        #region //Health
        [Tooltip("When not in inverted mode")] [SerializeField] private int maxHealth = 1;
        [Tooltip("When in inverted mode")] [SerializeField] private int invertedHealth = 1;
        [SerializeField, ReadOnly] private int currentHealth = 1;
        #endregion

        #region //Hurt and Death
        private AudioClip deathSFX = null;
        public event Action<EnemyType> OnDeath;
        public event Action OnDamage;
        private bool canBeHit = true;
        #endregion


        #region //Monobehaviour
        //Protected
        protected virtual void Awake()
        {
            currentHealth = (GameSettings.inversion ? invertedHealth : maxHealth);
            sfxPlayer = GetComponent<SoundPlayer>();
            deathSFX = Resources.Load<AudioClip>("Enemy Death");
            colliders = GetComponents<Collider2D>();
        }

        //Private
        private void Update()
        {
            canBeHit = true;
        }
        #endregion

        #region //Collision Interface Methods
        public virtual void CollisionWithSameType(int _damage)
        {
            if(!GameSettings.inversion)
                Damage(_damage);
            else
                sfxPlayer.Play("Bullet Bounce");
        }

        public virtual void CollisionWithDifferentType(int _damage)
        {
            if(!GameSettings.inversion)
                sfxPlayer.Play("Bullet Bounce");
            else
                Damage(_damage);
        }

        public virtual ShapeTypeSO GetShapeType()
        {
            return myShape;
        }
        #endregion

        #region //Damange and Death
        protected void Damage(int _damage)
        {
            if(colliders.Length > 1 && !canBeHit) return; // Ensures multiple colliders don't result in hit duplication

            canBeHit = false;
            currentHealth -= _damage;
            if(currentHealth > 0)
            {
                sfxPlayer.Play("Damage");
                OnDamage?.Invoke();
            }
            else
            {
                Death(deathSFX);
            }
        }

        protected void Death(AudioClip _sfx)
        {
            var source = FindObjectOfType<SFXPoolContainer>().GetOjbectFromPool();
            source.clip = _sfx;
            source.Play();
            OnDeath?.Invoke(this);
        }
        #endregion
    }
}