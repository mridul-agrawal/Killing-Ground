using UnityEngine;
using KillingGround.Player;

namespace KillingGround.Player
{
    public class CameraController : MonoBehaviour
    {
        // References:
        [SerializeField] private Transform playerTransform;
        [SerializeField] private Transform CameraHolderTransform;

        // Variables:
        [SerializeField] private float mouseSensitivity = 2f;
        [SerializeField] private float upLimit = -50f;
        [SerializeField] private float downLimit = 50f;

        private void Awake()
        {
            Mortality.OnPlayerDeath += DisableCameraController;
        }

        // Update is called once per frame
        void Update()
        {
            float horizontalRotation = Input.GetAxis("Mouse X");
            float verticalRotation = Input.GetAxis("Mouse Y");

            if (horizontalRotation != 0 || verticalRotation != 0)
            {
                HandleCameraInput(horizontalRotation, verticalRotation);
            }
        }

        // Rotates the Camera & Player according to the Mouse Input & Sensitivity.
        private void HandleCameraInput(float horizontalRotation, float verticalRotation)
        {
            playerTransform.Rotate(0, horizontalRotation * mouseSensitivity, 0);
            CameraHolderTransform.Rotate(-verticalRotation * mouseSensitivity, 0, 0);
            LimitVerticalRotation();

        }

        // Clamps the Vertical Camera Rotation between min & max limit specified.
        private void LimitVerticalRotation()
        {
            Vector3 currentRotation = CameraHolderTransform.localEulerAngles;
            if (currentRotation.x > 180) { currentRotation.x -= 360; }
            currentRotation.x = Mathf.Clamp(currentRotation.x, upLimit, downLimit);
            CameraHolderTransform.localRotation = Quaternion.Euler(currentRotation);
        }

        // Used to Disable this Script when Player dies.
        private void DisableCameraController()
        {
            this.enabled = false;
        }
    }
}