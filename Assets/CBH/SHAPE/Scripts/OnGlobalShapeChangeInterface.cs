namespace CBH.SHAPE
{
    /// <summary>
    /// Implementers react to a changing of the global shape type
    /// </summary>
    public interface IOnGlobalShapeChange
    {
        void ShapeChanged(ShapeTypeSO _newShape);
    }
}
