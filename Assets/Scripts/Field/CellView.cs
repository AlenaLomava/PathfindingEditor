using Assets.Scripts.Config;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Field
{
    public class CellView : MonoBehaviour, ISelectable
    {
        private const string START_POINT_TEXT = "START";
        private const string OUTLINE_NAME = "_Outline";

        [SerializeField]
        private Renderer _renderer;

        [SerializeField]
        private Color _traversableColor;

        [SerializeField]
        private Color _obstacleColor;

        [SerializeField]
        private Color _drawPathColor;

        [SerializeField]
        private TextMeshPro _pathText;

        private Cell _cell;

        private bool _isSelected = false;

        private Color DefaultColor => _cell.IsTraversable ? _traversableColor : _obstacleColor;

        public Cell Data => _cell;

        public void Initialize(Cell cell, Vector3 rootPosition)
        {
            _cell = cell;

            SetPosition(rootPosition);
            UpdateView();
            SetOutline();
            ClearPathText();
        }

        public override string ToString()
        {
            return _cell.ToString();
        }

        public void Deselect()
        {
            SetSelected(false);
        }

        public void Select()
        {
            SetSelected(true);
        }

        public void SetPathColor()
        {
            _renderer.material.color = _drawPathColor;
        }

        public void SetDefaultColor()
        {
            _renderer.material.color = DefaultColor;
        }

        public void UpdateView()
        {
            SetDefaultColor();
        }

        public void SetPathStartPointText()
        {
            _pathText.SetText(START_POINT_TEXT);
        }

        public void ClearPathText()
        {
            _pathText.SetText(string.Empty);
        }

        private void SetSelected(bool isSelected)
        {
            if (_isSelected != isSelected)
            {
                _isSelected = isSelected;
                SetOutline();
            }
        }

        private void SetOutline()
        {
            var outlineThickness = _isSelected ? 0.1f : 0f;
            _renderer.material.SetFloat(OUTLINE_NAME, outlineThickness);
        }

        private void SetPosition(Vector3 rootPosition)
        {
            transform.position = new Vector3(
                 rootPosition.x + (_cell.Row * Constants.Field.SPACE_BETWEEN_CELLS),
                 0,
                 rootPosition.z + (_cell.Column * Constants.Field.SPACE_BETWEEN_CELLS));
        }
    }
}
