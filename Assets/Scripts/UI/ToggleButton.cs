using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class ToggleButton : MonoBehaviour 
    {
        [SerializeField]
        private Button _button;

        [SerializeField]
        private Color _normalColor = Color.white;

        [SerializeField]
        private Color _toggledColor = Color.green;

        private bool _isToggled;

        public bool IsToggled => _isToggled;

        public event Action<ToggleButton> OnToggleChanged;

        private void OnEnable()
        {
            _button.onClick.AddListener(OnButtonClick);
            UpdateButtonVisual();
        }

        private void OnDisable()
        {
            _button.onClick.RemoveAllListeners();
        }

        public void SetToggled(bool isToggled)
        {
            if (_isToggled != isToggled)
            {
                _isToggled = isToggled;
                UpdateButtonVisual();
            }
        }

        private void OnButtonClick()
        {
            _isToggled = !_isToggled;
            UpdateButtonVisual();
            OnToggleChanged?.Invoke(this);
        }

        private void UpdateButtonVisual()
        {
            var colors = _button.colors;
            colors.normalColor = _isToggled ? _toggledColor : _normalColor;
            colors.selectedColor = _isToggled ? _toggledColor : _normalColor;
            _button.colors = colors;
        }
    }
}
