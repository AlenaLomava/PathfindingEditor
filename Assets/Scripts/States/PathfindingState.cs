using Assets.Scripts.Field;
using UnityEngine;

namespace Assets.Scripts.States
{
    public class PathfindingState : IState
    {
        private readonly ISelectableController _selectableController;
        private readonly IStatesController _statesController;
        private CellView _startCell;
        private CellView _endCell;

        public PathfindingState(ISelectableController selectableController, IStatesController statesController)
        {
            _selectableController = selectableController;
            _statesController = statesController;

            _selectableController.OnClicked += HandleClicked;
        }

        public void Dispose()
        {
            _startCell?.ClearPathText();
            _endCell?.ClearPathText();
            _selectableController.OnClicked -= HandleClicked;
        }

        private void HandleClicked(ISelectable selectable)
        {
            if (selectable is CellView cellView)
            {
                if (_startCell == null)
                {
                    HandleStartCellSelection(cellView);
                }
                else if (_endCell == null)
                {
                    HandleEndCellSelection(cellView);
                }
            }
        }

        private void HandleStartCellSelection(CellView cellView)
        {
            if (IsValid(cellView))
            {
                _startCell = cellView;
                _startCell.SetPathStartPointText();
            }
            else
            {
                HandleInvalidPathPoint();
            }
        }

        private void HandleEndCellSelection(CellView cellView)
        {
            if (IsValid(cellView) && _startCell.Data != cellView.Data)
            {
                _endCell = cellView;
                _statesController.SetPathfindingVisualizeState();
            }
            else
            {
                HandleInvalidPathPoint();
            }
        }

        private bool IsValid(CellView cell)
        {
            return cell.Data.IsTraversable;
        }

        private void HandleInvalidPathPoint()
        {
            Debug.LogWarning("Invalid path point.");
            _statesController.SetNoneState();
        }
    }
}
