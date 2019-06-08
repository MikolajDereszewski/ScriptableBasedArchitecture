using System;
using UnityEngine;

namespace ScriptableBasedArchitecture
{
    [Serializable]
    [CreateAssetMenu(fileName = "FloatVariable", menuName = "Scriptable Based/Float Variable", order = 0)]
    public class FloatVariable : ScriptableVariable<float>
    {
        public override void LoadVariable()
        {
            _value = PlayerPrefs.GetFloat("FLOAT_VAR_" + name + '_' + GetHashCode().ToString(), _defaultValue);
            base.LoadVariable();
        }

        public override void SaveVariable()
        {
            PlayerPrefs.SetFloat("FLOAT_VAR_" + name + '_' + GetHashCode().ToString(), _value);
            base.SaveVariable();
        }
    }

    [Serializable]
    public class ScriptableFloat : ScriptableVariableProperty<float>
    {
        [SerializeField]
        protected new FloatVariable _variable;

        public override IScriptableVariable<float> Variable
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
    [UnityEditor.CustomPropertyDrawer(typeof(ScriptableFloat))]
    public class ScriptableFloatDrawer : ScriptableVariablePropertyDrawer<float> { }

    [UnityEditor.CustomEditor(typeof(FloatVariable))]
    public class ScriptableFloatEditor : ScriptableEventSenderEditor { }
#endif
}