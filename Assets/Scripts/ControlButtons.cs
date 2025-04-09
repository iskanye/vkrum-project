using UnityEngine;
using UnityEngine.UI;

public class ControlButtons : MonoBehaviour
{
    [SerializeField] private RewindObject rewindObject;
    [SerializeField] private GameObject instrumentsPanel;
    [SerializeField] private Image stopResumeImage;
    [SerializeField] private Sprite stop, resume;

    private bool isStopped = false;

    public void StopResume() 
    {
        if (isStopped)
            rewindObject.StartSimulating();
        else
            rewindObject.StopSimulating();

        isStopped = !isStopped;
        stopResumeImage.sprite = isStopped ? resume : stop;
        instrumentsPanel.SetActive(isStopped);
    }

    public void Rewind() 
    {
        rewindObject.StartRewind();

        isStopped = false;
        stopResumeImage.sprite = stop;
        instrumentsPanel.SetActive(false);
    }
}
