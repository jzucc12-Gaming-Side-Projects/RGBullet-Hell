using UnityEngine;

namespace CBH.MOVEMENT
{
    /// <summary>
    /// Movement types are modular movement behaviours for enemies and weapons
    /// </summary>
    public abstract class MovementTypeSO : ScriptableObject 
    {
        public abstract void MovementBehavior(IMovable _movable);
    }
}