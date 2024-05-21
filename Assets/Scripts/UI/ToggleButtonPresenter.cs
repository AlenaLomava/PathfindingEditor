using Assets.Scripts.States;
using System;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class ToggleButtonPresenter : IDisposable
    {
        private readonly ToggleButton _setObstacleButton;
        private readonly ToggleButton _setTraversableButton;
        private readonly ToggleButton _pathfindingButton;
        private readonly IStatesController _statesController;

        public ToggleButtonPresenter(
            ToggleButton setObstacleButton, 
            ToggleButton setTraversableButton, 
            ToggleButton pathfindingButton,
            IStatesController statesController)
        {
            _setObstacleButton = setObstacleButton;
            _setTraversableButton = setTraversableButton;
            _pathfindingButton = pathfindingButton;
            _statesController = statesController;

            _setObstacleButton.OnToggleChanged += OnToggleButtonChanged;
            _setTraversableButton.OnToggleChanged += OnToggleButtonChanged;
            _pathfindingButton.OnToggleChanged += OnToggleButtonChanged;
        }

        public void Dispose()
        {
            _setObstacleButton.OnToggleChanged -= OnToggleButtonChanged;
            _setTraversableButton.OnToggleChanged -= OnToggleButtonChanged;
            _pathfindingButton.OnToggleChanged-= OnToggleButtonChanged;
        }

        public void ResetToggles()
        {
            _setObstacleButton.SetToggled(false);
            _setTraversableButton.SetToggled(false);
            _pathfindingButton.SetToggled(false);
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
            else if (_pathfindingButton.IsToggled)
            {
                _statesController.SetPathfindingState();
                Debug.Log("Pathfinding mode activated");
            }
            else
            {
                _statesController.SetNoneState();
            }
        }
    }
}
