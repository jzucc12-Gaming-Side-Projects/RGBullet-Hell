namespace CBH.SHAPE
{
    /// <summary>
    /// Implementers react to collisions with projectile shapes
    /// </summary>
    public interface IShapeCollision
    {
        void CollisionWithSameType(int _damage);
        void CollisionWithDifferentType(int _damage);
        ShapeTypeSO GetShapeType();
    }
}