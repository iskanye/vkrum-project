using UnityEngine;
using UnityEngine.EventSystems;

public class PositionHolder : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public string Group { get => group; set => group = value; }

    [SerializeField] private string group;

    private float grid = .5f;
    private bool canRotate;

    void Update() 
    {
        if (canRotate && Input.GetKeyDown(KeyCode.E)) 
        {
            transform.Rotate(new(0, 0, 22.5f));
        }
    }

    public void OnBeginDrag(PointerEventData e)
    {
        canRotate = true;
    }

    public void OnEndDrag(PointerEventData e)
    {
        canRotate = false;
    }

    public void OnDrag(PointerEventData e)
    {
        transform.position = (Vector2)Camera.main.ScreenToWorldPoint(e.position);
        transform.position = new(
            Mathf.Floor(transform.position.x / grid) * grid,
            Mathf.Floor(transform.position.y / grid) * grid);
    }
}