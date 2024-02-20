using System;
using UnityEngine;
# if UNITY_EDITOR
using UnityEditor;

[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
public class VisibleAttribute : PropertyAttribute
{
    public readonly bool Visible;
    
    public VisibleAttribute(bool visible)
    {
        Visible = visible;
    }
}

[CustomPropertyDrawer(typeof(VisibleAttribute))]
public class VisibleDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (property.propertyType == SerializedPropertyType.Boolean)
        {
            var visibleAttribute = (VisibleAttribute) attribute;
            if (visibleAttribute.Visible)
            {
                EditorGUI.PropertyField(position, property, label);
            }
        }
    }
}

[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
public class HideInSpecificClassAttribute : PropertyAttribute
{
    public readonly string ClassName;

    public HideInSpecificClassAttribute(string className)
    {
        ClassName = className;
    }
}

[CustomPropertyDrawer(typeof(HideInSpecificClassAttribute))]
public class HideInSpecificClassDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        HideInSpecificClassAttribute hideAttribute = (HideInSpecificClassAttribute)attribute;

        // フィールドを持つオブジェクトのクラス名を取得
        string className = property.serializedObject.targetObject.GetType().Name;

        // 特定のクラス名でなければフィールドを表示
        if (className != hideAttribute.ClassName)
        {
            EditorGUI.PropertyField(position, property, label, true);
        }
    }
}
#endif
