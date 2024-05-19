using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Field
{
    public class GameGridView : MonoBehaviour
    {
        [SerializeField]
        private GameObject _cellPrefab;

        [SerializeField]
        private Transform _gridParent;

        private Dictionary<Vector2Int, CellView> _cellViews = new Dictionary<Vector2Int, CellView>();

        public void UpdateGridRendering(GameGrid gameGrid, GameState gameState)
        {
            ClearGrid();
            var cells = gameGrid.GetAllCells();

            foreach (var cell in cells)
            {
                var cellObject = Instantiate(_cellPrefab, new Vector3(cell.Column, 0, cell.Row), Quaternion.identity, _gridParent); //Make pool?
                var cellView = cellObject.GetComponent<CellView>();
                cellView.Initialize(cell, gameState, _gridParent.position);
                _cellViews[new Vector2Int(cell.Row, cell.Column)] = cellView;
            }
        }

        public void UpdateCellRendering(Cell cell)
        {
            var cellView = FindCellView(cell.Row, cell.Column);
            if (cellView != null)
            {
                cellView.UpdateView();
            }
        }

        public void ClearGrid()
        {
            foreach (var cellView in _cellViews.Values)
            {
                Destroy(cellView.gameObject);
            }
            _cellViews.Clear();
        }

        private CellView FindCellView(int row, int column)
        {
            var key = new Vector2Int(row, column);
            if (_cellViews.TryGetValue(key, out var cellView))
            {
                return cellView;
            }
            return null;
        }
    }
}
