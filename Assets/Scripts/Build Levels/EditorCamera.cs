using UnityEngine;
using UnityEngine.EventSystems;

public class EditorCamera : MonoBehaviour, IDragHandler
{
    public void OnDrag(PointerEventData e)
    {
        var cam = Camera.main;
        cam.transform.position -= (Vector3)e.delta / 100;
    }
}
