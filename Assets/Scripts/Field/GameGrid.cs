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
                cell.SetPassable(isPassable);
            }
        }

        public IEnumerable<Cell> GetAllCells()
        {
            return _cells.Values;
        }
    }
}
