using UnityEngine;

public class MusicController : MonoBehaviour
{
    [SerializeField] private AudioSource regular, stop, rewind, win, destroy;

    void StopAll()
    {
        regular.Pause();
        rewind.Stop();
        stop.Stop();
        win.Stop();
        destroy.Stop();
    }

    public void Initialize()
    {
        RewindBall.Current.OnStartSimulation += () => 
        {
            StopAll();
            regular.UnPause();
        };
        RewindBall.Current.OnStartStop += () => 
        {
            StopAll();
            stop.Play();
        };
        RewindBall.Current.OnStartRewind += () => 
        {
            StopAll();
            rewind.Play();
        };
        RewindBall.Current.OnWin += () => 
        {
            StopAll();
            win.Play();
        };
        RewindBall.Current.OnDestroy += () => 
        {
            StopAll();
            destroy.Play();
        };

        regular.Play();
    }
}
