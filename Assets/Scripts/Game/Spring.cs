using UnityEngine;

public class Spring : MonoBehaviour 
{
    [SerializeField] private float springCoefficient;
    [SerializeField] private LayerMask ballLayer;

    private bool triggered = true;

    void Start()
    {
        if (RewindBall.Current)
        {
            RewindBall.Current.OnStartRewind += () => triggered = false;
            RewindBall.Current.OnEndRewind += () => triggered = true;
        }
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if (triggered && (ballLayer & (1 << c.gameObject.layer)) != 0) 
        {
            RewindBall.Current.Bounciness = springCoefficient;
        }
    }

    void OnTriggerStay2D(Collider2D c)
    {
        if (triggered && (ballLayer & (1 << c.gameObject.layer)) != 0) 
        {
            RewindBall.Current.Bounciness = springCoefficient;
        }
    }

    void OnTriggerExit2D(Collider2D c)
    {
        if (triggered && (ballLayer & (1 << c.gameObject.layer)) != 0) 
        {
            RewindBall.Current.Bounciness = float.PositiveInfinity;
        }
    }
}