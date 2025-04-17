using System.Collections.Generic;
using UnityEngine;

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
        DataTransfer.Current.OnLevelLoaded += () => 
        {
            RewindBall.Current.AfterWin += NextLevel;
        };
    }

    public void NewGame()
    {
        current = 0;
        DataTransfer.Current.LoadLevel(levelDatas[0]);
    }

    public void NextLevel()
    {
        if (current == levels.Length)
        {
            return;
        }

        current++;
        Continue();
    }

    public void Continue() 
    {
        DataTransfer.Current.LoadLevel(levelDatas[current]);
    }
}
