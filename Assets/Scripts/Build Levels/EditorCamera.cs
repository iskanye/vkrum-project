using UnityEngine;
using UnityEngine.EventSystems;

public class EditorCamera : MonoBehaviour, IDragHandler
{
    [SerializeField] private float minCameraSize = 3, maxCameraSize = 12;

    void Update()
    {
        if (Input.touchCount == 2) 
        {
            var t1 = Input.GetTouch(0);
            var t2 = Input.GetTouch(1);
            float size = (t1.position - t2.position).magnitude 
                - (t1.position - t2.position - t1.deltaPosition + t2.deltaPosition).magnitude;
            Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - size * .01f, minCameraSize, maxCameraSize);
        }
    }

    public void OnDrag(PointerEventData e)
    {
        if (Input.touchCount == 1) 
        {
            Camera.main.transform.position -= (Vector3)e.delta / 600 * Camera.main.orthographicSize;            
        }
    }
}
