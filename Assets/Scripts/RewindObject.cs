using RewindStates;
using UnityEngine;
using System.Collections.Generic;

public class RewindObject : StateManager<RewindObject>
{
    public Rigidbody2D Rigidbody { get => rigidbody; }
    public float MaxWriteTime { get => maxWriteTime; }
    public LinkedList<Vector2> RewindMemory { get => rewindMemory; }
    public int MaxMemorySize { get => Mathf.RoundToInt(maxWriteTime / Time.fixedDeltaTime); }
    public PhysicsMaterial2D BouncyMaterial { get => bouncyMaterial; }
    public PhysicsMaterial2D BouncelessMaterial { get => bouncelessMaterial; }

    [SerializeField] private new Rigidbody2D rigidbody;
    
    [SerializeField] private LayerMask obstacleLayer;
    [SerializeField] private float maxWriteTime;
    [SerializeField] private Vector2 startVelocity;
    
    [SerializeField] private PhysicsMaterial2D bouncyMaterial;
    [SerializeField] private PhysicsMaterial2D bouncelessMaterial;

    private LinkedList<Vector2> rewindMemory = new();

    private SimulatedState simulatedState;
    private StopState stopState;
    private RewindState rewindState;

    void Awake()
    {
        simulatedState = new(this);
        rewindState = new(this);
        stopState = new(this);
        ChangeState(simulatedState);
    }

    void OnCollisionEnter2D(Collision2D c)
    {
        if (currentState is RewindState && c.gameObject.layer == obstacleLayer)
        {
            ChangeState(simulatedState);
            RewindMemory.Clear();
        } 
    }

    public void _Start()
    {
        StartSimulating();
        rigidbody.velocity = startVelocity; 
    }

    public void StartSimulating() 
    {
        ChangeState(simulatedState);  
    }

    public void StopSimulating() 
    {
        ChangeState(stopState);  
    }

    public void StartRewind() 
    {
        ChangeState(rewindState);
    }
}
