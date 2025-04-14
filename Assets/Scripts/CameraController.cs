using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform lookAt;
    [SerializeField] private Transform borders;
    [SerializeField] private Vector2 bordersOffset;
    [SerializeField] private Vector3 offset = new(0, 0, -10);

    void Update()
    {
        var size = (Vector2)borders.localScale / 2 - bordersOffset;
        transform.position = new Vector3(
            size.x <= 0 ? 0 : Mathf.Clamp(lookAt.position.x, -size.x, size.x),
            size.y <= 0 ? 0 : Mathf.Clamp(lookAt.position.y, -size.y, size.y)) + offset;
    }
}
