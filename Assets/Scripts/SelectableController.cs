using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts
{
    public class SelectableController : MonoBehaviour
    {
        private ISelectable _previousSelectable;
        private ISelectable _currentSelectable;

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
                        _previousSelectable = _currentSelectable;
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
            _currentSelectable?.Click();
        }

        private void DeselectCurrent()
        {
            if (_currentSelectable != null)
            {
                _currentSelectable.Deselect();
                _previousSelectable = _currentSelectable;
                _currentSelectable = null;
            }
        }
    }
}
