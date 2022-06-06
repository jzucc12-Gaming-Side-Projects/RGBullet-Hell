using System.Linq;
using UnityEngine;

namespace CBH.SHAPE
{
    /// <summary>
    /// Keeps track of the active global color
    /// This should correspond to the player's current color
    /// </summary>
    
    [CreateAssetMenu(fileName = "GlobalShapeManager", menuName = "CBH/GlobalShapeManager", order = 0)]
    public class GlobalShapeManager : ScriptableObject 
    {
        [SerializeField] private ShapeTypeSO globalShape = null;

        public ShapeTypeSO GetGlobalShape() { return globalShape; }

        public void SetGlobalShape(ShapeTypeSO _newShape)
        {
            globalShape = _newShape;
            OnShapeChanged(_newShape);
        }

        private void OnShapeChanged(ShapeTypeSO _newShape)
        {
            var listeners = FindObjectsOfType<MonoBehaviour>().OfType<IOnGlobalShapeChange>();
            foreach(var listener in listeners)
            {
                listener.ShapeChanged(_newShape);
            }
        }
    }
}
