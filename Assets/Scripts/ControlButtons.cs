using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControlButtons : MonoBehaviour
{
    [SerializeField] private Button stopResume, rewind;
    [SerializeField] private GameObject instrumentsPanel;
    [SerializeField] private Image stopResumeImage;
    [SerializeField] private Sprite stop, resume;

    private bool isStopped = false;

    void Start()
    {
        RewindBall.Current.OnStartRewind += () => 
        {
            stopResume.interactable = false;
            rewind.interactable = false;
        };
        RewindBall.Current.OnEndRewind += () => 
        {
            stopResume.interactable = true;
            rewind.interactable = true;
        };
    }

    public void StopResume() 
    {
        if (!RewindBall.Current.gameObject.activeInHierarchy)
            return;

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
        if (!RewindBall.Current.gameObject.activeInHierarchy)
            return;

        RewindBall.Current.StartRewind();
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
