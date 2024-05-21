using Assets.Scripts.Config;
using UnityEngine;

namespace Assets.Scripts.Field
{
    public class FieldGenerator : IFieldGenerator
    {
        private readonly IFieldController _fieldController;
        private readonly IFieldStorage _fieldStorage;
        private Field _field;

        public FieldGenerator(IFieldController fieldController, IFieldStorage fieldStorage)
        {
            _fieldController = fieldController;
            _fieldStorage = fieldStorage;
        }

        public void Generate(int rows, int columns, int obstaclesCount)
        {
            if (rows < 1 || columns < 1)
            {
                Debug.LogError("Invalid field size. Rows and columns input must be greater than zero.");
                return;
            }

            if (obstaclesCount > rows * columns)
            {
                Debug.LogError("The number of obstacles exceeds the total number of cells.");
                return;
            }

            if (rows * columns > Constants.Field.MAX_CELLS_COUNT)
            {
                Debug.LogError($"The field size exceeds the maximum supported number of cells ({Constants.Field.MAX_CELLS_COUNT}).");
                return;
            }

            _field = new Field(rows, columns);

            GenerateObstacles(obstaclesCount);

            _fieldController.UpdateFieldRendering(_field);
            _fieldStorage.Save(_field);

        }

        private void GenerateObstacles(int count)
        {
            var random = new System.Random();
            var placedObstacles = 0;

            while (placedObstacles < count)
            {
                var row = random.Next(_field.Rows);
                var col = random.Next(_field.Columns);

                var cell = _field.GetCell(row, col);
                if (cell != null && cell.IsTraversable)
                {
                    _field.SetCellTraversable(row, col, false);
                    placedObstacles++;
                }
            }
        }
    }
}
