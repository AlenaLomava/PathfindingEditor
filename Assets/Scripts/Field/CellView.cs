using Assets.Scripts.Config;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Field
{
    public class CellView : MonoBehaviour, ISelectable
    {
        [SerializeField]
        private Renderer _renderer;

        [SerializeField]
        private Color _passableColor;

        [SerializeField]
        private Color _nonPassableColor;

        [SerializeField]
        private Color _drawPathColor;

        [SerializeField]
        private TextMeshPro _pathText;

        private Color DefaultColor => _cell.IsTraversable ? _passableColor : _nonPassableColor;

        private Cell _cell;

        private bool _isSelected = false;

        public Cell Data => _cell;

        public void Initialize(Cell cell, Vector3 rootPosition)
        {
            _cell = cell;

            SetPosition(rootPosition);
            UpdateView();
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

        public void DrawPath()
        {
            _renderer.material.color = _drawPathColor;
        }

        public void SetTraversable(bool isTraversable)
        {
            _cell.SetTraversable(isTraversable);
            UpdateView();
        }

        public void UpdateView()
        {
            var outlineThikness = _isSelected ? 0.1f : 0f;
            _renderer.material.SetFloat("_Outline", outlineThikness);
            _renderer.material.color = DefaultColor;
        }

        public void SetPathStartPoint()
        {
            _pathText.SetText("START");
        }

        public void SetPathEndPoint()
        {
            _pathText.SetText("END");
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
                UpdateView();
            }
        }

        private void SetPosition(Vector3 rootPosition)
        {
            transform.position = new Vector3(
                 rootPosition.x + (_cell.Row * Constants.SPACE_BETWEEN_CELLS),
                 0,
                 rootPosition.z + (_cell.Column * Constants.SPACE_BETWEEN_CELLS));
        }
    }
}
