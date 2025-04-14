using UnityEngine;

public abstract class BaseRewind : MonoBehaviour
{
    public abstract void StartRewind();
    public abstract void StopRewind();

    public static void StartRewindAll() 
    {
        foreach (var i in FindObjectsOfType<BaseRewind>())
        {
            i.StartRewind();
        }
    }

    public static void StopRewindAll() 
    {
        foreach (var i in FindObjectsOfType<BaseRewind>())
        {
            i.StopRewind();
        }
    }
}