using Assets.Scripts.Field;

namespace Assets.Scripts.States
{
    public class PathfindingStartState : IFieldEditorState
    {
        private readonly ISelectableController _selectableController;
        private readonly IFieldEditorStatesController _statesController;

        public PathfindingStartState(ISelectableController selectableController, IFieldEditorStatesController statesController)
        {
            _selectableController = selectableController;
            _statesController = statesController;

            _selectableController.OnClicked += HandleClicked;
        }

        public void Dispose()
        {
            _selectableController.OnClicked -= HandleClicked;
        }

        private void HandleClicked(ISelectable selectable)
        {
            if (selectable is CellView cellView)
            {
                cellView.SetPathStartPoint();
            }
            _statesController.SetPathfindingEndState();
        }
    }
}
