using Assets.Scripts.Field;
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
        private readonly GridController _gridController;

        public GenerateFieldButtonPresenter(
            TMP_InputField rowsInput,
            TMP_InputField columnsInput,
            TMP_InputField obstaclesInput,
            Button generateButton,
            GridController gridController)
        {
            _rowsInput = rowsInput;
            _columnsInput = columnsInput;
            _obstaclesInput = obstaclesInput;
            _generateButton = generateButton;
            _gridController = gridController;

            _generateButton.onClick.AddListener(OnGenerateButtonClicked);
        }

        public void Dispose()
        {
            _generateButton.onClick.RemoveAllListeners();
        }

        private void OnGenerateButtonClicked()
        {
            if (int.TryParse(_rowsInput.text, out int rows) &&
                int.TryParse(_columnsInput.text, out int columns) &&
                int.TryParse(_obstaclesInput.text, out int obstacles))
            {
                _gridController?.Create(rows, columns, obstacles);
            }
            else
            {
                Debug.LogError("Invalid input for rows, columns, or obstacles.");
            }
        }
    }
}
