using UnityEngine;

namespace CBH.MOVEMENT
{
    /// <summary>
    /// User rotates around the target at a radius equal to its current distance from the target
    /// </summary>
    [CreateAssetMenu(fileName = "OrbitMovementType", menuName = "CBH/Movement Types/Targeted/OrbitMovementType", order = 0)]
    public class OrbitMovementType : TargetedMovementType
    {
        [Tooltip("Degrees per second")]
        [SerializeField] float angularSpeed = 1;
        [SerializeField] bool rotateClockwise = false;
        [SerializeField] bool aimAtTarget = false;

        protected override void TargetedBehavior(Rigidbody2D _rb, Transform _target)
        {
            float speedValue = angularSpeed * (rotateClockwise ? -1 : 1);
            Vector2 linearVelocity = JZMathUtils.AngularToLinearVelocity(_rb.position, _target.position, speedValue);
            _rb.velocity += linearVelocity;

            if(aimAtTarget)
            {
                Vector2 distanceFromTarget = (Vector2)_target.position - _rb.position;
                float angleToTarget = Vector2.SignedAngle(Vector2.up, distanceFromTarget);
                _rb.SetRotation(angleToTarget);
            }
        }
    }
}
