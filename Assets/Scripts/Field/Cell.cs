namespace Assets.Scripts.Field
{
    public class Cell
    {
        public int Row { get; }

        public int Column { get; }

        public bool IsTraversable { get; private set; }

        public Cell(int row, int column, bool isTraversable)
        {
            Row = row;
            Column = column;
            IsTraversable = isTraversable;
        }

        public void SetTraversable(bool isTraversable)
        {
            IsTraversable = isTraversable;
        }

        public override string ToString()
        {
            return $"Row: {Row}, Column: {Column}, IsTraversable: {IsTraversable}.";
        }
    }
}
