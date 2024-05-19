using UnityEngine;

namespace Assets.Scripts.Field
{
    public class GridController
    {
        private readonly GameGridView _gridView;
        private GameGrid _grid;

        public GridController(GameGridView gridView)
        {
            _gridView = gridView;
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

            _gridView.UpdateGridRendering(_grid);
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
                if (cell != null && cell.IsTraversable)
                {
                    _grid.SetCellPassable(row, col, false);
                    placedObstacles++;
                }
            }
        }
    }
}
