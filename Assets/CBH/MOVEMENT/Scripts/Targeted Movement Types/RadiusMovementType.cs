using UnityEngine;

namespace CBH.MOVEMENT
{
    /// <summary>
    /// User tries to stay a certain distance away
    /// from its target
    /// </summary>
    [CreateAssetMenu(fileName = "RadiusMovementType", menuName = "CBH/Movement Types/Targeted/RadiusMovementType", order = 0)]
    public class RadiusMovementType : TargetedMovementType
    {
        #region //Radius Variables
        [Tooltip("Move speed is limited by the speed of its target to prevent jittery movement")]
        [SerializeField, Min(0)] float maxMoveSpeed = 1f;
        [Tooltip("In Unity Units")] [SerializeField, Min(0)] float minRadius = 2f;
        [Tooltip("In Unity Units")] [SerializeField, Min(0)] float maxRadius = 4f;
        #endregion


        protected override void TargetedBehavior(Rigidbody2D _rb, Transform _target)
        {
            Vector2 distanceFromTarget = (Vector2)_target.position - _rb.position;
            float squareDistance = distanceFromTarget.sqrMagnitude;
            if(!TooClose(squareDistance) && !TooFar(squareDistance)) return;
            
            float offset;
            if(TooClose(squareDistance))
                offset = minRadius - distanceFromTarget.magnitude;
            else
                offset = distanceFromTarget.magnitude - maxRadius;
            
            //Will use the offset value if movespeed matches/exceeds player movespeed
            //Prevents jittery motion in the camera
            float velocityScale = Mathf.Min(maxMoveSpeed, offset/Time.deltaTime); 
            
            Vector2 velocityVector = distanceFromTarget.normalized *  velocityScale;
            velocityVector *= (TooClose(squareDistance) ? -1 : 1);
            _rb.velocity += velocityVector;
        }

        private bool TooClose(float _squareMagnitude)
        {
            return _squareMagnitude < minRadius * minRadius;
        }

        private bool TooFar(float _squareMagnitudue)
        {
            return _squareMagnitudue > maxRadius * maxRadius;
        }
    }
}
