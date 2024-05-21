using Assets.Scripts.Field;
using UnityEngine;

namespace Assets.Scripts.States
{
    public abstract class DrawState : IState
    {
        protected readonly ISelectableController _selectableController;
        protected readonly IField _field;

        protected DrawState(ISelectableController selectableController, IField field)
        {
            _selectableController = selectableController;
            _field = field;

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
                HandleCellClicked(cellView);
            }
            else
            {
                Debug.LogWarning("Current clicked object is not a CellView.");
            }
        }

        protected abstract void HandleCellClicked(CellView cellView);
    }
}
