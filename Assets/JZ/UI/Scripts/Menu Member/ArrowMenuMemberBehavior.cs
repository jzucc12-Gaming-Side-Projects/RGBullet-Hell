using CBH.SHAPE;
using UnityEngine;
using UnityEngine.UI;

namespace JZ.UI.MENU.MEMBER
{
    /// <summary>
    /// Shows arrow when member is hovered over
    /// </summary>
    public class ArrowMenuMemberBehavior : MenuMemberBehavior
    {
        [SerializeField] private Image arrowImage = null;
        private Color arrowColor = Color.clear;
        [SerializeField] private ShapeTypeSO shape = null;


        protected override void Awake() 
        {
            arrowColor = arrowImage.color;
            base.Awake();
        }

        protected override void OnHover(bool _active)
        {
            Color newColor = _active ? shape.GetColor() : Color.clear;
            arrowImage.color = newColor;
        }
    }
}