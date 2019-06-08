using System;
using UnityEngine;

namespace ScriptableBasedArchitecture
{
    public interface IScriptableVariable<T>
    {
        T DefaultValue { get; }
        T Value { get; set; }
        event Action<T> ValueChanged;
    }

    public interface ISaveableVariable
    {
        bool IsLoaded { get; }
        void LoadVariable();
        void SaveVariable();
    }

    [Serializable]
    public class ScriptableVariable<T> : ScriptableObject, IScriptableVariable<T>, ISerializationCallbackReceiver, ISaveableVariable, IScriptableEventSender
    {
        public bool IsLoggingEvent { get { return _isLogged; } }
        public bool IsLoaded { get { return _isLoaded; } protected set { _isLoaded = value; } }
        public T DefaultValue { get { return _defaultValue; } }
        public T Value
        {
            get
            {
                if (_isSavedToPrefs && !IsLoaded)
                {
                    LoadVariable();
                }
                return _value;
            }
            set
            {
                if (_isSavedToPrefs && !IsLoaded)
                {
                    LoadVariable();
                }
                _value = value;
                if (_isSavedToPrefs)
                {
                    SaveVariable();
                }
                RaiseEvent();
            }
        }

        [SerializeField]
        [Label("Save Variable to Prefs?")]
        protected bool _isSavedToPrefs = false;
        [SerializeField]
        [Label("Log event raising?")]
        protected bool _isLogged = false;
        [SerializeField]
        protected T _defaultValue;
        [SerializeField]
        [GreyOut]
        protected T _value;

        [NonSerialized]
        protected bool _isLoaded = false;

        public virtual void LoadVariable()
        {
            IsLoaded = true;
        }

        public virtual void SaveVariable() { }

        public void RaiseEvent()
        {
            if(IsLoggingEvent)
            {
                Debugger.LogEventRaising("VALUE CHANGED", name);
            }
            OnValueChanged();
        }

        public event Action<T> ValueChanged;
        private void OnValueChanged()
        {
            if (ValueChanged != null)
                ValueChanged(_value);
        }

        public void OnAfterDeserialize()
        {
            if (!_isSavedToPrefs)
            {
                _value = _defaultValue;
            }
        }

        public void OnBeforeSerialize() { }
    }
}
