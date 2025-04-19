using UnityEngine;

[ExecuteInEditMode]
public class CameraScaler : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float size = 5;

    void Update()
    {
        mainCamera.orthographicSize = size * mainCamera.scaledPixelHeight / mainCamera.scaledPixelWidth;
    }
}
