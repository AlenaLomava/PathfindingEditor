using Assets.Scripts.States;
using System;

namespace Assets.Scripts.UI
{
    public class ToggleButtonPresenter : IDisposable
    {
        private readonly ToggleButton _setObstacleButton;
        private readonly ToggleButton _setTraversableButton;
        private readonly IFieldEditorStatesController _statesController;

        public ToggleButtonPresenter(
            ToggleButton setObstacleButton, 
            ToggleButton setTraversableButton, 
            IFieldEditorStatesController statesController)
        {
            _setObstacleButton = setObstacleButton;
            _setTraversableButton = setTraversableButton;
            _statesController = statesController;

            _setObstacleButton.OnToggleChanged += OnToggleButtonChanged;
            _setTraversableButton.OnToggleChanged += OnToggleButtonChanged;
        }

        public void Dispose()
        {
            _setObstacleButton.OnToggleChanged -= OnToggleButtonChanged;
            _setTraversableButton.OnToggleChanged -= OnToggleButtonChanged;
        }

        private void OnToggleButtonChanged(ToggleButton toggleButton)
        {
            if (_setObstacleButton.IsToggled)
            {
                _statesController.SetDrawObstacleState();
            }
            else if (_setTraversableButton.IsToggled)
            {
                _statesController.SetDrawTraversableState();
            }
            else
            {
                _statesController.SetNoneState();
            }
        }
    }
}
