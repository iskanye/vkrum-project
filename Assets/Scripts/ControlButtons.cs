using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControlButtons : MonoBehaviour
{
    [SerializeField] private RewindBall rewindBall;
    [SerializeField] private GameObject instrumentsPanel;
    [SerializeField] private Image stopResumeImage;
    [SerializeField] private Sprite stop, resume;

    private bool isStopped = false;

    public void StopResume() 
    {
        if (!rewindBall.gameObject.activeInHierarchy)
            return;

        if (isStopped)
            rewindBall.StartSimulating();
        else
            rewindBall.StopSimulating();

        isStopped = !isStopped;
        stopResumeImage.sprite = isStopped ? resume : stop;
        instrumentsPanel.SetActive(isStopped);
    }

    public void Rewind() 
    {
        if (!rewindBall.gameObject.activeInHierarchy)
            return;

        rewindBall.StartRewind();
        foreach (var i in FindObjectsOfType<BaseRewind>())
        {
            i.StartRewind();
        }

        isStopped = false;
        stopResumeImage.sprite = stop;
        instrumentsPanel.SetActive(false);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
