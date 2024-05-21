using UnityEngine;

namespace Assets.Scripts.UI
{
    public class ToggleButtonController : MonoBehaviour
    {
        [SerializeField]
        private ToggleButton[] _toggleButtons;

        private void OnEnable()
        {
            foreach (var button in _toggleButtons)
            {
                button.OnToggleChanged += HandleToggleChanged;
            }
        }

        private void OnDisable()
        {
            foreach (var button in _toggleButtons)
            {
                button.OnToggleChanged -= HandleToggleChanged;
            }
        }

        private void HandleToggleChanged(ToggleButton toggledButton)
        {
            if (toggledButton.IsToggled)
            {
                foreach (var button in _toggleButtons)
                {
                    if (button != toggledButton)
                    {
                        button.SetToggled(false);
                    }
                }
            }
        }
    }
}
