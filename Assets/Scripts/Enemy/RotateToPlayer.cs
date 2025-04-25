using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class RotateToPlayer : MonoBehaviour
{
    Vector3 _cameraDirection;

    void Update()
    {
        _cameraDirection = Camera.main.transform.forward;   
        _cameraDirection.y = 0;
        transform.rotation = Quaternion.LookRotation(_cameraDirection);
    }
}
