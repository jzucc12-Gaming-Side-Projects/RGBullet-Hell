using CBH.CORE;
using UnityEngine.InputSystem;

namespace CBH.PLAYER.ABILITY
{
    /// <summary>
    /// Change game speed from player input
    /// </summary>
    public class PlayerChangeGameSpeed : PlayerAbilityDataContainer
    {
        #region //Input actions
        private InputAction gameSpeedInput = null;
        private ChangeGameSpeed gameSpeedChanger = null;
        #endregion

        
        #region //Monobehaviour
        protected override void Awake()
        {
            base.Awake();
            gameSpeedInput = inputSystem.gameSpeedInput;
        }

        private void Start()
        {
            gameSpeedChanger = FindObjectOfType<ChangeGameSpeed>();
        }

        private void Update()
        {
            var input = gameSpeedInput.ReadValue<float>();
            if(input != 0)
                gameSpeedChanger.IncrementScale(input);
        }
        #endregion
    }
}