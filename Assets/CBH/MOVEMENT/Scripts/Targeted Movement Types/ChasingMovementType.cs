using UnityEngine;

namespace CBH.MOVEMENT
{
    /// <summary>
    /// User constantly moves toward target
    /// </summary>
    [CreateAssetMenu(fileName = "ChasingMovementType", menuName = "CBH/Movement Types/Targeted/ChasingMovementType", order = 0)]
    public class ChasingMovementType : TargetedMovementType
    {
        [SerializeField, Min(0)] private float chaseSpeed = 1f;
        [SerializeField] private bool runAway = false;

        [Tooltip("Distance from target the enemy will stop moving")]
        [SerializeField] private float stoppingDistance = 0.2f;

        protected override void TargetedBehavior(Rigidbody2D _rb, Transform _target)
        {
            Vector2 _distanceFromTarget = (Vector2)_target.position - _rb.position;
            if(_distanceFromTarget.sqrMagnitude < stoppingDistance * stoppingDistance) return;
            Vector2 chaseVelocity = _distanceFromTarget.normalized * chaseSpeed;
            chaseVelocity *= runAway ? -1 : 1;
            _rb.velocity += chaseVelocity;
        }
    }
}