namespace Assets.Scripts.Field
{
    public interface IFieldStorage
    {
        IField Field { get; }

        void Save(IField field);
    }
}
