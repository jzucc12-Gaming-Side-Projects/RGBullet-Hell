using UnityEngine;

namespace CBH.MOVEMENT
{
    /// <summary>
    /// Runs through each movment type in order
    /// </summary>
    [CreateAssetMenu(fileName = "SequenceMovementType", menuName = "CBH/Movement Types/Composite/SequenceMovementType", order = 0)]
    public class SequenceMovementType : CompositeMovementType
    {
        public override void MovementBehavior(IMovable _movable)
        {
            foreach(var movement in movementTypes)
                movement.MovementBehavior(_movable);
        }
    }
}
