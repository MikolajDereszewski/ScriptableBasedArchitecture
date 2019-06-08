using UnityEngine;

namespace ScriptableBasedArchitecture
{
    public interface IScriptableEventSender
    {
        bool IsLoggingEvent { get; }
        void RaiseEvent();
    }

#if UNITY_EDITOR
    public class ScriptableEventSenderEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            if (!(target is Object))
            {
                UnityEditor.EditorGUILayout.HelpBox("The object connected to ScriptableEventSenderEditor is not a UnityEngine.Object! Custom inspector will not draw",
                    UnityEditor.MessageType.Error);
                return;
            }
            if (!(target is IScriptableEventSender))
            {
                UnityEditor.EditorGUILayout.HelpBox("The object connected to ScriptableEventSenderEditor is not a IScriptableEventSender! Custom inspector will not draw",
                    UnityEditor.MessageType.Error);
                return;
            }
            UnityEditor.EditorGUILayout.BeginVertical(GUI.skin.box);
            UnityEditor.EditorGUILayout.LabelField((target as Object).name + " Properties", UnityEditor.EditorStyles.boldLabel);
            UnityEditor.EditorGUILayout.Space();
            base.OnInspectorGUI();
            UnityEditor.EditorGUILayout.Space();
            if (GUILayout.Button("Raise Event"))
            {
                (target as IScriptableEventSender).RaiseEvent();
            }
            UnityEditor.EditorGUILayout.EndVertical();
        }
    }
#endif
}