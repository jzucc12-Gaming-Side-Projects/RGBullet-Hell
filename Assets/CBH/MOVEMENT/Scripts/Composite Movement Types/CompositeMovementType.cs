using UnityEngine;

namespace CBH.MOVEMENT
{
    /// <summary>
    /// Allows for combination of movement types
    /// </summary>
    public abstract class CompositeMovementType : MovementTypeSO
    {
        [SerializeField] protected MovementTypeSO[] movementTypes;
    }
}