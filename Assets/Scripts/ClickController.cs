using Assets.Scripts.Field;
using UnityEngine;

namespace Assets.Scripts
{
    public static class ClickController
    {
        public static Vector2Int GetGridPosition(Vector3 worldPosition, Vector3 gridOrigin, float cellSize)
        {
            int x = Mathf.FloorToInt((worldPosition.x - gridOrigin.x) / cellSize);
            int y = Mathf.FloorToInt((worldPosition.z - gridOrigin.z) / cellSize);
            return new Vector2Int(Mathf.Abs(x), Mathf.Abs(y));
        }

        // Метод для получения позиции в сетке из экранных координат
        public static Vector2Int GetGridPositionFromScreen(Vector3 screenPosition, Camera camera, Vector3 gridOrigin, float cellSize)
        {
            screenPosition.z = camera.transform.position.y;
            var worlPos = camera.ScreenToWorldPoint(screenPosition);
            Debug.Log(worlPos);
            int x = Mathf.FloorToInt(worlPos.x);
            int z = Mathf.FloorToInt(worlPos.z);

            // Обрабатываем клик на квадратике (x, z)
            Debug.Log("Clicked on grid cell: (" + x + ", " + z + ")");

            // Получаем координаты сетки из мировых координат
            return GetGridPosition(worlPos, gridOrigin, cellSize);
        }
    }
}
