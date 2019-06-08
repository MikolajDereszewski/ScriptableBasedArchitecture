using UnityEngine;

namespace ScriptableBasedArchitecture.Example
{
    public class EndGameScreen : MonoBehaviour
    {
        [SerializeField]
        private GameEventProperty _gameEvent;
        [SerializeField]
        private GameObject _gameOverScreen;

        private void Awake()
        {
            _gameEvent.RegisterEventListener(OnEventRaised);
        }

        private void OnDestroy()
        {
            _gameEvent.UnregisterEventListener(OnEventRaised);
        }

        private void OnEventRaised(IGameEvent eventSent, string message)
        {
            _gameOverScreen.SetActive(true);
        }
    }
}