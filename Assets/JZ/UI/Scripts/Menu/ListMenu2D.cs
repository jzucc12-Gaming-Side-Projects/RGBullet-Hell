using JZ.INPUT;
using UnityEngine;

namespace JZ.UI.MENU
{
    /// <summary>
    /// List menu where items are laid out in a grid
    /// </summary>
    public class ListMenu2D : ListMenuUI<MenuNavigator2D>
    {
        [SerializeField] private Vector2Int layout = new Vector2Int(0,0);
        [SerializeField] private bool loopX = false;
        [SerializeField] private bool loopY = false;

        protected override void Awake()
        {
            base.Awake();
            navigator = new MenuNavigator2D(members.Count, layout, loopX, loopY);
        }
    }
}
