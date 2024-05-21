using Assets.Scripts.Field;
using System;

namespace Assets.Scripts.States
{
    public interface IStatesController
    {
        event Action OnNoneState;

        void SetDrawObstacleState();

        void SetDrawTraversableState();

        void SetNoneState();

        void SetPathfindingVisualizeState();

        void SetPathfindingState();
    }
}
