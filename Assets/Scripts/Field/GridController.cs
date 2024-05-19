using UnityEngine;

namespace Assets.Scripts.Field
{
    public class GridController
    {
        private readonly GameGridView _gridView;
        private readonly GameState _gameState;
        private GameGrid _grid;

        public GridController(GameGridView gridView, GameState gameState)
        {
            _gridView = gridView;
            _gameState = gameState;
        }

        public void Create(int rows, int columns, int obstaclesCount)
        {
            if (rows < 1 || columns < 1)
            {
                Debug.LogError("Invalid grid size. Rows and columns input must be greater than zero.");
                return;
            }

            if (obstaclesCount > rows * columns)
            {
                Debug.LogError("The number of obstacles exceeds the total number of cells.");
                return;
            }

            _grid = new GameGrid(rows, columns);

            GenerateObstacles(obstaclesCount);

            _gridView.UpdateGridRendering(_grid, _gameState);
        }

        public void SetCellPassable(int row, int column, bool isPassable)
        {
            if (_grid == null)
            {
                Debug.LogError("Grid is not initialized.");
                return;
            }

            var cell = _grid.GetCell(row, column);
            if (cell != null && cell.IsPassable != isPassable)
            {
                cell.SetPassable(isPassable);
                _gridView.UpdateCellRendering(cell);
            }
            else
            {
                Debug.LogError($"Cell at position ({row}, {column}) does not exist.");
            }
        }

        private void GenerateObstacles(int count)
        {
            var random = new System.Random();
            var placedObstacles = 0;

            while (placedObstacles < count)
            {
                var row = random.Next(_grid.Rows);
                var col = random.Next(_grid.Columns);

                var cell = _grid.GetCell(row, col);
                if (cell != null && cell.IsPassable)
                {
                    _grid.SetCellPassable(row, col, false);
                    placedObstacles++;
                }
            }
        }
    }
}
