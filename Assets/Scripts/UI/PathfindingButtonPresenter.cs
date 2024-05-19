using Assets.Scripts.States;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class PathfindingButtonPresenter : IDisposable
    {
        private readonly Button _pathfindingButton;
        private readonly IFieldEditorStatesController _statesController;

        public PathfindingButtonPresenter(Button pathfindingButton, IFieldEditorStatesController statesController)
        {
            _pathfindingButton = pathfindingButton;
            _statesController = statesController;

            _pathfindingButton.onClick.AddListener(OnPathfindingButtonClicked);
        }

        public void Dispose()
        {
            _pathfindingButton.onClick.RemoveAllListeners();
        }

        private void OnPathfindingButtonClicked()
        {
            _statesController.SetPathfindingStartState();
            Debug.Log("Pathfinding mode activated");
        }
    }
}
