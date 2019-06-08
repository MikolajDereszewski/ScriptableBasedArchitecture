using UnityEngine;
using UnityEditor;

namespace ScriptableBasedArchitecture
{
    [CustomPropertyDrawer(typeof(GreyOutAttribute))]
    public class GreyOutDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            GUI.enabled = false;
            EditorGUI.PropertyField(position, property, label, true);
            GUI.enabled = true;
        }
    }

    [CustomPropertyDrawer(typeof(LabelAttribute))]
    public class LabelDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            LabelAttribute labelAttribute = attribute as LabelAttribute;
            label.text = labelAttribute.label;
            EditorGUI.PropertyField(position, property, label, true);
        }
    }

    [CustomPropertyDrawer(typeof(GameEventProperty))]
    public class GameEventPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            var buttonRect = new Rect(position.x, position.y, 125, position.height);
            var eventRect = new Rect(position.x + 125, position.y, position.width - 125, position.height);

            if (GUI.Button(buttonRect, "Raise Event"))
            {
                property.GetValue<GameEventProperty>().Raise();
            }
            EditorGUI.PropertyField(eventRect, property.FindPropertyRelative("_gameEvent"), GUIContent.none);

            EditorGUI.indentLevel = indent;
            EditorGUI.EndProperty();
        }
    }
}