using System;

namespace Assets.Scripts.UI
{
    public class ToggleButtonPresenter : IDisposable
    {
        private readonly ToggleButton _setObstacleButton;
        private readonly ToggleButton _setPassableButton;
        private readonly GameState _gameState;

        public ToggleButtonPresenter(ToggleButton setObstacleButton, ToggleButton setPassableButton, GameState gameState)
        {
            _setObstacleButton = setObstacleButton;
            _setPassableButton = setPassableButton;
            _gameState = gameState;

            _setObstacleButton.OnToggleChanged += OnToggleButtonChanged;
            _setPassableButton.OnToggleChanged += OnToggleButtonChanged;
        }

        public void Dispose()
        {
            _setObstacleButton.OnToggleChanged -= OnToggleButtonChanged;
            _setPassableButton.OnToggleChanged -= OnToggleButtonChanged;
        }

        private void OnToggleButtonChanged(ToggleButton toggleButton)
        {
            if (_setObstacleButton.IsToggled)
            {
                _gameState.CellClickMode = CellClickMode.TurnIntoObstacle;
            }
            else if (_setPassableButton.IsToggled)
            {
                _gameState.CellClickMode = CellClickMode.TurnIntoPassable;
            }
            else
            {
                _gameState.CellClickMode = CellClickMode.None;
            }
        }
    }
}
