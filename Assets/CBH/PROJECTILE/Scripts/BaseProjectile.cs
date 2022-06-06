using System;
using System.Collections;
using CBH.MOVEMENT;
using CBH.SHAPE;
using UnityEngine;

namespace CBH.PROJECTILE
{
    public abstract class BaseProjectile : MonoBehaviour, IMovable
    {
        #region //Cached components
        [SerializeField] protected SpriteRenderer spriteRenderer = null;
        protected Rigidbody2D rb = null;
        private Transform currentTarget = null;
        protected MovementTypeSO movementType = null;
        #endregion

        #region //Projectile properties
        [SerializeField] protected ShapeTypeSO myShape = null;
        [SerializeField] private int normalDamage = 1;
        [SerializeField] private int invertedDamage = 1;
        private float timeOffscreenToDeactivate = 2f;
        private float enableTime = 0f;
        private float baseSpeed = 0f;
        private Vector2 lastVelocity = Vector2.zero;
        #endregion

        #region //Events
        public event Action<BaseProjectile> OnDeactivate;
        #endregion


        #region //Monobehaviour
        //Protected
        protected virtual void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }
        protected virtual void Start() { }
        protected virtual void OnEnable() 
        { 
            enableTime = Time.time;
            GameSettings.OnColorModeChanged += SetAppearance;
            SetAppearance();
        }
        protected virtual void OnDisable()
        { 
            GameSettings.OnColorModeChanged -= SetAppearance;
        }
        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            var targets = other.GetComponentsInChildren<IShapeCollision>();
            int damage = GameSettings.inversion ? invertedDamage : normalDamage;

            if(targets.Length > 0)
            {
                Collision(targets, damage);
            }

            DeactivateProjectile();
        }

        protected virtual void FixedUpdate()
        {
            if (movementType == null) return;
            rb.velocity = Vector2.zero;
            movementType.MovementBehavior(this);
            lastVelocity = rb.velocity;
        }

        //Private
        private void OnValidate()
        {
            if(spriteRenderer == null) return;
            if(myShape == null) return;
            SetAppearance();
        }
        private void OnBecameInvisible() 
        {
            if(!gameObject.activeInHierarchy) return;
            StartCoroutine(DeactivateTimer(timeOffscreenToDeactivate)); 
        }
        private void OnBecameVisible() 
        {
            StopAllCoroutines();
        }
        #endregion

        #region //Collision
        protected abstract void Collision(IShapeCollision[] targets, int damage);
        #endregion

        #region //Setting up Projectiles
        //Public
        public virtual void OrientProjectile(Transform _spawnPoint)
        {
            transform.position = _spawnPoint.position;
            transform.rotation = _spawnPoint.rotation;
            rb.position = _spawnPoint.position;
            rb.rotation = _spawnPoint.eulerAngles.z;
        }

        public virtual void SetUpShot(float _exitSpeed, float _exitRotation)
        {
            transform.Rotate(0, 0, _exitRotation);
            rb.velocity = transform.up * _exitSpeed;
            baseSpeed = _exitSpeed;
        }

        public void SetUpMovement(MovementTypeSO _movementType, Transform _target)
        {
            movementType = _movementType;
            currentTarget = _target;
        }

        //Protected
        protected virtual void SetAppearance()
        {
            spriteRenderer.sprite = myShape.GetProjectileSprite();
            spriteRenderer.color = myShape.GetColor();
        }
        #endregion

        #region //Deactivate projectiles
        protected IEnumerator DeactivateTimer(float _time)
        {
            yield return new WaitForSeconds(_time);
            DeactivateProjectile();
        }

        protected void DeactivateProjectile()
        {
            OnDeactivate?.Invoke(this);
            gameObject.SetActive(false);    
        }
        #endregion
    
        #region //Getters
        public ShapeTypeSO GetShapeType() { return myShape; }
        public int GetDamage()
        {
            return GameSettings.inversion ? invertedDamage  : normalDamage;
        }
        #endregion

        #region //IMovable
        public Rigidbody2D GetRigidBody() { return rb; }
        public Transform GetTarget() { return currentTarget; }
        public float GetStartTime() { return enableTime; }
        public Quaternion GetOrientation() { return Quaternion.Euler(0, 0, rb.rotation); }
        public void Break() 
        { 
            float minVelocityThreshold = 3;

            movementType = null;
            Vector2 newVelocity = lastVelocity;
            if(lastVelocity.magnitude < minVelocityThreshold)
            {
                newVelocity *= (minVelocityThreshold / lastVelocity.magnitude);
            }
            rb.velocity = newVelocity;
        }
        #endregion
    }
}