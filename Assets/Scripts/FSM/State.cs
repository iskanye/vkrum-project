using System.Collections;
using UnityEngine;

public abstract class State<T> where T : MonoBehaviour
{
    protected T mn;

    public State(T manager) =>
        mn = manager;

    public virtual IEnumerator Start() 
    {
        mn.StartCoroutine(Update());
        yield break;
    }

    public virtual IEnumerator Update() 
    {
        yield return null;
    }

    public virtual IEnumerator Stop() 
    {
        mn.StopAllCoroutines();
        yield break;
    }
}