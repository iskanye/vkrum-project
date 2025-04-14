using UnityEngine;
using UnityEngine.EventSystems;

public class EditorCamera : MonoBehaviour, IDragHandler
{
    public void OnDrag(PointerEventData e)
    {
        Camera.main.transform.position -= (Vector3)e.delta / 100;
    }
}
