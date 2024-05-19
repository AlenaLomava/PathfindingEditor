using System.Collections.Generic;

namespace Assets.Scripts.Field
{
    public class GameGrid
    {
        public int Rows { get; }

        public int Columns { get; }

        private Dictionary<(int, int), Cell> _cells;

        public GameGrid(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            _cells = new Dictionary<(int, int), Cell>();

            for (var row = 0; row < rows; row++)
            {
                for (var col = 0; col < columns; col++)
                {
                    _cells[(row, col)] = new Cell(row, col, true);
                }
            }
        }

        public Cell GetCell(int row, int column)
        {
            _cells.TryGetValue((row, column), out Cell cell);
            return cell;
        }

        public void SetCellPassable(int row, int column, bool isPassable)
        {
            if (_cells.TryGetValue((row, column), out Cell cell))
            {
                cell.SetTraversable(isPassable);
            }
        }

        public IEnumerable<Cell> GetAllCells()
        {
            return _cells.Values;
        }

        public IEnumerable<Cell> GetNeighbors(Cell cell)
        {
            var neighbors = new List<Cell>();

            var directions = new (int, int)[]
            {
            (-1, 0), // Up
            (1, 0),  // Down
            (0, -1), // Left
            (0, 1)   // Right
            };

            foreach (var direction in directions)
            {
                int newRow = cell.Row + direction.Item1;
                int newCol = cell.Column + direction.Item2;

                if (IsInBounds(newRow, newCol) && GetCell(newRow, newCol)?.IsTraversable == true)
                {
                    neighbors.Add(GetCell(newRow, newCol));
                }
            }

            return neighbors;
        }

        public bool IsInBounds(int row, int col)
        {
            return row >= 0 && row < Rows && col >= 0 && col < Columns;
        }
    }
}
