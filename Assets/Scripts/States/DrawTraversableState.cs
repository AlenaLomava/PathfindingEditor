using Assets.Scripts.Field;

namespace Assets.Scripts.States
{
    public class DrawTraversableState : DrawState
    {
        public DrawTraversableState(ISelectableController selectableController, IField field)
        : base(selectableController, field)
        {
        }

        protected override void HandleCellClicked(CellView cellView)
        {
            _field.SetCellTraversable(cellView.Data.Row, cellView.Data.Column, true);
            cellView.UpdateView();
        }
    }
}
