using UnityEngine;

namespace CBH.CORE
{
    /// <summary>
    /// Sets inversion mode
    /// </summary>
    public class Inverter : MonoBehaviour
    {
        public void Invert(bool _invert)
        {
            GameSettings.inversion = _invert;   
        }
    }
}
