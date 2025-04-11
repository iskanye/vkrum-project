using UnityEngine;
using UnityEngine.EventSystems;

public class PositionHolder : MonoBehaviour, IDragHandler
{
    public string Group { get; set; }

    private float grid = .5f;

    public void OnDrag(PointerEventData e)
    {
        transform.position = (Vector2)Camera.main.ScreenToWorldPoint(e.delta);
        //transform.position = new(
        //    Mathf.Floor(transform.position.x / grid) * grid,
        //    Mathf.Floor(transform.position.y / grid) * grid);
    }
}