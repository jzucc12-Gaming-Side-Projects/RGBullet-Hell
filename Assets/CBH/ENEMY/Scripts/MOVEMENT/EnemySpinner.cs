using CBH.MOVEMENT;
using UnityEngine;

namespace CBH.ENEMY.MOVEMENT
{
    /// <summary>
    /// Necessary so they can be affected by game speed setting
    /// </summary>
    public class EnemySpinner : Spinner
    {
        protected override void FixedUpdate()
        {
            float spinAmount = baseSpinAmount * Time.deltaTime * GameSettings.GetGameSpeed();
            base.FixedUpdate();
        }
    }
}