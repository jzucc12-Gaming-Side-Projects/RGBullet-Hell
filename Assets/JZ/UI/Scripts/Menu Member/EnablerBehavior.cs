using UnityEngine;

namespace JZ.UI.MENU.MEMBER
{
    /// <summary>
    /// Enables component when member is hovered over
    /// </summary>
    public class EnablerBehavior : MenuMemberBehavior
    {
        [SerializeField] private MonoBehaviour[] behavioursToEnable = new MonoBehaviour[0];



        protected override void OnHover(bool _active)
        {
            foreach(var behaviour in behavioursToEnable)
                behaviour.enabled = _active;
        }
    }
}