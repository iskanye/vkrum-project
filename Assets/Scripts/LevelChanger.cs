using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    public static LevelChanger Current { get; private set; }

    [SerializeField] [TextArea] private string[] levels;

    private List<LevelData> levelDatas = new();
    private int current = 0;
    
    void Awake()
    {
        if (Current) 
        {
            Destroy(gameObject);
            return;
        }
        
        Current = this;
        DontDestroyOnLoad(gameObject);

        foreach (var i in levels)
        {
            levelDatas.Add(JsonUtility.FromJson<LevelData>(i));
        }

        current = PlayerPrefs.GetInt("current_level", 0);
    }

    void ChangeLevel() 
    {
        RewindBall.Current.AfterWin += NextLevel;
    }

    public void NewGame()
    {
        current = 0;
        DataTransfer.Current.LoadLevel(levelDatas[0]);
        DataTransfer.Current.OnLevelLoaded += ChangeLevel;
    }

    public void NextLevel()
    {
        if (current == levels.Length - 1)
        {
            PlayerPrefs.SetInt("editor", 1);
            DataTransfer.Current.OnLevelLoaded -= ChangeLevel;
            SceneManager.LoadScene(0);
            return;
        }

        current++;
        PlayerPrefs.SetInt("current_level", current);
        Continue();
    }

    public void Continue() 
    {
        DataTransfer.Current.LoadLevel(levelDatas[current]);
        DataTransfer.Current.OnLevelLoaded += ChangeLevel;
    }
}
