using Assets.Scripts.Field;
using Assets.Scripts.Pathfinding;
using UnityEngine;

namespace Assets.Scripts.States
{
    public class PathfindingDrawState : IFieldEditorState
    {
        private readonly ISelectableController _selectableController;
        private readonly GameGridView _gameGridView;

        public PathfindingDrawState(ISelectableController selectableController, GameGridView gameGridView)
        {
            _selectableController = selectableController;
            _gameGridView = gameGridView;
            HandleClick();
        }

        public void Dispose() { }

        public void HandleClick()
        {
            if (_selectableController.PreviousClicked is CellView start && _selectableController.PreviousClicked is CellView end)
            {
                var cells = PathfindingAStar.FindPathAStar(_gameGridView.GameGrid, start.Data, end.Data);
                //var cells = JPSPathfinding.FindPathJPS(_gameGridView.GameGrid, start.Data, end.Data);
                foreach (var cell in cells)
                {
                    _gameGridView.FindCellView(cell.Row, cell.Column).DrawPath();
                }
                end.ClearPathText();
                start.ClearPathText();
                Debug.LogWarning("Everything is OK!");
            }
        }
    }
}
