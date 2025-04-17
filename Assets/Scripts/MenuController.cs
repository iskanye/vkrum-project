using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField] private int editorScene;
    [SerializeField] private GameObject editorButton;

    void Awake()
    {
        if (PlayerPrefs.GetInt("editor") == 1)
        {
            editorButton.SetActive(true);
        }   
    }

    public void Play()
    {
        LevelChanger.Current.NewGame();
    }

    public void Continue() 
    {
        LevelChanger.Current.Continue();
    }

    public void OpenEditor() 
    {
        SceneManager.LoadScene(editorScene);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
