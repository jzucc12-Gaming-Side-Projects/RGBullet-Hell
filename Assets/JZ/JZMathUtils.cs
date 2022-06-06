using UnityEngine;

/// <summary>
/// Extra math functions that could be helpful
/// </summary>
public static class JZMathUtils
{
    /// <summary>
    /// If "value" is outside the boundaries, will return a value
    /// that wraps between the boundaries.
    /// </summary>
    /// <param name="value"></param>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    public static float Wrap(float value, float min, float max)
    {
        float range = max - min + 1;
        while(value > max)
            value -= range;
            
        while(value < min)
            value += range;

        return value;
    }


    /// <summary>
    /// If "value" is outside the boundaries, will return a value
    /// that wraps between the boundaries.
    /// </summary>
    /// <param name="value"></param>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    public static int Wrap(int value, int min, int max)
    {
        float val = Wrap((float)value, (float)min, (float)max);
        return (int)val;
    }

    /// <summary>
    /// Finds the linear velocity of a point on a circle from its
    /// angular speed
    /// </summary>
    /// <param name="point"></param>
    /// <param name="center"></param>
    /// <param name="angularSpeed"></param>
    /// <returns></returns>
    public static Vector2 AngularToLinearVelocity(Vector2 point, Vector2 center, float angularSpeed)
    {
        Vector2 radiusVector = point - center;
        float radius = radiusVector.magnitude;
        float angle = Mathf.Atan2(radiusVector.y, radiusVector.x);
        float dAngle = angularSpeed * Mathf.Deg2Rad * Time.deltaTime;

        float newAngle = angle + dAngle;
        float x = center.x + radius * Mathf.Cos(newAngle);
        float y = center.y + radius * Mathf.Sin(newAngle);
        Vector2 endPoint = new Vector2(x,y);
        return (endPoint - point) / Time.deltaTime;
    }
}
