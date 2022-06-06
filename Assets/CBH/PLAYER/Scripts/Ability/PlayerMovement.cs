using UnityEngine;
using UnityEngine.InputSystem;

namespace CBH.PLAYER.ABILITY
{
    /// <summary>
    /// Move from player input
    /// </summary>
    public class PlayerMovement : PlayerAbilityDataContainer
    {
        #region //Input actions
        private InputAction movementInput = null;
        #endregion

        #region //Movement variables
        private Vector2 normalizedMovementVector = Vector2.zero;
        private Vector2 movementVector = Vector2.zero;
        [SerializeField] private float movementSpeed = 5f;
        #endregion

        
        #region //Monobehaviour
        protected override void Awake()
        {
            base.Awake();
            movementInput = inputSystem.movementInput;
        }

        private void Update()
        {
            normalizedMovementVector = movementInput.ReadValue<Vector2>();
        }

        private void FixedUpdate()
        {
            movementVector = normalizedMovementVector * movementSpeed * Time.deltaTime;
            var newPosition = movementVector + rb.position;
            rb.MovePosition(newPosition);
        }
        #endregion
    }
}