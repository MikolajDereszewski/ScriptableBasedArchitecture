using System;
using UnityEngine;

namespace ScriptableBasedArchitecture
{
    [Serializable]
    [CreateAssetMenu(fileName = "IntVariable", menuName = "Scriptable Based/Int Variable", order = 1)]
    public class IntVariable : ScriptableVariable<int>
    {
        public override void LoadVariable()
        {
            _value = PlayerPrefs.GetInt("INTEGER_VAR_" + name + '_' + GetHashCode().ToString(), _defaultValue);
            base.LoadVariable();
        }

        public override void SaveVariable()
        {
            PlayerPrefs.SetInt("INTEGER_VAR_" + name + '_' + GetHashCode().ToString(), _value);
            base.SaveVariable();
        }
    }

    [Serializable]
    public class ScriptableInt : ScriptableVariableProperty<int>
    {
        [SerializeField]
        protected new IntVariable _variable;

        public override IScriptableVariable<int> Variable
        {
            get
            {
                if (_variable == null)
                {
                    Debugger.LogNullError("Variable", "ScriptableProperty");
                    return null;
                }
                return _variable;
            }
        }
    }

#if UNITY_EDITOR
    [UnityEditor.CustomPropertyDrawer(typeof(ScriptableInt))]
    public class ScriptableIntDrawer : ScriptableVariablePropertyDrawer<int> { }

    [UnityEditor.CustomEditor(typeof(IntVariable))]
    public class ScriptableIntEditor : ScriptableEventSenderEditor { }
#endif
}