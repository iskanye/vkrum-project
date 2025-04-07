using System.Collections;
using UnityEngine;

public abstract class StateManager<T> : MonoBehaviour where T : MonoBehaviour
{
    protected State<T> currentState;

    protected void ChangeState(State<T> state)
    {
        IEnumerator _ChangeState() 
        {
            if (currentState != null) 
            {
                StartCoroutine(currentState.Stop());
                yield return new WaitWhile(() => state.IsStopping);
            }

            currentState = state;
            StartCoroutine(currentState.Start());
            yield break;
        }
        StartCoroutine(_ChangeState());
    }
}
