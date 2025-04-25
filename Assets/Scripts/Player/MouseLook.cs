using UnityEngine;

public class MouseLook : MonoBehaviour
{
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (mainCamera != null)
        {
            Vector3 cameraForward = mainCamera.transform.forward;
            cameraForward.y = 0f;

            if (cameraForward != Vector3.zero)
            {
                Quaternion newRotation = Quaternion.LookRotation(cameraForward);
                transform.rotation = newRotation;
            }
        }
    }
}
