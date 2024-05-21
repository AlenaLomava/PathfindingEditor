using Assets.Scripts.Field;
using Assets.Scripts.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class AgentController : MonoBehaviour
    {
        private const int POOL_INITAL_SIZE = 1;

        [SerializeField]
        private AgentView _agentPrefab;

        private ObjectPool<AgentView> _agentPool;
        private Coroutine _movementCoroutine;
        private AgentView _agent;

        private void Awake()
        {
            _agentPool = new ObjectPool<AgentView>(() => InstantiateAgent(), POOL_INITAL_SIZE);
        }

        public AgentView GetAgent()
        {
            var agent = _agentPool.GetItem();
            agent.gameObject.SetActive(true);
            return agent;
        }

        public void ReleaseAgent(AgentView agent)
        {
            agent.gameObject.SetActive(false);
            _agentPool.ReleaseItem(agent);
        }

        public void MoveAgentAlongPath(List<Cell> path, IFieldController fieldController)
        {
            var start = path[0];
            _agent = GetAgent();
            _agent.transform.position = fieldController.GetCellView(start.Row, start.Column).transform.position;
            _movementCoroutine = StartCoroutine(Move(path, fieldController));
        }

        public void StopMovement()
        {
            if (_movementCoroutine != null)
            {
                StopCoroutine(_movementCoroutine);
            }

            if (_agent != null)
            {
                ReleaseAgent(_agent);
            }
        }

        private IEnumerator Move(List<Cell> path, IFieldController fieldController)
        {
            var speed = _agent.MovementSpeed;

            foreach (var cell in path)
            {
                var targetPosition = fieldController.GetCellView(cell.Row, cell.Column).transform.position;
                var startPosition = _agent.transform.position;
                float travelTime = Vector3.Distance(startPosition, targetPosition) / speed;
                float elapsedTime = 0;

                while (elapsedTime < travelTime)
                {
                    elapsedTime += Time.deltaTime;
                    _agent.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / travelTime);
                    yield return null;
                }

                _agent.transform.position = targetPosition;
            }
        }

        private AgentView InstantiateAgent()
        {
            var agent = Instantiate(_agentPrefab);
            agent.gameObject.SetActive(false);
            return agent;
        }
    }
}
