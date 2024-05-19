using Assets.Scripts.Field;
using Assets.Scripts.States;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class UIController : MonoBehaviour
    {
        [SerializeField]
        private TMP_InputField _rowsInput;

        [SerializeField]
        private TMP_InputField _columnsInput;

        [SerializeField]
        private TMP_InputField _obstaclesInput;

        [SerializeField]
        private Button _generateFieldButton;

        [SerializeField]
        private ToggleButton _setObstacleButton;

        [SerializeField]
        private ToggleButton _setPassableButton;

        [SerializeField]
        private Button _pathfindingButton;

        private ToggleButtonPresenter _toggleButtonPresenter;
        private GenerateFieldButtonPresenter _generateFieldButtonPresenter;
        private PathfindingButtonPresenter _pathfindingButtonPresenter;

        public void OnDisable()
        {
            _toggleButtonPresenter?.Dispose();
            _generateFieldButtonPresenter?.Dispose();
            _pathfindingButtonPresenter?.Dispose();
        }

        public void Initialize(GridController gridController, IFieldEditorStatesController statesController)
        {
            _toggleButtonPresenter = new ToggleButtonPresenter(_setObstacleButton, _setPassableButton, statesController);

            _generateFieldButtonPresenter = new GenerateFieldButtonPresenter(
                _rowsInput, 
                _columnsInput, 
                _obstaclesInput,
                _generateFieldButton,
                gridController);

            _pathfindingButtonPresenter = new PathfindingButtonPresenter(_pathfindingButton, statesController);
        }
    }
}
