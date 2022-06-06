using JZ.INPUT;
using UnityEngine.UI;
using UnityEngine;

namespace JZ.UI.MENU
{
    /// <summary>
    /// List menu where all items are on a single axis
    /// </summary>
    public class ListMenu1D : ListMenuUI<MenuNavigator1D>
    {
        [SerializeField] private bool loop = false;
        [SerializeField] private bool verticalLayout = true;


        protected override void Awake()
        {
            base.Awake();
            navigator = new MenuNavigator1D(members.Count, loop, verticalLayout);
        }

        protected override void Update()
        {
            base.Update();
            
            Slider activeSlider = activeMember.GetComponentInChildren<Slider>();
            if(activeSlider != null)
                activeSlider.value += navigator.GetOffAxisValue() * .01f;
        }
    }
}
