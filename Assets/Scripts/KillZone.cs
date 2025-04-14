using UnityEngine;

public class KillZone : MonoBehaviour
{
    [SerializeField] private float height;

    void Update()
    {
        transform.localScale = new(transform.localScale.x, height / transform.parent.localScale.y, 1);
    }
}
