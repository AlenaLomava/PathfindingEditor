using UnityEngine;

namespace Assets.Scripts
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField]
        private float _moveSpeed = 3.5f;

        [SerializeField]
        private float _zoomSpeed = 10f;

        [SerializeField]
        private float _rotateSpeed = 10f;

        [SerializeField]
        private float _minZoom = 5f;

        [SerializeField]
        private float _maxZoom = 50f;

        [SerializeField]
        private float _heightInfluence = 0.5f;

        public void Update()
        {
            HandleKeyboardMovement();
            HandleMouseMovement();
            HandleMouseZoom();
        }

        private void HandleKeyboardMovement()
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            Vector3 translation = new Vector3(horizontalInput, 0, verticalInput);
            float heightFactor = 1 + (transform.position.y * _heightInfluence);

            transform.Translate(translation * _moveSpeed * Time.deltaTime * heightFactor, Space.World);
        }

        private void HandleMouseMovement()
        {
            if (Input.GetMouseButton(1))
            {
                float h = Input.GetAxis("Mouse X");
                float v = Input.GetAxis("Mouse Y");

                Vector3 right = transform.right;
                Vector3 up = transform.up;

                Vector3 moveDirection = (right * -h + up * -v) * _rotateSpeed * Time.deltaTime;
                transform.Translate(moveDirection, Space.World);
            }
        }

        private void HandleMouseZoom()
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            if (scroll != 0.0f)
            {
                float zoomAmount = scroll * _zoomSpeed;
                float newHeight = Mathf.Clamp(transform.position.y - zoomAmount, _minZoom, _maxZoom);
                transform.position = new Vector3(transform.position.x, newHeight, transform.position.z);
            }
        }
    }
}
