using UnityEngine;
using System.Collections.Generic;
using System;

public partial class RewindBall : StateManager<RewindBall>
{
    public static RewindBall Current { get; private set; }

    public event Action OnStartRewind;
    public event Action OnEndRewind;
    public event Action OnDestroy;
    public event Action AfterDestroy;
    public event Action OnWin;
    public event Action AfterWin;
    public event Action OnStartStop;
    public event Action OnEndStop;
    public event Action OnStartSimulation;

    public int MaxMemorySize { get => Mathf.RoundToInt(maxWriteTime / Time.fixedDeltaTime); }
    public int MemorySize { get => rewindMemory.Count; }
    public float Bounciness 
    { 
        set 
        {
            if (float.IsInfinity(value)) 
            {
                bounciness = defualtBounciness;
            } 
            else 
            {
                bounciness = value;                
            }
        }
    }

    [SerializeField] private new Rigidbody2D rigidbody;
    
    [SerializeField] private LayerMask obstacleLayer;
    [SerializeField] private LayerMask spikeLayer;
    [SerializeField] private float maxWriteTime;
    [SerializeField] private float defeatDelay = 6;
    [SerializeField] private float winDelay = 6;
    
    [SerializeField] private float defualtBounciness;
    [SerializeField] private float defualtGravity;

    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject trail;
    [SerializeField] private GameObject particles;

    private LinkedList<Vector2> rewindMemory = new();

    private SimulatedState simulatedState;
    private StopState stopState;
    private RewindState rewindState;
    private DestroyedState destroyedState;
    private WinState winState;

    private float bounciness;

    public void Initialize(LevelData data)
    {
        Current = this;

        simulatedState = new(this);
        rewindState = new(this);
        stopState = new(this);
        destroyedState = new(this);
        winState = new(this);

        trail.SetActive(true);
        rigidbody.velocity = data.startVelocity;
        defualtBounciness = bounciness = data.defaultBounciness;
        defualtGravity = rigidbody.gravityScale = data.gravityScale;
        
        ChangeState(simulatedState);
    }

    void OnCollisionEnter2D(Collision2D c)
    {
        if ((spikeLayer & (1 << c.gameObject.layer)) != 0) 
        {
            ChangeState(destroyedState);
            return;
        }

        if (currentState is RewindState && (obstacleLayer & (1 << c.gameObject.layer)) != 0)
        {
            ChangeState(simulatedState);
        } 

        var contact = c.GetContact(0);
        rigidbody.AddForce(bounciness * (contact.normalImpulse * contact.normal +
            contact.tangentImpulse * Vector2.Perpendicular(contact.normal)), ForceMode2D.Impulse);
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

    public void Win() 
    {
        ChangeState(winState);
    }
}
