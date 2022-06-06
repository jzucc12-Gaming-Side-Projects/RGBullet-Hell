using CBH.MOVEMENT;
using UnityEngine;

namespace CBH.ENEMY.MOVEMENT
{
    /// <summary>
    /// Handles aimless or targeted enemy movement
    /// </summary>
    public class EnemyMover : MonoBehaviour, IMovable
    {
        #region //Cached components
        private Rigidbody2D rb = null;
        #endregion

        #region //Movement variables
        [Header("Base Movement")]
        [Tooltip("Can leave null")] [SerializeField] private MovementTypeSO movementType = null;
        [Range(0,2), SerializeField] private float velocityMultiplier = 1f;
        private float enableTime = 0f; //Used for time based movement types
        #endregion

        #region //Targeting variables
        [Header("Targeting")]
        [SerializeField] private bool playerIsTarget = false;

        [Tooltip("Leave empty if you want no target")]
        [SerializeField, HideIf("playerIsTarget")] private Transform currentTarget = null;
        #endregion


        #region //Monobehaviour
        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            if (playerIsTarget)
            {
                currentTarget = GameObject.FindGameObjectWithTag("Player").transform;
            }
        }

        private void OnEnable()
        {
            enableTime = Time.time;
        }

        private void FixedUpdate()
        {
            rb.velocity = Vector2.zero; //Reset for purposes of movement type logic
            if(movementType != null)
                movementType.MovementBehavior(this);

            rb.velocity *= velocityMultiplier;
            rb.velocity *= GameSettings.GetGameSpeed();
        }
        #endregion

        #region //Setters
        public void SetTarget(Transform _newTarget)
        {
            currentTarget = _newTarget;
        }
        #endregion

        #region //IMovable
        public Rigidbody2D GetRigidBody() { return rb; }
        public Transform GetTarget() { return currentTarget; }
        public float GetStartTime() { return enableTime; }
        public Quaternion GetOrientation() { return Quaternion.Euler(0, 0, rb.rotation); }
        public void Break() { movementType = null; }
        #endregion
    }
}