using UnityEngine;

/// <summary>
/// Sets box colliders to border a polygon collider
/// Useful for setting player boundaries in relation to the camera boundaries
/// </summary>
public class AlignBoxCollidersWithPolygonCollider : MonoBehaviour
{
    #region //Cached Components
    [Header("Colliders")]
    [SerializeField] private PolygonCollider2D polygonCollider = null;
    [SerializeField] private BoxCollider2D leftBoxCollider = null;
    [SerializeField] private BoxCollider2D rightBoxCollider = null;
    [SerializeField] private BoxCollider2D topBoxCollider = null;
    [SerializeField] private BoxCollider2D bottomBoxCollider = null;
    #endregion

    #region //Offset
    [Header("Size and Positioning")]
    [Tooltip("Distance from polygon collider")]
    [SerializeField] private float offset = 0f;
    [SerializeField] private float boundaryWidth = 1f;
    [Tooltip("Amount boundary exceeds the polygon collider per side")]
    [SerializeField] private float overflow = 0f;
    #endregion

    
    #region //Monobehaviour
    private void OnValidate()
    {
        Start();
    }

    private void Start()
    {
        PositionPlayerBounds();
    }
    #endregion

    #region //Position player bounds around camera bounds
    public void PositionPlayerBounds()
    {
        SetupBoundary(leftBoxCollider, true, true);
        SetupBoundary(rightBoxCollider, true, false);
        SetupBoundary(topBoxCollider, false, true);
        SetupBoundary(bottomBoxCollider, false, false);
    }

    private void SetupBoundary(BoxCollider2D _collider, bool _isHorizontal, bool _topLeft)
    {
        //Determine indices to use based off boundary direction
        int mainIndex = _isHorizontal ? 0 : 1;
        int offIndex = 1 - mainIndex;

        //Determine which camera bound points to use as reference
        int firstPoint;
        int secondPoint;
        firstPoint = _topLeft ? 1 - mainIndex : 3 * mainIndex;
        secondPoint = _topLeft ? 2 - mainIndex : 3 - mainIndex;
        
        //Resize bound
        Vector2 newSize = Vector2.zero;
        newSize[mainIndex] = boundaryWidth;
        newSize[offIndex] = polygonCollider.points[firstPoint][offIndex] - polygonCollider.points[secondPoint][offIndex] + 2 * overflow;
        _collider.size = newSize;

        //Determine main axis positioning
        float halfSize = newSize[mainIndex]/2;
        float mainAxisOffset = halfSize + offset;
        mainAxisOffset *= (_topLeft ^ _isHorizontal) ? 1 : -1;
        
        //Reposition bound
        Vector2 newPosition = Vector2.zero;
        newPosition[mainIndex] = polygonCollider.points[firstPoint][mainIndex]  + mainAxisOffset;
        newPosition[offIndex] = (polygonCollider.points[firstPoint][offIndex] + polygonCollider.points[secondPoint][offIndex])/2;
        _collider.transform.localPosition = newPosition;
    }
    #endregion
}