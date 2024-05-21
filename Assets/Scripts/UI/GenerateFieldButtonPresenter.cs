using Assets.Scripts.Field;
using Assets.Scripts.States;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class GenerateFieldButtonPresenter : IDisposable
    {
        private readonly TMP_InputField _rowsInput;
        private readonly TMP_InputField _columnsInput;
        private readonly TMP_InputField _obstaclesInput;
        private readonly Button _generateButton;
        private readonly IFieldGenerator _fieldGenerator;
        private readonly IStatesController _statesController;

        public GenerateFieldButtonPresenter(
            TMP_InputField rowsInput,
            TMP_InputField columnsInput,
            TMP_InputField obstaclesInput,
            Button generateButton,
            IFieldGenerator fieldGenerator,
            IStatesController statesController)
        {
            _rowsInput = rowsInput;
            _columnsInput = columnsInput;
            _obstaclesInput = obstaclesInput;
            _generateButton = generateButton;
            _fieldGenerator = fieldGenerator;
            _statesController = statesController;

            _generateButton.onClick.AddListener(OnGenerateButtonClicked);
        }

        public void Dispose()
        {
            _generateButton.onClick.RemoveAllListeners();
        }

        private void OnGenerateButtonClicked()
        {
            _statesController.SetNoneState();

            if (int.TryParse(_rowsInput.text, out int rows) &&
                int.TryParse(_columnsInput.text, out int columns) &&
                int.TryParse(_obstaclesInput.text, out int obstacles))
            {
                _fieldGenerator.Generate(rows, columns, obstacles);
            }
            else
            {
                Debug.LogError("Invalid input for rows, columns, or obstacles.");
            }
        }
    }
}
