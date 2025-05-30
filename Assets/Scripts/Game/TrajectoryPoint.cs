using UnityEngine;

public class TrajectoryPoint : MonoBehaviour
{
    [SerializeField] private LayerMask ballLayer;

    public void Initialize()
    {
        EndPoint.Current.TrajectoryPoints++;
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if ((ballLayer & (1 << c.gameObject.layer)) != 0) 
        {
            EndPoint.Current.TrajectoryPoints--;
            Destroy(gameObject);
        }
    }
}
