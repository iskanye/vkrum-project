using UnityEngine;
using UnityEngine.EventSystems;

public class PositionHolder : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public string Group { get => group; set => group = value; }

    [SerializeField] private string group;

    private float grid = .5f;
    private bool canInteract;

    void Update() 
    {
        if (canInteract)
        {
            if (Input.GetKeyDown(KeyCode.Q)) 
            {
                transform.Rotate(new(0, 0, 22.5f));
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                transform.Rotate(new(0, 0, -22.5f));
            }            
            if (Input.GetKeyDown(KeyCode.Delete))
            {
                Destroy(gameObject);
            }
        }
    }

    public void OnBeginDrag(PointerEventData e)
    {
        canInteract = true;
    }

    public void OnEndDrag(PointerEventData e)
    {
        canInteract = false;
    }

    public void OnDrag(PointerEventData e)
    {
        transform.position = (Vector2)Camera.main.ScreenToWorldPoint(e.position);
        transform.position = new(
            Mathf.Floor(transform.position.x / grid) * grid,
            Mathf.Floor(transform.position.y / grid) * grid);
    }
}