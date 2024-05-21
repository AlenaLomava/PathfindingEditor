using Assets.Scripts.Field;
using Assets.Scripts.States;
using Assets.Scripts.UI;
using UnityEngine;

namespace Assets.Scripts
{
    public class Main : MonoBehaviour
    {
        [SerializeField]
        private UIController _uiController;

        [SerializeField]
        private FieldController _fieldController;

        [SerializeField]
        private SelectableController _selectableController;

        [SerializeField]
        private AgentController _agentController;

        void Start()
        {
            var fieldStorage = new FieldStorage();

            var fieldGenerator = new FieldGenerator(_fieldController, fieldStorage);

            var statesController = new StatesController(
                _selectableController, 
                fieldStorage, 
                _fieldController,
                _agentController);

            _uiController.Initialize(fieldGenerator, statesController);
        }
    }
}
