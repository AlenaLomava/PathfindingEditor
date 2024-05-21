namespace Assets.Scripts.Field
{
    public class FieldStorage : IFieldStorage
    {
        public IField Field { get; private set; }

        public void Save(IField field)
        {
            Field = field;
        }
    }
}
