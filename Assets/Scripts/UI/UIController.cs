using Assets.Scripts.Field;
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

        private ToggleButtonPresenter _toggleButtonPresenter;
        private GenerateFieldButtonPresenter _generateFieldButtonPresenter;

        public void OnDisable()
        {
            _toggleButtonPresenter?.Dispose();
            _generateFieldButtonPresenter?.Dispose();
        }

        public void Initialize(GridController gridController, GameState gameState)
        {
            _toggleButtonPresenter = new ToggleButtonPresenter(_setObstacleButton, _setPassableButton, gameState);
            _generateFieldButtonPresenter = new GenerateFieldButtonPresenter(
                _rowsInput, 
                _columnsInput, 
                _obstaclesInput,
                _generateFieldButton,
                gridController);
        }
    }
}
