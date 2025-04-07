using System.Collections;
using UnityEngine;

public abstract class State<T> where T : MonoBehaviour
{
    public bool IsStopping { get; protected set; }

    protected T mn;

    private Coroutine update;

    public State(T manager) =>
        mn = manager;

    public virtual IEnumerator Start() 
    {
        update = mn.StartCoroutine(Update());
        yield break;
    }

    public virtual IEnumerator Update() 
    {
        yield return null;
    }

    public virtual IEnumerator Stop() 
    {
        mn.StopCoroutine(update);
        yield break;
    }
}