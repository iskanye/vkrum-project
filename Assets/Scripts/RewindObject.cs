using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RewindObject : BaseRewind
{
    public Rigidbody2D Rigidbody { get => rigidbody;}

    [SerializeField] private new Rigidbody2D rigidbody;
    [SerializeField] private float maxWriteTime;

    private LinkedList<Vector2> rewindMemory;

    void FixedUpdate()
    {
        if (rewindMemory.Count == Mathf.RoundToInt(maxWriteTime / Time.fixedDeltaTime)) 
        {                    
           rewindMemory.RemoveFirst();
        }
        rewindMemory.AddLast(-rigidbody.velocity);
    }

    public override void StartRewind() 
    {
        IEnumerator Rewind() 
        {
            while (rewindMemory.Count != 0) 
            {
                rigidbody.velocity = rewindMemory.Last.Value;
                rewindMemory.RemoveLast();
                yield return new WaitForFixedUpdate();
            }   
        }
        StartCoroutine(Rewind());
    }
}
