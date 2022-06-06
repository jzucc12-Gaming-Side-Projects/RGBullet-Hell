using UnityEngine;

namespace CBH.PROJECTILE
{
    /// <summary>
    /// Projectile holds still
    /// </summary>
    [CreateAssetMenu(fileName = "StillShotType", menuName = "CBH/Shot Types/StillShotType", order = 0)]
    public class StillShotType : ShotTypeSO
    {
        public override void FireShot(BaseProjectile _projectile, int _shotNumber)
        {
            
        }
    }
}
