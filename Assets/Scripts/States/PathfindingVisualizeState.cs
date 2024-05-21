using Assets.Scripts.Field;
using Assets.Scripts.Pathfinding;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.States
{
    public class PathfindingVisualizeState : IState
    {
        private readonly ISelectableController _selectableController;
        private readonly IField _field;
        private readonly IFieldController _fieldController;
        private readonly StatesController _statesController;
        private readonly AgentController _agentController;

        private List<Cell> _path = new List<Cell>();

        public PathfindingVisualizeState(
            ISelectableController selectableController,
            IField field,
            IFieldController fieldController,
            StatesController statesController,
            AgentController agentController)
        {
            _selectableController = selectableController;
            _field = field;
            _fieldController = fieldController;
            _statesController = statesController;
            _agentController = agentController;

            HandleClick();
        }

        public void Dispose()
        {
            _agentController.StopMovement();

            if (_path != null)
            {
                foreach (var cell in _path)
                {
                    _fieldController.GetCellView(cell.Row, cell.Column).SetDefaultColor();
                }
            }
        }

        public void HandleClick()
        {
            if (_selectableController.PreviousClicked is CellView start && _selectableController.CurrentClicked is CellView end)
            {
                var (pathAStar, nodesAStar) = PathfindingAStar.FindPath(_field, start.Data, end.Data);

                Debug.Log($"A* explored nodes: {nodesAStar}");

                _path = pathAStar;

                if (_path == null)
                {
                    Debug.LogWarning("Path not found.");
                    _statesController.SetNoneState();
                    return;
                }

                foreach (var cell in _path)
                {
                    _fieldController.GetCellView(cell.Row, cell.Column).SetPathColor();
                }

                _agentController.MoveAgentAlongPath(_path, _fieldController);
            }
            else
            {
                HandleInvalidPathPoint();
            }
        }

        private void HandleInvalidPathPoint()
        {
            Debug.LogWarning("Invalid path start or end point.");
            _statesController.SetNoneState();
        }
    }
}
