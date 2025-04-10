using UnityEngine;

public abstract class StateManager<T> : MonoBehaviour where T : MonoBehaviour
{
    protected State<T> currentState;

    protected void ChangeState(State<T> state)
    {
        if (currentState != null) 
            StartCoroutine(currentState.Stop());
        
        currentState = state;
        StartCoroutine(currentState.Start());
    }
}
