using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(VectorMinMaxAttribute))]
public class VectorMinMaxDrawer: PropertyDrawer 
{
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUI.GetPropertyHeight(property, label, true);
    }
    
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) 
    {
        VectorMinMaxAttribute vmm = (VectorMinMaxAttribute)attribute;
        if(property.propertyType == SerializedPropertyType.Vector2)
        {
            Vector2 vector = property.vector2Value;
            vector.x = BringFloatInRange(vector.x, vmm.min, vmm.max);
            vector.y = BringFloatInRange(vector.y, vmm.min, vmm.max);
            property.vector2Value = vector;
        }
        else if(property.propertyType == SerializedPropertyType.Vector2Int)
        {
            Vector2Int vector = property.vector2IntValue;
            vector.x = BringIntInRange(vector.x, vmm.min, vmm.max);
            vector.y = BringIntInRange(vector.y, vmm.min, vmm.max);
            property.vector2IntValue = vector;
        }
        else if(property.propertyType == SerializedPropertyType.Vector3)
        {
            Vector3 vector = property.vector3Value;
            vector.x = BringFloatInRange(vector.x, vmm.min, vmm.max);
            vector.y = BringFloatInRange(vector.y, vmm.min, vmm.max);
            vector.z = BringFloatInRange(vector.z, vmm.min, vmm.max);
            property.vector3Value = vector;
        }
        else if(property.propertyType == SerializedPropertyType.Vector3Int)
        {
            Vector3Int vector = property.vector3IntValue;
            vector.x = BringIntInRange(vector.x, vmm.min, vmm.max);
            vector.y = BringIntInRange(vector.y, vmm.min, vmm.max);
            vector.z = BringIntInRange(vector.z, vmm.min, vmm.max);
            property.vector3IntValue = vector;
        }
        else if (property.propertyType == SerializedPropertyType.Vector4)
        {
            Vector4 vector = property.vector4Value;
            vector.x = BringFloatInRange(vector.x, vmm.min, vmm.max);
            vector.y = BringFloatInRange(vector.y, vmm.min, vmm.max);
            vector.z = BringFloatInRange(vector.z, vmm.min, vmm.max);
            vector.w = BringFloatInRange(vector.w, vmm.min, vmm.max);
            property.vector4Value = vector;
        }
        else
        {
            Debug.LogWarning($"{property.name} needs to be a vector");
        }

        EditorGUI.PropertyField(position, property, label, true);
    }

    private float BringFloatInRange(float _number, float _min, float _max)
    {
        if(_number < _min) return _min;
        if(_number > _max) return _max;
        return _number;
    }

    private int BringIntInRange(int _number, float _min, float _max)
    {
        if(_number < _min) return (int)_min;
        if(_number > _max) return (int)_max;
        return _number;
    }
}

