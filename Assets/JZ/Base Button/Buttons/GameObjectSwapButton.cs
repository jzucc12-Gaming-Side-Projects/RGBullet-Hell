using UnityEngine;

namespace JZ.BUTTONS.FUNCTION
{
    /// <summary>
    /// Button that deactivates on object
    /// while activating another
    /// </summary>
    public class GameObjectSwapButton : ButtonFunction
    {
        [SerializeField] private GameObject goToClose = null;
        [SerializeField] private GameObject goToOpen = null;
        public override void OnClick()
        {
            goToClose.SetActive(false);
            goToOpen.SetActive(true);
        }
    }
}
