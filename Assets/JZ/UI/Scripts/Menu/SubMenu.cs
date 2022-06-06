using UnityEngine;

namespace JZ.UI.MENU
{
    /// <summary>
    /// Menu that deactivates the menu that activated it
    /// Reactivates parent upon close
    /// </summary>
    public class SubMenu : MonoBehaviour
    {
        [SerializeField] private MenuUI parentMenu = null;
        [SerializeField] private GameObject blockingWindow = null;


        private void OnEnable()
        {
            blockingWindow.SetActive(true);
            parentMenu.LockControl(true);
        }

        private void OnDisable()
        {
            parentMenu.LockControl(false);
            blockingWindow.SetActive(false);
        }
    }
}
