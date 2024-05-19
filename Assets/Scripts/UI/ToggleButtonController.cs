using UnityEngine;

namespace Assets.Scripts.UI
{
    public class ToggleButtonController : MonoBehaviour
    {
        [SerializeField]
        private ToggleButton _toggleButton1;

        [SerializeField]
        private ToggleButton _toggleButton2;

        private void OnEnable()
        {
            _toggleButton1.OnToggleChanged += HandleToggleChanged;
            _toggleButton2.OnToggleChanged += HandleToggleChanged;
        }

        private void OnDisable()
        {
            _toggleButton1.OnToggleChanged -= HandleToggleChanged;
            _toggleButton2.OnToggleChanged -= HandleToggleChanged;
        }

        private void HandleToggleChanged(ToggleButton toggledButton)
        {
            if (toggledButton == _toggleButton1 && _toggleButton1.IsToggled)
            {
                _toggleButton2.SetToggled(false);
            }
            else if (toggledButton == _toggleButton2 && _toggleButton2.IsToggled)
            {
                _toggleButton1.SetToggled(false);
            }
        }
    }
}
