using UnityEditor;
using UnityEngine;

namespace CBH.MOVEMENT
{
    /// <summary>
    /// Is not dependant upon target
    /// </summary>
    public abstract class FreeMovementType : MovementTypeSO
    {
        #region //Free movement variables
        [Tooltip("If true, travels in the direction the user's rigidbody is facing.")]
        [HideInInspector, SerializeField] protected bool moveWithRBOrientation = true;

        [Tooltip("Unit vector representing the direction the user will move")]
        [HideInInspector, SerializeField, VectorMinMax(0,1)] protected Vector2 movementDirection = Vector2.one;

        [Tooltip("Angle the user will travel in with respect to the positive-x axis. Determined by the movementDirection variable")]
        [HideInInspector, SerializeField, ReadOnly] protected float movementAngle;
        #endregion


        protected virtual void OnValidate()
        {
            movementAngle = Mathf.Atan2(movementDirection.y, movementDirection.x) * Mathf.Rad2Deg;
        }

        protected Vector2 OrientVector(IMovable _movable, Vector2 _baseDirection)
        {
            if(moveWithRBOrientation)
                return _movable.GetOrientation() * _baseDirection;
            else
            {
                float angle = Vector2.SignedAngle(Vector2.up, movementDirection.normalized);
                return Quaternion.Euler(0,0,angle) * _baseDirection;
            }
        }
    }

    #region //Custom Editor
#if UNITY_EDITOR
    [CustomEditor(typeof(FreeMovementType), true)]
    public class FreeMovementTypeEditor : Editor
    {
        #region //Properties
        private SerializedProperty moveWithRBOrientationProperty = null;
        private SerializedProperty movementDirectionProperty = null;
        private SerializedProperty movementAngleProperty = null;
        #endregion


        private void OnEnable()
        {
            moveWithRBOrientationProperty = serializedObject.FindProperty("moveWithRBOrientation");
            movementDirectionProperty = serializedObject.FindProperty("movementDirection");
            movementAngleProperty = serializedObject.FindProperty("movementAngle");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            base.OnInspectorGUI();

            EditorGUILayout.LabelField(" ");
            EditorGUILayout.LabelField("Free movement variables", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(moveWithRBOrientationProperty);
            if (!moveWithRBOrientationProperty.boolValue)
            {
                EditorGUILayout.PropertyField(movementDirectionProperty);
                EditorGUILayout.PropertyField(movementAngleProperty);
            }
            serializedObject.ApplyModifiedProperties();
        }
    }
#endif
    #endregion
}
