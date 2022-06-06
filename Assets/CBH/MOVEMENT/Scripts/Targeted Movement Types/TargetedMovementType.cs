using UnityEngine;

namespace CBH.MOVEMENT
{
    /// <summary>
    /// These Movement Types move in relation to a predetermined target
    /// </summary>
    public abstract class TargetedMovementType : MovementTypeSO
    {
        public override void MovementBehavior(IMovable _movable)
        {
            var target = _movable.GetTarget();
            if(target == null)
            {
                NoTargetWarning(_movable);
            }
            else
            {
                TargetedBehavior(_movable.GetRigidBody(), target);
            }
        }

        protected abstract void TargetedBehavior(Rigidbody2D _rb, Transform _target);

        protected void NoTargetWarning(IMovable _movable)
        {
            Debug.LogWarning($"{name} requires target, but no target was found");
            _movable.Break();
        }
    }
}