using UnityEngine;
using UnityEngine.SceneManagement;

public class DataTransfer : MonoBehaviour 
{
    public static DataTransfer Current { get; private set; }

    public LevelData LevelData { set => currentData = value; }

    [SerializeField] private int editorSceneIndex, playSceneIndex;

    private LevelData currentData;

    void Awake()
    {
        if (Current) 
        {
            Destroy(gameObject);
        }
        else 
        {
            Current = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void OpenEditor()
    {
        void SceneLoaded(Scene s, LoadSceneMode m) 
        {
            FindObjectOfType<LevelEditor>().Load(currentData);
            SceneManager.sceneLoaded -= SceneLoaded;
        }

        SceneManager.LoadScene(editorSceneIndex);
        SceneManager.sceneLoaded += SceneLoaded;
    }

    public void StartTesting(LevelData data) 
    {
        currentData = data;

        void SceneLoaded(Scene s, LoadSceneMode m) 
        {
            Debug.Log("loaded");
            FindObjectOfType<LevelBuilder>().BuildLevel(data);
            SceneManager.sceneLoaded -= SceneLoaded;
        }

        SceneManager.LoadScene(playSceneIndex);
        SceneManager.sceneLoaded += SceneLoaded;
    }

    public void Reload()
    {
        StartTesting(currentData);
    }
}
