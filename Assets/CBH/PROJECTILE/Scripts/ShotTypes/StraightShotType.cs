using UnityEngine;

namespace CBH.PROJECTILE
{
    /// <summary>
    /// Projectile fires in a straight line
    /// </summary>
    [CreateAssetMenu(fileName = "StraightShotType", menuName = "CBH/Shot Types/StraightShotType", order = 0)]
    public class StraightShotType : ShotTypeSO
    {
        public override void FireShot(BaseProjectile projectile, int _shotNumber)
        {
            projectile.SetUpShot(exitSpeed, 0);
        }
    }
}