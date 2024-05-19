using Assets.Scripts.Field;
using UnityEngine;

namespace Assets.Scripts.States
{
    public class DrawTraversableState : IFieldEditorState
    {
        private readonly ISelectableController _selectableController;

        public DrawTraversableState(ISelectableController selectableController)
        {
            _selectableController = selectableController;

            _selectableController.OnClicked += HandleClicked;
        }

        public void Dispose()
        {
            _selectableController.OnClicked -= HandleClicked;
        }

        public void HandleClicked(ISelectable selectable)
        {
            if (_selectableController.CurrentClicked is CellView cellView)
            {
                cellView.SetTraversable(true);
            }
            else
            {
                Debug.LogWarning("Current clicked object is not a CellView.");
            }
        }
    }
}
