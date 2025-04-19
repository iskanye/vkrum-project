using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataTransfer : MonoBehaviour 
{
    public static DataTransfer Current { get; private set; }
    public event Action OnLevelLoaded;

    public LevelData LevelData { set => currentData = value; }

    [SerializeField] private int editorSceneIndex, playSceneIndex;

    private LevelData currentData;
    private bool editor;

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

        SceneManager.sceneLoaded += SceneLoaded;
        SceneManager.LoadScene(editorSceneIndex);
    }

    public void LoadLevel(LevelData data, bool editor = false) 
    {
        currentData = data;
        this.editor = editor;

        void SceneLoaded(Scene s, LoadSceneMode m) 
        {
            FindObjectOfType<LevelBuilder>().BuildLevel(data, editor);
            OnLevelLoaded?.Invoke();
            SceneManager.sceneLoaded -= SceneLoaded;
        }

        SceneManager.sceneLoaded += SceneLoaded;
        SceneManager.LoadScene(playSceneIndex);
    }

    public void Reload()
    {
        LoadLevel(currentData, editor);
    }
}
