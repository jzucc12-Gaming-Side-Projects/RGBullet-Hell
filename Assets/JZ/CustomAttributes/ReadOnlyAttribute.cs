using UnityEngine;

public class ReadOnlyAttribute : PropertyAttribute
{
    /// <summary>
    /// Attribute can't be edited in the inspector
    /// </summary>
    public ReadOnlyAttribute()
    {

    }
}