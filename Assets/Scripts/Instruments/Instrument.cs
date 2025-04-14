using UnityEngine;
using UnityEngine.EventSystems;

public class Instrument : MonoBehaviour, IDragHandler
{
    public int Type { get; private set; }

    private float grid = .5f;
    private Instruments manager;

    public void Initialize(Instruments manager, int type)
    {
        Type = type;
        this.manager = manager;
    }

    public void OnDrag(PointerEventData e)
    {
        manager.Select(this);
        transform.position = (Vector2)Camera.main.ScreenToWorldPoint(e.position);
        transform.position = new(
            Mathf.Floor(transform.position.x / grid) * grid,
            Mathf.Floor(transform.position.y / grid) * grid);
    }
}
