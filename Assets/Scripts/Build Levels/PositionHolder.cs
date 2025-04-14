using UnityEditor.Experimental;
using UnityEngine;
using UnityEngine.EventSystems;

public class PositionHolder : MonoBehaviour, IDragHandler
{
    public string Group { get => group; set => group = value; }

    [SerializeField] private string group;

    private float grid = .5f;
    private LevelEditor editor;

    public void Initialize(LevelEditor editor)
    {
        this.editor = editor;
    }

    public void OnDrag(PointerEventData e)
    {
        editor.Select(gameObject);
        transform.position = (Vector2)Camera.main.ScreenToWorldPoint(e.position);
        transform.position = new(
            Mathf.Floor(transform.position.x / grid) * grid,
            Mathf.Floor(transform.position.y / grid) * grid);
    }
}