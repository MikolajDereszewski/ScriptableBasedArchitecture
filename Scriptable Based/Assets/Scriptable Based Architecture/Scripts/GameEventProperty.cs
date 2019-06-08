using System;
using UnityEngine;

namespace ScriptableBasedArchitecture
{
    [Serializable]
    public class GameEventProperty
    {
        [SerializeField]
        private GameEvent _gameEvent;

        public IGameEvent GameEvent
        {
            get
            {
                if (_gameEvent == null)
                {
                    Debugger.LogNullError("GameEvent", "GameEventProperty");
                    return null;
                }
                return _gameEvent;
            }
        }

        public void RegisterEventListener(Action<IGameEvent, string> onEventRaised)
        {
            if(_gameEvent == null)
            {
                Debugger.LogNullError("event listener", "register", "GameEvent in GameEventProperty");
                return;
            }
            _gameEvent.EventRaised += onEventRaised;
        }

        public void UnregisterEventListener(Action<IGameEvent, string> onEventRaised)
        {
            if (_gameEvent == null)
            {
                Debugger.LogNullError("event listener", "unregister", "GameEvent in GameEventProperty");
                return;
            }
            _gameEvent.EventRaised -= onEventRaised;
        }

        public void Raise()
        {
            if (_gameEvent == null)
            {
                Debugger.LogNullError("GameEvent", "raise", "GameEventProperty");
                return;
            }
            _gameEvent.RaiseEvent();
        }
    }
}