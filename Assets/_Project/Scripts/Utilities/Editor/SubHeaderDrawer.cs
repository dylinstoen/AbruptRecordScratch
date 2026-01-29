#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(SubHeaderAttribute))]
public class SubHeaderDrawer : PropertyDrawer {
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        // Header
        SubHeaderAttribute subHeader = (SubHeaderAttribute)attribute;
        float lineHeight = EditorGUIUtility.singleLineHeight;
        float spacing = 4f;
        Rect headerRect = new Rect(position.x, position.y, position.width, lineHeight);
        GUIStyle style = new GUIStyle(EditorStyles.label) {
            fontStyle = FontStyle.Normal,
            normal = { textColor = Color.gray }
        };
        EditorGUI.LabelField(headerRect, subHeader.text, style);

        // Property
        Rect fieldRect = new Rect(position.x, position.y + lineHeight + spacing, position.width, EditorGUI.GetPropertyHeight(property, true) );
        GUIContent propertyLabel = new GUIContent(property.displayName);
        EditorGUI.PropertyField(fieldRect, property, propertyLabel, true);
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
        float lineHeight = EditorGUIUtility.singleLineHeight;
        float spacing = 4f;
        return lineHeight + spacing + EditorGUI.GetPropertyHeight(property, true);
    }
}
# endif