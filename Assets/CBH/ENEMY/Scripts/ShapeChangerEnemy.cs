using System.Collections;
using CBH.PLAYER.ABILITY;
using CBH.SHAPE;
using CBH.WEAPON.PLAYER;
using UnityEngine;

namespace CBH.ENEMY
{
    /// <summary>
    /// Logic specific to the shape changing enemy.
    /// This enemy does not require an enemy renderer,
    /// hence why it has a field for the sprite renderer
    /// </summary>
    public class ShapeChangerEnemy : EnemyType
    {
        #region //Cached components
        [SerializeField] private SpriteRenderer spriteRenderer = null;
        [SerializeField] private ParticleSystem particles = null;
        private ParticleSystem.MainModule particlesMain;
        private AudioClip shapeChangeSFX = null;
        #endregion

        #region //Shapes
        [SerializeField] private ShapeTypeSO[] myShapes = new ShapeTypeSO[0];
        private int index = 0;
        private ShapeTypeSO activeShape => myShapes[index];
        #endregion

        #region //Timers
        [SerializeField] private float weaponSwapFreezeTimer = 1f;
        [Tooltip("Delay for enemy to change shape type")] [SerializeField] private float changeDelay = 0.5f;
        #endregion


        #region //Monobehaviour
        //Protected
        protected override void Awake()
        {
            base.Awake();
            shapeChangeSFX = Resources.Load<AudioClip>("Enemy Shape Changer SFX");
        }

        //Private
        private IEnumerator Start()
        {
            particlesMain = particles.main;
            SetShape(activeShape);

            //Enemy permentantly changes its shape type
            while(true)
            {
                yield return StartCoroutine(GameSettings.GameSpeedScaledTimer(changeDelay));
                index = JZMathUtils.Wrap(index + 1, 0, myShapes.Length - 1);
                SetShape(activeShape);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            //Ensures collision with a player
            var target = other.GetComponent<PlayerWeaponSwapper>();
            if(target == null) return;

            //Sets the player's weapon and freezes it from changing
            other.GetComponentInChildren<WeaponCache>().SetActiveWeapon(activeShape);
            target.FreezeSwapper(weaponSwapFreezeTimer);
            Death(shapeChangeSFX);

        }
        #endregion

        #region //Changing shape
        private void SetShape(ShapeTypeSO _shape)
        {
            spriteRenderer.sprite = _shape.GetBaseSprite();
            particlesMain.startColor = _shape.GetColor();
            spriteRenderer.color = _shape.GetColor();
        }
        #endregion

        #region //Shape Collision Interface
        public override void CollisionWithSameType(int _damage)
        {
            Damage(_damage);
        }
        public override void CollisionWithDifferentType(int _damage)
        {
            Damage(_damage);
        }

        public override ShapeTypeSO GetShapeType()
        {
            return activeShape;
        }
        #endregion
    }
}
