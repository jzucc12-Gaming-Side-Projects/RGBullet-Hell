using UnityEngine;

namespace JZ.UI.MENU
{
    /// <summary>
    /// Common parent to all menus
    /// </summary>
    public class MenuUI : MonoBehaviour
    {
        public virtual void LockControl(bool _lock) { }
    }
}