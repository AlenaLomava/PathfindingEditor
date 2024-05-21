using System.Collections.Generic;

namespace Assets.Scripts.Field
{
    public interface IField
    {
        int Rows { get; }

        int Columns { get; }

        IEnumerable<Cell> GetAllCells();

        IEnumerable<Cell> GetCachedNeighbors(Cell cell);

        Cell GetCell(int row, int column);

        bool IsInBounds(int row, int col);

        void SetCellTraversable(int row, int column, bool isTraversable);
    }
}
