using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform playerTransform;
    public Transform CameraHolderTransform;
    public float mouseSensitivity = 2f;
    public float upLimit = -50f;
    public float downLimit = 50f;

    // Update is called once per frame
    void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        float horizontalRotation = Input.GetAxis("Mouse X");
        float verticalRotation = Input.GetAxis("Mouse Y");

        playerTransform.Rotate(0, horizontalRotation * mouseSensitivity ,0);
        CameraHolderTransform.Rotate(-verticalRotation * mouseSensitivity, 0, 0);

        Vector3 currentRotation = CameraHolderTransform.localEulerAngles;
        if(currentRotation.x > 180) { currentRotation.x -= 360; }
        currentRotation.x = Mathf.Clamp(currentRotation.x, upLimit, downLimit);
        CameraHolderTransform.localRotation = Quaternion.Euler(currentRotation);
    }
}
