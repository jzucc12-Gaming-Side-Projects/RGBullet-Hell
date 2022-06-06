using UnityEngine;

namespace CBH.ENEMY.MOVEMENT
{
    /// <summary>
    /// Determines aiming direction for enemy body or weapons
    /// </summary>
    public class EnemyAimer : MonoBehaviour
    {
        #region //Targeting variables
        [SerializeField] private bool aimAtPlayer = true;
        [Tooltip("Leave empty to target nothing")] [SerializeField, HideIf("aimAtPlayer")] private Transform aimTarget;
        [Tooltip("Degrees per second")] [SerializeField] private float aimSpeed = 10f;
        private float targetingAngle = 0;
        #endregion

        #region //Ping Pong variables
        [Tooltip("Causes forward direction to rotate between a minimum and maximum value")] [SerializeField] private bool doesPingPong = false;
        [Tooltip("In degrees")] [SerializeField, ShowIf("doesPingPong")] private float minOffset = -20f;
        [Tooltip("In degrees")] [SerializeField, ShowIf("doesPingPong")] private float maxOffset = 20f;
        [Tooltip("Degrees per second")][SerializeField, ShowIf("doesPingPong")] private float pingPongSpeed = 30f;
        private float pingPongAngle = 0;
        #endregion


        #region //Monobehaviour
        private void Start()
        {
            if(aimAtPlayer)
            {
                aimTarget = GameObject.FindGameObjectWithTag("Player").transform;
            }
        }

        private void LateUpdate()
        {
            //Aim won't change while paused
            if(Time.timeScale == 0) return;
            PingPong();
            AimAtTarget(aimTarget);
            SetAimAngle(targetingAngle + pingPongAngle);
        }
        #endregion

        #region //Aim Enemy
        private void AimAtTarget(Transform _target)
        {
            if(_target != null)
            {
                Vector2 distanceFromTarget = aimTarget.position - transform.position;
                float angleToTarget = Vector2.SignedAngle(Vector2.up, distanceFromTarget);
                float currentAimSpeed = aimSpeed * GameSettings.GetGameSpeed() * Time.deltaTime;
                float newAngle = Mathf.MoveTowardsAngle(targetingAngle, angleToTarget, currentAimSpeed);
                targetingAngle = newAngle;
            }
            else
            {
                targetingAngle = 0;
            }
        }

        private void PingPong()
        {
            if(doesPingPong)
            {
                float currentValue = Time.time * pingPongSpeed;
                float range = maxOffset - minOffset;
                pingPongAngle = Mathf.PingPong(currentValue, range) + minOffset;
            }
            else
            {
                pingPongAngle = 0;
            }
        }

        private void SetAimAngle(float _angle)
        {
            transform.eulerAngles = new Vector3(0, 0, _angle);
        }
        #endregion
    }
}
