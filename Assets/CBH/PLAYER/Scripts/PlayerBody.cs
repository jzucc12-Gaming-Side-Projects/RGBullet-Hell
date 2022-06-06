using System;
using System.Collections;
using System.Collections.Generic;
using CBH.SHAPE;
using CBH.WEAPON.PLAYER;
using JZ.AUDIO;
using JZ.CORE;
using UnityEngine;

namespace CBH.PLAYER
{
    /// <summary>
    /// Handles player collisions and effects
    /// </summary>
    public class PlayerBody : MonoBehaviour, IShapeCollision, IOnGlobalShapeChange
    {
        #region //Cached components
        [SerializeField] private Collider2D myCollider = null;
        [SerializeField] private ParticleSystem deathParticles = null;
        private SpriteRenderer spriteRenderer = null;
        private HitEffect hitEffect = null;
        private SoundPlayer sfxPlayer = null;
        private ShapeTypeSO myShape = null;
        private ActiveWeapon activeWeapon = null; //Used for same shape collisions
        private WeaponCache weaponCache = null; //Used to set shape type
        #endregion

        #region //Health and invincibility
        [SerializeField] private int maxHealthPerShape = 5;
        private bool isInvincible = false;    
        private Dictionary<ShapeTypeSO, int> health = new Dictionary<ShapeTypeSO, int>();
        public event Action<ShapeTypeSO, int> OnDamageTaken; 
        #endregion


        #region //Monobehaviour
        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            hitEffect = GetComponent<HitEffect>();
            sfxPlayer = GetComponent<SoundPlayer>();
            activeWeapon = GetComponentInChildren<ActiveWeapon>();
            weaponCache = GetComponentInChildren<WeaponCache>();
            foreach(var shape in weaponCache.GetShapes())
                health.Add(shape, maxHealthPerShape);
        }

        private void OnEnable()
        {
            GameSettings.OnColorModeChanged += ChangeColor;
        }

        private void OnDisable()
        {
            GameSettings.OnColorModeChanged -= ChangeColor;
        }
        #endregion

        #region //Shape changing
        public void ShapeChanged(ShapeTypeSO _newShape)
        {
            OnShapeChange(_newShape);
        }

        private void OnShapeChange(ShapeTypeSO _newShape)
        {
            myShape = _newShape;
            ChangeColor();
        }

        private void ChangeColor()
        {
            spriteRenderer.color = myShape.GetColor();
        }
        #endregion
    
        #region //IHitRegister Methods
        public void CollisionWithSameType(int _)
        {
            activeWeapon.PickUpAmmo();
        }

        public void CollisionWithDifferentType(int _damage)
        {
            PlayerHurt(_damage);
        }

        public ShapeTypeSO GetShapeType()
        {
            return myShape;
        }
        #endregion

        #region //Getting Hurt and death
        //Public
        public void PlayerHurt(int _damage)
        {
            if(isInvincible) return;
            health[myShape] = (int)Mathf.MoveTowards(health[myShape], 0, _damage);
            OnDamageTaken?.Invoke(myShape, health[myShape]);

            if(health[myShape] > 0)
            {
                DamageEffects();
            }
            else
            {
                StartCoroutine(PlayerDeath());
            }
        }

        public void ToggleInvincible() { isInvincible = !isInvincible; }

        //Private
        private IEnumerator PlayerDeath()
        {
            spriteRenderer.enabled = false;
            myCollider.enabled = false;
            DeathEffects();

            //Wait for particle effects to end to trigger defeat
            yield return new WaitUntil(() => !deathParticles.isPlaying);
            FindObjectOfType<GameOver>().InitiateGameOver();
        }

        private void DeathEffects()
        {
            sfxPlayer.Play("Death");
            var main = deathParticles.main;
            main.startColor = myShape.GetColor();
            deathParticles.Play();
            ScreenShake.CallShake(0.67f, 1f);
        }

        private void DamageEffects()
        {
            sfxPlayer.Play("Damage");
            hitEffect.StartHitEffect();
            StartCoroutine(IFrames());
        }

        private IEnumerator IFrames()
        {
            isInvincible = true;
            yield return new WaitForSeconds(hitEffect.GetEffectDuration());
            isInvincible= false;
        }
        #endregion

        #region //Getters
        public int GetMaxHealthPerShape() { return maxHealthPerShape; }
        #endregion
    }
}