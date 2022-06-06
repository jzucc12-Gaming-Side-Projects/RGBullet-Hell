using UnityEngine;

namespace CBH.PROJECTILE
{
    /// <summary>
    /// Fires set number of projectiles in an arc
    /// </summary>
    [CreateAssetMenu(fileName = "ArcShotType", menuName = "CBH/Shot Types/ArcShotType", order = 2)]
    public class ArcShotType : ShotTypeSO
    {
        [Header("Arc Shot Specific")]
        [Tooltip("In degrees")] [SerializeField] private float maxOffsetAngle = 30f;
        
        [Tooltip("Set to 0 for projectiles to be evenly spaced from each other")] 
        [SerializeField] private float angleDeiviation = 1f;


        public override void FireShot(BaseProjectile projectile, int _shotNumber)
        {
            float offsetAngle = -maxOffsetAngle + _shotNumber * GetIncrementAngle();
            offsetAngle += Random.Range(-angleDeiviation, angleDeiviation);
            projectile.SetUpShot(exitSpeed, offsetAngle);
        }

        private float GetIncrementAngle()
        {
            return 2 * maxOffsetAngle / (projectilesPerShot - 1);
        }
    }
}
