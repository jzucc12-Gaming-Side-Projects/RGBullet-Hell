using CBH.SHAPE;
using UnityEngine;

namespace CBH.WEAPON.UI
{
    /// <summary>
    /// Ui for entire weapon clip. Modifies specific AmmoUI
    /// Necessary due to colors of a shape being changeable at any time
    /// </summary>
    public class ClipUI : MonoBehaviour
    {
        private AmmoUI[] allAmmoUI = new AmmoUI[0];
        [SerializeField] ShapeTypeSO myShape = null;


        private void Awake()
        {
            allAmmoUI = GetComponentsInChildren<AmmoUI>();
        }

        public void UpdateClipUI(int _clipAmount)
        {
            for(int ii = 0; ii < allAmmoUI.Length; ii++)
            {
                int ammoIndex = allAmmoUI.Length - 1 - ii; //So the UI shows ammo disappearing from top to bottom
                if(ii < _clipAmount)
                    allAmmoUI[ammoIndex].ReplenishAmmo();
                else
                    allAmmoUI[ammoIndex].DepleteAmmo();
            }
        }

        public ShapeTypeSO GetShape() { return myShape; }
    }
}
