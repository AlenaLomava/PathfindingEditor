namespace Assets.Scripts.Config
{
    public static class Constants
    {
        public static class Field
        {
            public const int MAX_CELLS_COUNT = 10000;  
            public const float SPACE_BETWEEN_CELLS = 1.2f;
        }

        public static class Camera
        {
            public static float MOVE_SPEED = 3.5f;
            public static float ZOOM_SPEED = 10f;
            public static float ROTATE_SPEED = 10f;
            public static float MIN_ZOOM = 5f;
            public static float MAX_ZOOM = 50f;
            public static float HEIGHT_INFLUENCE = 0.5f;
        }
    }
}
