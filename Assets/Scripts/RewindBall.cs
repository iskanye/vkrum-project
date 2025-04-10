using UnityEngine;
using System.Collections.Generic;
using System;

public partial class RewindBall : StateManager<RewindBall>
{
    public static RewindBall Current { get; private set; }

    public event Action OnStartRewind;
    public event Action OnEndRewind;

    public LinkedList<Vector2> RewindMemory { get => rewindMemory; }
    public int MaxMemorySize { get => Mathf.RoundToInt(maxWriteTime / Time.fixedDeltaTime); }

    [SerializeField] private new Rigidbody2D rigidbody;
    
    [SerializeField] private LayerMask obstacleLayer;
    [SerializeField] private LayerMask spikeLayer;
    [SerializeField] private float maxWriteTime;
    [SerializeField] private Vector2 startVelocity;
    
    [SerializeField] private PhysicsMaterial2D bouncyMaterial;
    [SerializeField] private PhysicsMaterial2D bouncelessMaterial;

    [SerializeField] private GameObject particles;

    private LinkedList<Vector2> rewindMemory = new();

    private SimulatedState simulatedState;
    private StopState stopState;
    private RewindState rewindState;
    private DestroyedState destroyedState;

    void Awake()
    {
        Current = this;

        simulatedState = new(this);
        rewindState = new(this);
        stopState = new(this);
        destroyedState = new(this);
        ChangeState(simulatedState);
        rigidbody.velocity = startVelocity; 
    }

    void OnCollisionEnter2D(Collision2D c)
    {
        if ((spikeLayer & (1 << c.gameObject.layer)) != 0) 
        {
            ChangeState(destroyedState);
        }

        if (currentState is RewindState && (obstacleLayer & (1 << c.gameObject.layer)) != 0)
        {
            ChangeState(simulatedState);
        } 
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
