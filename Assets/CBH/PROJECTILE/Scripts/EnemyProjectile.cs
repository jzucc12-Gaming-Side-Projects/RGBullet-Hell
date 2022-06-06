using CBH.SHAPE;
using UnityEngine;

namespace CBH.PROJECTILE
{
    public class EnemyProjectile : BaseProjectile, IOnGlobalShapeChange
    {
        #region //Variables
        [SerializeField] private Sprite ammoSprite = null;
        [SerializeField] private BoxCollider2D ammoCollider = null;
        private Collider2D bulletCollider = null;
        private GlobalShapeManager globalShapeManager = null;
        private Vector2 baseVelocity = Vector2.zero;
        #endregion


        #region //Monobehaviour
        protected override void Awake()
        {
            base.Awake();
            globalShapeManager = Resources.Load<GlobalShapeManager>("Global Shape Manager");
            GetBulletCollider();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            SetSprite(globalShapeManager.GetGlobalShape());
        }

        protected override void FixedUpdate()
        {
            if(movementType != null)
            {
                base.FixedUpdate();
                rb.velocity *= GameSettings.GetGameSpeed();
            }
            else
            {
                rb.velocity = baseVelocity * GameSettings.GetGameSpeed();
            }
  
        }
        #endregion

        #region //Collision
        protected override void Collision(IShapeCollision[] _targets, int _damage)
        {
            var target = _targets[0];
            if (myShape.IsSameAs(target.GetShapeType()))
            {
                target.CollisionWithSameType(_damage);
            }
            else
            {
                target.CollisionWithDifferentType(_damage);
            }
        }
        #endregion

        #region //Setup
        private void GetBulletCollider()
        {
            foreach (var collider in GetComponents<Collider2D>())
            {
                if (collider == ammoCollider) continue;
                bulletCollider = collider;
                return;
            }
        }

        public override void SetUpShot(float _exitSpeed, float _exitRotation)
        {
            base.SetUpShot(_exitSpeed, _exitRotation);
            baseVelocity = rb.velocity;
        }
        #endregion

        #region //Shape changing
        //Public
        public void ShapeChanged(ShapeTypeSO _newShape)
        {
            SetSprite(_newShape);
        }

        //Private
        private void SetSprite(ShapeTypeSO _shape)
        {
            if(myShape.IsSameAs(_shape))
            {
                SetToAmmo();
            }
            else
            {
                SetToBullet();
            }
        }

        private void SetToAmmo()
        {
            spriteRenderer.sprite = ammoSprite;
            bulletCollider.enabled = false;
            ammoCollider.enabled = true;
        }

        private void SetToBullet()
        {
            spriteRenderer.sprite = myShape.GetProjectileSprite();
            bulletCollider.enabled = true;
            ammoCollider.enabled = false;
        }
        #endregion
    }
}