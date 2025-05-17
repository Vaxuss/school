using UnityEngine;

public class MouseLook : MonoBehaviour
{
    private Camera _mainCamera;

    private void Start()
    {
        _mainCamera = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (_mainCamera != null)
        {
            Vector3 cameraForward = _mainCamera.transform.forward;
            cameraForward.y = 0f;

            if (cameraForward != Vector3.zero)
            {
                Quaternion newRotation = Quaternion.LookRotation(cameraForward);
                transform.rotation = newRotation;
            }
        }
    }
}
