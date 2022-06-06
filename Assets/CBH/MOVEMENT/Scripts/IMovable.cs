using UnityEngine;

namespace CBH.MOVEMENT
{
    /// <summary>
    /// Interface placed on classes that use Movement Types.
    /// Provides information to the movement type independent of its user
    /// </summary>
    public interface IMovable
    {
        Rigidbody2D GetRigidBody();
        Transform GetTarget();
        float GetStartTime();
        Quaternion GetOrientation();
        void Break();
    }
}
