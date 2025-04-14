using UnityEngine;
using UnityEngine.EventSystems;

public class Instrument : MonoBehaviour, IDragHandler
{
    private float grid = .5f;
    private Instruments manager;

    public void Initialize(Instruments manager)
    {
        this.manager = manager;
    }

    public void OnDrag(PointerEventData e)
    {
        manager.Select(gameObject);
        transform.position = (Vector2)Camera.main.ScreenToWorldPoint(e.position);
        transform.position = new(
            Mathf.Floor(transform.position.x / grid) * grid,
            Mathf.Floor(transform.position.y / grid) * grid);
    }
}
