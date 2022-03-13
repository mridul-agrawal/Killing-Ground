using UnityEngine;
using KillingGround.Player;

public class CameraController : MonoBehaviour
{
    public Transform playerTransform;
    public Transform CameraHolderTransform;
    public float mouseSensitivity = 2f;
    public float upLimit = -50f;
    public float downLimit = 50f;

    private void Awake()
    {
        Mortality.OnPlayerDeath += DisableCameraController;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalRotation = Input.GetAxis("Mouse X");
        float verticalRotation = Input.GetAxis("Mouse Y");

        if(horizontalRotation != 0  || verticalRotation != 0)
        {
            HandleCameraInput(horizontalRotation, verticalRotation);
        }
    }

    private void HandleCameraInput(float horizontalRotation, float verticalRotation)
    {
        playerTransform.Rotate(0, horizontalRotation * mouseSensitivity, 0);
        CameraHolderTransform.Rotate(-verticalRotation * mouseSensitivity, 0, 0);
        LimitVerticalMovement();

    }

    private void LimitVerticalMovement()
    {
        Vector3 currentRotation = CameraHolderTransform.localEulerAngles;
        if(currentRotation.x > 180) { currentRotation.x -= 360; }
        currentRotation.x = Mathf.Clamp(currentRotation.x, upLimit, downLimit);
        CameraHolderTransform.localRotation = Quaternion.Euler(currentRotation);
    }

    private void DisableCameraController()
    {
        this.enabled = false;
    }
}
