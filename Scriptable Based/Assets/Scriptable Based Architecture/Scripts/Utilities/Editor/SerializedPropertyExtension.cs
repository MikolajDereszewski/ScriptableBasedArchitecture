using UnityEngine;
using UnityEditor;
using System.Reflection;

namespace ScriptableBasedArchitecture
{
    public static class SerializedPropertyExtensions
    {
        public static T GetValue<T>(this SerializedProperty property)
        {
            return GetNestedObject<T>(property.propertyPath, GetSerializedPropertyRootComponent(property));
        }
        
        public static Component GetSerializedPropertyRootComponent(SerializedProperty property)
        {
            return (Component)property.serializedObject.targetObject;
        }
        
        public static T GetNestedObject<T>(string path, object obj)
        {
            foreach (string part in path.Split('.'))
            {
                obj = GetFieldOrPropertyValue<object>(part, obj);
            }
            return (T)obj;
        }

        public static T GetFieldOrPropertyValue<T>(string fieldName, object obj, BindingFlags bindings = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
        {
            FieldInfo field = obj.GetType().GetField(fieldName, bindings);
            if (field != null)
            {
                return (T)field.GetValue(obj);
            }

            PropertyInfo property = obj.GetType().GetProperty(fieldName, bindings);
            if (property != null)
            {
                return (T)property.GetValue(obj, null);
            }

            return default(T);
        }
    }
}