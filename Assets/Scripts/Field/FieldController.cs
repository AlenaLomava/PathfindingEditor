using Assets.Scripts.Pool;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Field
{
    public class FieldController : MonoBehaviour, IFieldController
    {
        private const int POOL_INITAL_SIZE = 1000;

        [SerializeField]
        private GameObject _cellPrefab;

        [SerializeField]
        private Transform _fieldRoot;

        private Dictionary<(int, int), CellView> _cellViews = new Dictionary<(int, int), CellView>();

        private void Awake()
        {
            PoolManager.SetRoot(_fieldRoot);
            PoolManager.WarmPool(_cellPrefab, POOL_INITAL_SIZE);
        }

        public void UpdateFieldRendering(Field field)
        {
            ClearField();
            var cells = field.GetAllCells();

            foreach (var cell in cells)
            {
                var cellObject = PoolManager.SpawnObject(_cellPrefab);
                cellObject.transform.position = new Vector3(cell.Column, 0, cell.Row);
                var cellView = cellObject.GetComponent<CellView>();
                cellView.Initialize(cell, _fieldRoot.position);
                _cellViews[(cell.Row, cell.Column)] = cellView;
            }
        }

        public void ClearField()
        {
            foreach (var cellView in _cellViews.Values)
            {
                PoolManager.ReleaseObject(cellView.gameObject);
            }

            _cellViews.Clear();
        }

        public CellView GetCellView(int row, int column)
        {
            if (_cellViews.TryGetValue((row, column), out CellView cellView))
            {
                return cellView;
            }

            return null;
        }
    }
}
