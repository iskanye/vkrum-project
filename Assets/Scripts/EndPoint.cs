using UnityEngine;

public class EndPoint : MonoBehaviour
{
    public static EndPoint Current { get; private set; }

    public int TrajectoryPoints { get; set; }

    [SerializeField] private LayerMask ballLayer;

    void Awake()
    {
        Current = this;
    }

    void Update()
    {
        GetComponent<SpriteRenderer>().color = TrajectoryPoints == 0 ? Color.green : Color.gray;
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if (TrajectoryPoints == 0 && (ballLayer & (1 << c.gameObject.layer)) != 0) 
        {
            RewindBall.Current.Win();
        }
    }
}
