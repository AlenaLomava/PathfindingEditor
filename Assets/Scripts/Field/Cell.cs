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

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Cell other = (Cell)obj;
            return Row == other.Row && Column == other.Column && IsTraversable == other.IsTraversable;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + Row.GetHashCode();
                hash = hash * 23 + Column.GetHashCode();
                hash = hash * 23 + IsTraversable.GetHashCode();
                return hash;
            }
        }

        public static bool operator ==(Cell left, Cell right)
        {
            if (ReferenceEquals(left, null))
            {
                return ReferenceEquals(right, null);
            }

            return left.Equals(right);
        }

        public static bool operator !=(Cell left, Cell right)
        {
            return !(left == right);
        }
    }
}
