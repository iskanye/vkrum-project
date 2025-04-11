using UnityEngine;

public class EndPoint : MonoBehaviour
{
    [SerializeField] private LayerMask ballLayer;

    void OnTriggerEnter2D(Collider2D c)
    {
        if ((ballLayer & (1 << c.gameObject.layer)) != 0) 
        {
            RewindBall.Current.Win();
        }
    }
}
