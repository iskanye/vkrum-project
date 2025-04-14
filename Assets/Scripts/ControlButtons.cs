using UnityEngine;
using UnityEngine.UI;

public class ControlButtons : MonoBehaviour
{
    [SerializeField] private Button stopResume, rewind;
    [SerializeField] private GameObject instrumentsPanel;
    [SerializeField] private Image stopResumeImage;
    [SerializeField] private Sprite stop, resume;

    private bool isStopped = false;

    public void Initialize()
    {
        void Deactivate() 
        {
            stopResume.interactable = false;
            rewind.interactable = false;
        }
        void Activate() 
        {
            stopResume.interactable = true;
            rewind.interactable = true;
        }

        RewindBall.Current.OnStartRewind += Deactivate;
        RewindBall.Current.OnEndRewind += Activate;
        RewindBall.Current.OnDestroy += Deactivate;
        RewindBall.Current.OnWin += Deactivate;
    }

    public void StopResume() 
    {
        if (isStopped)
            RewindBall.Current.StartSimulating();
        else
            RewindBall.Current.StopSimulating();

        isStopped = !isStopped;
        stopResumeImage.sprite = isStopped ? resume : stop;
        instrumentsPanel.SetActive(isStopped);
    }

    public void Rewind() 
    {
        RewindBall.Current.StartRewind();
        BaseRewind.StartRewindAll();

        isStopped = false;
        stopResumeImage.sprite = stop;
        instrumentsPanel.SetActive(false);
    }

    public void Restart()
    {
        Time.timeScale = 1;
        DataTransfer.Current.Reload();
    }

    public void ReturnToEditor() 
    {
        Time.timeScale = 1;
        DataTransfer.Current.OpenEditor();
    }
}
