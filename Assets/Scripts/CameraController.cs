using Assets.Scripts.Config;
using UnityEngine;

namespace Assets.Scripts
{
    public class CameraController : MonoBehaviour
    {
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
            float heightFactor = 1 + (transform.position.y * Constants.Camera.HEIGHT_INFLUENCE);

            transform.Translate(translation * Constants.Camera.MOVE_SPEED * Time.deltaTime * heightFactor, Space.World);
        }

        private void HandleMouseMovement()
        {
            if (Input.GetMouseButton(1))
            {
                float h = Input.GetAxis("Mouse X");
                float v = Input.GetAxis("Mouse Y");

                Vector3 right = transform.right;
                Vector3 up = transform.up;

                Vector3 moveDirection = (right * -h + up * -v) * Constants.Camera.ROTATE_SPEED * Time.deltaTime;
                transform.Translate(moveDirection, Space.World);
            }
        }

        private void HandleMouseZoom()
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            if (scroll != 0.0f)
            {
                float zoomAmount = scroll * Constants.Camera.ZOOM_SPEED;
                float newHeight = Mathf.Clamp(transform.position.y - zoomAmount, Constants.Camera.MIN_ZOOM, Constants.Camera.MAX_ZOOM);
                transform.position = new Vector3(transform.position.x, newHeight, transform.position.z);
            }
        }
    }
}
