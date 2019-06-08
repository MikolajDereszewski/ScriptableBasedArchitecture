using UnityEngine;

namespace ScriptableBasedArchitecture.Example
{
    public class Player : MonoBehaviour
    {
        [SerializeField]
        private ScriptableFloat _playerHP;
        [SerializeField]
        private ScriptableInt _points;
        [SerializeField]
        private GameEventProperty _gameOverEvent;

        private void Awake()
        {
            _playerHP.Value = _playerHP.DefaultValue;
        }

        private void Update()
        {
            if (Input.GetKeyDown("e"))
            {
                _points.Value++;
            }
        }

        private void OnMouseDown()
        {
            _playerHP.Value -= 10;
            if (_playerHP.Value <= 0)
            {
                _gameOverEvent.Raise();
            }
        }
    }
}