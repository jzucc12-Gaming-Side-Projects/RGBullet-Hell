using CBH.SHAPE;
using JZ.CORE;
using JZ.CORE.POOL;
using UnityEngine;

namespace CBH.ENEMY
{
    /// <summary>
    /// Sets the enemy color and handles VFX
    /// </summary>
    public class EnemyRenderer : MonoBehaviour
    {
        #region //Sprite and shape
        [SerializeField] private EnemyType myType = null;
        [SerializeField] private SpriteRenderer enemyBodySprite = null;
        private HitEffect hitEffect = null;
        #endregion


        #region //Monobehaviour
        private void OnValidate()
        {
            if(enemyBodySprite == null) return;
            if(myType == null) return;
            SetUpSprite(myType.GetShapeType());
        }

        private void Awake()
        {
            enemyBodySprite = GetComponent<SpriteRenderer>();
            hitEffect = GetComponent<HitEffect>();
        }

        private void Start()
        {
            SetUpSprite(myType.GetShapeType());
        }

        private void OnEnable()
        {
            myType.OnDamage += hitEffect.StartHitEffect;
            myType.OnDeath += ShowDeathParticles;
            GameSettings.OnColorModeChanged += Start;
        }

        private void OnDisable()
        {
            myType.OnDamage -= hitEffect.StartHitEffect;
            myType.OnDeath -= ShowDeathParticles;
            GameSettings.OnColorModeChanged -= Start;
        }
        #endregion

        #region //Sprite altering
        public void SetUpSprite(ShapeTypeSO _shape)
        {
            if(_shape == null) return;
            enemyBodySprite.color = _shape.GetColor();
        }
        #endregion

        #region //Particle effects
        private void ShowDeathParticles(EnemyType _type)
        {
            var deathParticles = FindObjectOfType<ParticlePoolContainer>().GetOjbectFromPool();
            var main = deathParticles.main;
            main.startColor = _type.GetShapeType().GetColor();
            deathParticles.transform.position = transform.position;
            deathParticles.Play();
        }
        #endregion
    }
}