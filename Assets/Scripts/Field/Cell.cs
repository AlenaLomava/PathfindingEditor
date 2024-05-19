namespace Assets.Scripts.Field
{
    public class Cell
    {
        public int Row { get; }

        public int Column { get; }

        public bool IsPassable { get; private set; }

        public Cell(int row, int column, bool isPassable)
        {
            Row = row;
            Column = column;
            IsPassable = isPassable;
        }

        public void SetPassable(bool isPassable)
        {
            IsPassable = isPassable;
        }

        public override string ToString()
        {
            return $"Row: {Row}, Column: {Column}, IsPassable: {IsPassable}.";
        }
    }
}
