using System;
using UnityEngine;

namespace ScriptableBasedArchitecture
{
    public class ScriptableVariableProperty<T>
    {
        protected IScriptableVariable<T> _variable;

        public virtual IScriptableVariable<T> Variable { get { return _variable; } }
        public T DefaultValue
        {
            get
            {
                if (Variable == null)
                {
                    Debugger.LogNullError("Default Value", "Variable in ScriptableProperty");
                    return default(T);
                }
                return Variable.DefaultValue;
            }
        }
        public T Value
        {
            get
            {
                if (Variable == null)
                {
                    Debugger.LogNullError("Value", "Variable in ScriptableProperty");
                    return default(T);
                }
                return Variable.Value;
            }
            set
            {
                if (Variable == null)
                {
                    Debugger.LogNullError("Value", "Variable in ScriptableProperty");
                    return;
                }
                Variable.Value = value;
            }
        }

        public void RegisterEventListener(Action<T> onValueChanged)
        {
            if (Variable == null)
            {
                Debugger.LogNullError("event listener", "register", "Variable in ScriptableProperty");
                return;
            }
            Variable.ValueChanged += onValueChanged;
        }

        public void UnregisterEventListener(Action<T> onValueChanged)
        {
            if (Variable == null)
            {
                Debugger.LogNullError("event listener", "unregister", "Variable in ScriptableProperty");
                return;
            }
            Variable.ValueChanged -= onValueChanged;
        }
    }

#if UNITY_EDITOR
    public class ScriptableVariablePropertyDrawer<T> : UnityEditor.PropertyDrawer
    {
        public override void OnGUI(Rect position, UnityEditor.SerializedProperty property, GUIContent label)
        {
            UnityEditor.EditorGUI.BeginProperty(position, label, property);
            position = UnityEditor.EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
            int indent = UnityEditor.EditorGUI.indentLevel;
            UnityEditor.EditorGUI.indentLevel = 0;

            Rect valueRect = new Rect(position.x, position.y, 75, position.height);
            Rect variableRect = new Rect(position.x + 75, position.y, position.width - 75, position.height);

            GUI.enabled = false;
            UnityEngine.Object obj = property.FindPropertyRelative("_variable").objectReferenceValue;
            UnityEditor.SerializedObject variable = obj == null ? null : new UnityEditor.SerializedObject(obj);
            if (variable != null)
            {
                UnityEditor.EditorGUI.PropertyField(valueRect, variable.FindProperty("_value"), GUIContent.none);
            }
            else
            {
                UnityEditor.EditorGUI.TextField(valueRect, "");
            }
            GUI.enabled = true;

            UnityEditor.EditorGUI.PropertyField(variableRect, property.FindPropertyRelative("_variable"), GUIContent.none);

            UnityEditor.EditorGUI.indentLevel = indent;
            UnityEditor.EditorGUI.EndProperty();
        }
    }
#endif
}