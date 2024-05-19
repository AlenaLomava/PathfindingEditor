using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts
{
    public class SelectableController : MonoBehaviour, ISelectableController
    {
        private ISelectable _previousClicked;
        private ISelectable _currentClicked;
        private ISelectable _currentSelectable;

        public ISelectable PreviousClicked => _previousClicked;

        public ISelectable CurrentClicked => _currentClicked;

        public event Action<ISelectable> OnClicked;

        private void Update()
        {
            HandleMouseHover();

            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                HandleMouseClick();
            }
        }

        private void HandleMouseHover()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.TryGetComponent(out ISelectable selectable))
                {
                    if (_currentSelectable != selectable)
                    {
                        _currentSelectable?.Deselect();
                        _currentSelectable = selectable;
                        _currentSelectable.Select();
                    }
                }
                else
                {
                    DeselectCurrent();
                }
            }
            else
            {
                DeselectCurrent();
            }
        }

        private void HandleMouseClick()
        {
            _previousClicked = _currentClicked;

            if (_currentSelectable != null)
            {
                _currentClicked = _currentSelectable;
                OnClicked?.Invoke(_currentClicked);
            }
            else
            {
                _currentClicked = null;
            }
        }

        private void DeselectCurrent()
        {
            if (_currentSelectable != null)
            {
                _currentSelectable.Deselect();
                _currentSelectable = null;
            }
        }
    }
}
