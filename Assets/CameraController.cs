using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera camera;
    private const float playerFov = 90f;

    void Update()
    {
        if (Input.GetKey(KeyCode.F))
        {
            camera.fieldOfView = 30f;
        }
        else
        {
            camera.fieldOfView = playerFov;
        }
    }
}