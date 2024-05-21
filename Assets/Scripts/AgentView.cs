using UnityEngine;

namespace Assets.Scripts
{
    public class AgentView : MonoBehaviour
    {
        [SerializeField]
        private float _movementSpeed = 3f;

        public float MovementSpeed => _movementSpeed;
    }
}
