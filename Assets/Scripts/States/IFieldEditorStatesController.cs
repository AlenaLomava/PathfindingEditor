namespace Assets.Scripts.States
{
    public interface IFieldEditorStatesController
    {
        void SetDrawObstacleState();

        void SetDrawTraversableState();

        void SetNoneState();

        void SetPathfindingClearState();
        void SetPathfindingDrawState();
        void SetPathfindingEndState();

        void SetPathfindingStartState();
    }
}
