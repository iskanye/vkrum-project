using UnityEngine;
using UnityEngine.EventSystems;

public class PositionHolder : MonoBehaviour, IDragHandler, IPointerClickHandler
{
    public string Group { get => group; set => group = value; }

    [SerializeField] private string group;

    private float grid = .5f;

    public void OnDrag(PointerEventData e)
    {
        LevelEditor.Current.Select(gameObject);
        transform.position = (Vector2)Camera.main.ScreenToWorldPoint(e.position);
        transform.position = new(
            Mathf.Floor(transform.position.x / grid) * grid,
            Mathf.Floor(transform.position.y / grid) * grid);
    }

    public void OnPointerClick(PointerEventData e)
    {
        LevelEditor.Current.Select(gameObject);;
    }
}