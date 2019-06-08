using UnityEngine;
using UnityEngine.UI;

namespace ScriptableBasedArchitecture.Example
{
    [RequireComponent(typeof(Text))]
    public class PlayerPointCounter : MonoBehaviour
    {
        [SerializeField]
        private ScriptableInt _points;

        private Text _text;

        private void Awake()
        {
            _text = GetComponent<Text>();
            _points.RegisterEventListener(OnValueChanged);
            OnValueChanged(_points.Value);
        }

        private void OnDestroy()
        {
            _points.UnregisterEventListener(OnValueChanged);
        }

        private void OnValueChanged(int value)
        {
            _text.text = value.ToString();
        }
    }
}