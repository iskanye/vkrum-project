using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField] private int editorScene;

    public void Play()
    {

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
