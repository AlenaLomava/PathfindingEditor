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
        private ToggleButton _setTraversableButton;

        [SerializeField]
        private ToggleButton _pathfindingButton;

        [SerializeField]
        private ConsoleView _consoleView;

        private ToggleButtonPresenter _toggleButtonPresenter;
        private GenerateFieldButtonPresenter _generateFieldButtonPresenter;
        private ConsolePresenter _consolePresenter;
        private IStatesController _statesController;

        public void OnDisable()
        {
            _toggleButtonPresenter?.Dispose();
            _generateFieldButtonPresenter?.Dispose();
            _consolePresenter?.Dispose();
            _statesController.OnNoneState -= ResetButtons;
        }

        public void Initialize(IFieldGenerator fieldGenerator, IStatesController statesController)
        {
            _statesController = statesController;

            _toggleButtonPresenter = new ToggleButtonPresenter(
                _setObstacleButton, 
                _setTraversableButton, 
                _pathfindingButton, 
                statesController);

            _generateFieldButtonPresenter = new GenerateFieldButtonPresenter(
                _rowsInput, 
                _columnsInput, 
                _obstaclesInput,
                _generateFieldButton,
                fieldGenerator,
                statesController);

            _consolePresenter = new ConsolePresenter(_consoleView);

            _statesController.OnNoneState += ResetButtons;
        }

        public void ResetButtons()
        {
            _toggleButtonPresenter.ResetToggles();
        }
    }
}
