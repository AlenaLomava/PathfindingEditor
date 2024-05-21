namespace Assets.Scripts.Field
{
    public interface IFieldController
    {
        void ClearField();

        CellView GetCellView(int row, int column);

        void UpdateFieldRendering(Field field);
    }
}
