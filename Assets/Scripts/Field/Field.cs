using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Field
{
    public class Field : IField
    {
        public int Rows { get; }

        public int Columns { get; }

        private Dictionary<(int, int), Cell> _cells;

        private Dictionary<Cell, List<Cell>> _neighborsCache;

        public Field(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            _cells = new Dictionary<(int, int), Cell>();
            _neighborsCache = new Dictionary<Cell, List<Cell>>();

            for (var row = 0; row < rows; row++)
            {
                for (var col = 0; col < columns; col++)
                {
                    var cell = new Cell(row, col, true);
                    _cells[(row, col)] = cell;
                }
            }

            foreach (var cell in _cells.Values)
            {
                _neighborsCache[cell] = GetNeighbors(cell);
            }
        }

        public Cell GetCell(int row, int column)
        {
            _cells.TryGetValue((row, column), out Cell cell);
            return cell;
        }

        public void SetCellTraversable(int row, int column, bool isTraversable)
        {
            if (_cells.TryGetValue((row, column), out Cell cell))
            {
                cell.SetTraversable(isTraversable);
                UpdateNeighborsCache(cell);
            }
        }

        public IEnumerable<Cell> GetAllCells()
        {
            return _cells.Values;
        }

        public IEnumerable<Cell> GetCachedNeighbors(Cell cell)
        {
            return _neighborsCache[cell];
        }

        public bool IsInBounds(int row, int col)
        {
            return row >= 0 && row < Rows && col >= 0 && col < Columns;
        }

        private void UpdateNeighborsCache(Cell cell)
        {
            _neighborsCache[cell] = GetNeighbors(cell);

            foreach (var neighbor in _neighborsCache[cell])
            {
                _neighborsCache[neighbor] = GetNeighbors(neighbor);
            }
        }

        private List<Cell> GetNeighbors(Cell cell)
        {
            var neighbors = new List<Cell>();

            var directions = new (int, int)[]
            {
                (-1, 0),
                (1, 0),
                (0, -1),
                (0, 1)
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
    }
}
