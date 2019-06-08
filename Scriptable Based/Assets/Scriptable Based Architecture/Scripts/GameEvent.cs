using System;
using UnityEngine;

namespace ScriptableBasedArchitecture
{
    public interface IGameEvent
    {
        string Message { get; }
        event Action<IGameEvent, string> EventRaised;
    }

    [Serializable]
    [CreateAssetMenu(fileName = "GameEvent", menuName = "Scriptable Based/Game Event", order = 2)]
    public class GameEvent : ScriptableObject, IGameEvent, IScriptableEventSender
    {
        public bool IsLoggingEvent { get { return _isLogged; } }
        public string Message { get { return _message; } }

        [SerializeField]
        [Label("Log event raising?")]
        protected bool _isLogged = false;
        [SerializeField]
        [Label("Event Message")]
        private string _message;

        public void RaiseEvent()
        {
            if (IsLoggingEvent)
            {
                Debugger.LogEventRaising(Message, name);
            }
            OnEventRaised();
        }

        public event Action<IGameEvent, string> EventRaised;
        private void OnEventRaised()
        {
            if (EventRaised != null)
                EventRaised(this, Message);
        }
    }

#if UNITY_EDITOR
    [UnityEditor.CustomEditor(typeof(GameEvent))]
    public class GameEventEditor : ScriptableEventSenderEditor { }
#endif
}