using Assets.Scripts.Config;
using Assets.Scripts.Field;

namespace Assets.Scripts.States
{
    public class FieldEditorStatesController : IFieldEditorStatesController
    {
        private readonly ISelectableController _selectableController;
        private readonly GameGridView _gameGridView;
        private IFieldEditorState _currentState;

        public FieldEditorStatesController(ISelectableController selectableController, GameGridView gameGridView)
        {
            _selectableController = selectableController;
            _gameGridView = gameGridView;
            SetNoneState();
        }

        public void SetDrawObstacleState()
        {
            SetState(new DrawObstacleState(_selectableController));
        }

        public void SetDrawTraversableState()
        {
            SetState(new DrawTraversableState(_selectableController));
        }

        public void SetPathfindingStartState()
        {
            SetState(new PathfindingStartState(_selectableController, this));
        }

        public void SetPathfindingEndState()
        {
            SetState(new PathfindingEndState(_selectableController, this));
        }

        public void SetPathfindingDrawState()
        {
            SetState(new PathfindingDrawState(_selectableController, _gameGridView));
        }

        public void SetPathfindingClearState()
        {

        }

        public void SetNoneState()
        {
            SetState(new NoneState());
        }

        private void SetState(IFieldEditorState state)
        {
            _currentState?.Dispose();
            _currentState = state;
        }
    }
}
