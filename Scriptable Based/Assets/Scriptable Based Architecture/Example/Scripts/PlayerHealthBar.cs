using UnityEngine;
using UnityEngine.UI;

namespace ScriptableBasedArchitecture.Example
{
    [RequireComponent(typeof(Image))]
    public class PlayerHealthBar : MonoBehaviour
    {
        [SerializeField]
        private ScriptableFloat _playerHP;

        private Image _image;
        private float _startValue;

        private void Awake()
        {
            _startValue = _playerHP.DefaultValue;
            _image = GetComponent<Image>();
            _playerHP.RegisterEventListener(OnValueChanged);
        }

        private void OnDestroy()
        {
            _playerHP.UnregisterEventListener(OnValueChanged);
        }

        private void OnValueChanged(float value)
        {
            _image.fillAmount = value / _startValue;
        }
    }
}