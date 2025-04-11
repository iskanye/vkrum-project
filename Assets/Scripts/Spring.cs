using UnityEngine;

public class Spring : MonoBehaviour 
{
    [SerializeField] private LayerMask ballLayer;

    void OnCollisionEnter2D(Collision2D c)
    {
        if ((ballLayer & (1 << c.gameObject.layer)) != 0) 
        {
            var rigid = c.gameObject.GetComponent<Rigidbody2D>();
            rigid.AddForce((c.transform.position - transform.position).normalized, ForceMode2D.Impulse);
        }
    }
}