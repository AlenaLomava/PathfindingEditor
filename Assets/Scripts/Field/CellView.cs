using Assets.Scripts.Config;
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

        private Color DefaultColor => _cell.IsPassable ? _passableColor : _nonPassableColor;

        private Cell _cell;
        private GameState _gameState;

        private bool _isSelected = false;

        public int Row => _cell.Row;

        public int Column => _cell.Column;

        public void Initialize(Cell cell, GameState gameState, Vector3 rootPosition)
        {
            _cell = cell;
            _gameState = gameState;
            SetPosition(rootPosition);
            UpdateView();
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

        public void Click()
        {
            switch (_gameState.CellClickMode)
            {
                case CellClickMode.TurnIntoObstacle:
                    ChangeCellPassable(false);
                    break;
                case CellClickMode.TurnIntoPassable:
                    ChangeCellPassable(true);
                    break;
                default:
                    Debug.LogWarning("Unsupported CellClickMode");
                    break;
            }
        }

        private void ChangeCellPassable(bool isPassable)
        {
            _cell.SetPassable(isPassable);
            UpdateView();
        }

        public void UpdateView()
        {
            var outlineThikness = _isSelected ? 0.1f : 0f;
            _renderer.material.SetFloat("_Outline", outlineThikness);
            _renderer.material.color = DefaultColor;
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
                 rootPosition.x + (Row * Constants.SPACE_BETWEEN_CELLS),
                 0,
                 rootPosition.z + (Column * Constants.SPACE_BETWEEN_CELLS));
        }
    }
}
