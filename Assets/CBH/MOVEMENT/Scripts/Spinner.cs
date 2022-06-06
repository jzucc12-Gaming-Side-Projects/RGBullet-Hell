using UnityEngine;

namespace CBH.MOVEMENT
{
    /// <summary>
    /// Spins target rigidbody at a fixed rate
    /// </summary>
    public class Spinner : MonoBehaviour
    {
        [SerializeField] private bool spinClockwise = true;
        [Tooltip("Degrees per second")] [SerializeField] private float spinSpeed = 90f;
        private Rigidbody2D rb = null;
        protected float baseSpinAmount => spinSpeed * (spinClockwise ? -1 : 1);
        protected


        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        protected virtual void FixedUpdate()
        {
            float spinAmount = baseSpinAmount * Time.deltaTime;
            SpinTarget(spinAmount);
        }

        private void SpinTarget(float _spinAmount)
        {
            rb.SetRotation(rb.rotation + _spinAmount);
        }
    }
}