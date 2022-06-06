using UnityEngine;

public class VectorMinMaxAttribute : PropertyAttribute
{
    /// <summary>
    /// Allows you to set limits on a given vector field
    /// </summary>
    public readonly float min;
    public readonly float max;
    public VectorMinMaxAttribute(float _min, float _max)
    {
        this.min = _min;
        this.max = _max;
    }
}