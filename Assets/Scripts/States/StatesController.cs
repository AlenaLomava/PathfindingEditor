using Assets.Scripts.Field;
using System;

namespace Assets.Scripts.States
{
    public class StatesController : IStatesController
    {
        private readonly ISelectableController _selectableController;
        private readonly IFieldStorage _fieldStorage;
        private readonly IFieldController _fieldController;
        private readonly AgentController _agentController;
        private IState _currentState;

        public event Action OnNoneState;

        public StatesController(
            ISelectableController selectableController,
            IFieldStorage fieldStorage,
            IFieldController fieldController,
            AgentController agentController)
        {
            _selectableController = selectableController;
            _fieldStorage = fieldStorage;
            _fieldController = fieldController;
            _agentController = agentController;

            SetNoneState();
        }

        public void SetDrawObstacleState()
        {
            SetState(new DrawObstacleState(_selectableController, _fieldStorage.Field));
        }

        public void SetDrawTraversableState()
        {
            SetState(new DrawTraversableState(_selectableController, _fieldStorage.Field));
        }

        public void SetPathfindingState()
        {
            SetState(new PathfindingState(_selectableController, this));
        }

        public void SetPathfindingVisualizeState()
        {
            SetState(new PathfindingVisualizeState(_selectableController, _fieldStorage.Field, _fieldController, this, _agentController));
        }

        public void SetNoneState()
        {
            OnNoneState?.Invoke();
            SetState(new NoneState());
        }

        private void SetState(IState state)
        {
            _currentState?.Dispose();
            _currentState = state;
        }
    }
}
