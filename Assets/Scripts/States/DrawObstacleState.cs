using Assets.Scripts.Field;

namespace Assets.Scripts.States
{
    public class DrawObstacleState : DrawState
    {
        public DrawObstacleState(ISelectableController selectableController, IField field)
        : base(selectableController, field)
        {
        }

        protected override void HandleCellClicked(CellView cellView)
        {
            _field.SetCellTraversable(cellView.Data.Row, cellView.Data.Column, false);
            cellView.UpdateView();
        }
    }
}
