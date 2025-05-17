using UnityEngine;

public class SkyManager : MonoBehaviour
{
    [SerializeField]
    float _skySpeed;

    void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * _skySpeed);
    }
}
