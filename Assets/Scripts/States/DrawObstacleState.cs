using Assets.Scripts.Field;
using UnityEngine;

namespace Assets.Scripts.Config
{
    public class DrawObstacleState : IFieldEditorState
    {
        private readonly ISelectableController _selectableController;

        public DrawObstacleState(ISelectableController selectableController)
        {
            _selectableController = selectableController;

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
                cellView.SetTraversable(false);
            }
            else
            {
                Debug.LogWarning("Current clicked object is not a CellView.");
            }
        }
    }
}
