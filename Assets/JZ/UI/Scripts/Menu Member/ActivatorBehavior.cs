using UnityEngine;

namespace JZ.UI.MENU.MEMBER
{
    /// <summary>
    /// Activates objects when member is hovered over
    /// </summary>
    public class ActivatorBehavior : MenuMemberBehavior
    {
        [SerializeField] private GameObject[] objectsToActivate = new GameObject[0];



        protected override void OnHover(bool _active)
        {
            foreach(var go in objectsToActivate)
                go.SetActive(_active);
        }
    }
}