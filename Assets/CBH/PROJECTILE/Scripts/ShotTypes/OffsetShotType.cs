using UnityEngine;

namespace CBH.PROJECTILE
{
    /// <summary>
    /// Projecitle fires at angle
    /// </summary>
    [CreateAssetMenu(fileName = "OffsetShotType", menuName = "CBH/Shot Types/OffsetShotType", order = 1)]
    public class OffsetShotType : ShotTypeSO
    {
        [Header("Offset Shot Specific")]
        [SerializeField] float maxOffsetAngle = 5f;
        public override void FireShot(BaseProjectile projectile, int _shotNumber)
        {
            float offsetAngle = Random.Range(-maxOffsetAngle, maxOffsetAngle);
            projectile.SetUpShot(exitSpeed, offsetAngle);
        }
    }
}
