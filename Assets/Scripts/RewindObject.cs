using RewindStates;
using UnityEngine;
using System.Collections.Generic;

public class RewindObject : StateManager<RewindObject>
{
    public Rigidbody2D Rigidbody { get => rigidbody; }
    public float MaxWriteTime { get => maxWriteTime; }
    public Stack<Vector2> RewindMemory { get => rewindMemory; }

    [SerializeField] private new Rigidbody2D rigidbody;
    [SerializeField] private float maxWriteTime;

    private Stack<Vector2> rewindMemory = new();

    private SimulatedState simulatedState;
    private WriteState writeState;
    private StopState stopState;
    private RewindState rewindState;

    void Awake()
    {
        simulatedState = new(this);
        writeState = new(this);
        stopState = new(this);
        rewindState = new(this);

        ChangeState(simulatedState);
    }

    public void StartWriting() 
    {
        ChangeState(writeState);
    }

    public void StopWriting() 
    {
        ChangeState(stopState);
    }

    public void StartRewind() 
    {
        ChangeState(rewindState);
    }
}
