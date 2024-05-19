using Assets.Scripts.Field;
using Assets.Scripts.States;
using Assets.Scripts.UI;
using UnityEngine;

namespace Assets.Scripts
{
    public class Main : MonoBehaviour
    {
        [SerializeField]
        public UIController _uiController;

        [SerializeField]
        public GameGridView _gameGridView;

        [SerializeField]
        public SelectableController _selectableController;

        private GridController _gridController;

        void Start()
        {
            var statesController = new FieldEditorStatesController(_selectableController, _gameGridView);
            _gridController = new GridController(_gameGridView);
            _uiController.Initialize(_gridController, statesController);
        }
    }
}
