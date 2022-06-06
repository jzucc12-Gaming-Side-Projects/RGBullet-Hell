using UnityEngine;

namespace CBH.MOVEMENT
{
    /// <summary>
    /// User moves in a zig-zag pattern
    /// </summary>
    [CreateAssetMenu(fileName = "ZigZagMovementType", menuName = "CBH/Movement Types/ZigZagMovementType", order = 0)]
    public class ZigZagMovementType : FreeMovementType
    {
        [Header("Zigzag variables")]
        [Tooltip("How long until the user switches directions")] [SerializeField] float period = 1f;
        [SerializeField] float moveSpeed = 1f;
        [Tooltip("Determines zig-zag travel direction")] [SerializeField] Vector2 zigzagVector = Vector2.zero;
        [Tooltip("Numeric representation of the above vector")] [SerializeField, ReadOnly] float zigzagSlope = 0f;


        protected override void OnValidate()
        {
            base.OnValidate();
            zigzagSlope = zigzagVector.y / zigzagVector.x;
        }

        public override void MovementBehavior(IMovable _movable)
        {
            var rb = _movable.GetRigidBody();
            var startTime = _movable.GetStartTime();

            //Get current zig-zag direction
            int periods = Mathf.RoundToInt((Time.time - startTime) / period);
            Vector2 direction = zigzagVector.normalized;
            direction.x *= (periods % 2 == 0 ? 1 : -1);
            direction = OrientVector(_movable, direction);

            rb.velocity += direction.normalized * moveSpeed;
        }


    }
}