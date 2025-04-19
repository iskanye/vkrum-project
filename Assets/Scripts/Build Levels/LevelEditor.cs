using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Linq;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelEditor : MonoBehaviour
{
    public static LevelEditor Current { get; private set; }
    
    [SerializeField] private GameObject borders;
    [SerializeField] private TMP_Text messageField;
    [SerializeField] private TMP_InputField velocityX, velocityY, bounciness, gravityScale, output;
    [SerializeField] private TMP_InputField sizeX, sizeY;
    [SerializeField] private TMP_InputField[] restrictions;
    [SerializeField] private Transform grid;
    [SerializeField] private PositionHolder endPoint, ball;  
    [SerializeField] private Button rotate1, rotate2, delete;  
    [Header("Prefabs")]
    [SerializeField] private GameObject panel; 
    [SerializeField] private GameObject destroyablePanel; 
    [SerializeField] private GameObject spike;
    [SerializeField] private GameObject trajectoryPoint; 
    [SerializeField] private GameObject spring;

    private LevelData data;
    private GameObject selected;

    void Awake()
    {
        Current = this;
    }

    void Update()
    {        
        rotate1.interactable = rotate2.interactable = delete.interactable 
            = selected != null && selected != endPoint.gameObject && selected != ball.gameObject;
    }

    public void SpawnObject(int index) 
    {
        GameObject obj = index switch
        {
            0 => Instantiate(panel, (Vector2)Camera.main.transform.position, Quaternion.identity, grid),
            1 => Instantiate(destroyablePanel, (Vector2)Camera.main.transform.position, Quaternion.identity, grid),
            2 => Instantiate(spike, (Vector2)Camera.main.transform.position, Quaternion.identity, grid),
            3 => Instantiate(trajectoryPoint, (Vector2)Camera.main.transform.position, Quaternion.identity, grid),
            4 => Instantiate(spring, (Vector2)Camera.main.transform.position, Quaternion.identity, grid),
            _ => null
        };
        var posHolder = obj.AddComponent<PositionHolder>();
        posHolder.Group = index switch
        {
            0 => "panel",
            1 => "destr panel",
            2 => "spike",
            3 => "trajectory",
            4 => "spring",
            _ => ""
        };
    }

    public void LoadFromString() 
    {
        try 
        {
            DataTransfer.Current.LevelData = JsonUtility.FromJson<LevelData>(output.text);
            DataTransfer.Current.OpenEditor();
        }
        catch
        {
            Message("Ошибка: невозможно прочесть данные уровня");
        }
    }

    public void Load(LevelData data) 
    {
        borders.transform.localScale = data.levelSize;
        ball.transform.localPosition = data.ball;
        endPoint.transform.localPosition = data.endPoint;
        
        sizeX.text = data.levelSize.x.ToString(); sizeY.text = data.levelSize.y.ToString();
        velocityX.text = data.startVelocity.x.ToString(); velocityY.text = data.startVelocity.y.ToString();

        for (int i = 0; i < 3; i++) 
        {
            restrictions[i].text = data.restrictions[i].ToString();
        }

        bounciness.text = data.defaultBounciness.ToString();
        gravityScale.text = data.gravityScale.ToString();

        for (int i = 0; i < data.panels.Count; i++)
        {
            var posHolder = Instantiate(panel, data.panels[i] + (Vector2)grid.position, 
                Quaternion.Euler(0, 0, data.panelRotations[i]), grid).AddComponent<PositionHolder>();
            posHolder.Group = "panel";
        }

        for (int i = 0; i < data.destroyablePanels.Count; i++)
        {
            var posHolder = Instantiate(destroyablePanel, data.destroyablePanels[i] + (Vector2)grid.position, 
                Quaternion.Euler(0, 0, data.destroyablePanelRotations[i]), grid).AddComponent<PositionHolder>();
            posHolder.Group = "destr panel";
        }

        for (int i = 0; i < data.spikes.Count; i++)
        {
            var posHolder = Instantiate(spike, data.spikes[i] + (Vector2)grid.position, 
                Quaternion.Euler(0, 0, data.spikeRotations[i]), grid).AddComponent<PositionHolder>();
            posHolder.Group = "spike";
        }

        for (int i = 0; i < data.trajectoryPoints.Count; i++)
        {
            var posHolder = Instantiate(trajectoryPoint, data.trajectoryPoints[i] + (Vector2)grid.position, 
                Quaternion.Euler(0, 0, data.trajectoryPointRotations[i]), grid).AddComponent<PositionHolder>();
            posHolder.Group = "trajectory";
        }

        for (int i = 0; i < data.springs.Count; i++)
        {
            var posHolder = Instantiate(spring, data.springs[i] + (Vector2)grid.position, 
                Quaternion.Euler(0, 0, data.springRotations[i]), grid).AddComponent<PositionHolder>();
            posHolder.Group = "spring";
        }
    }

    float ToFloat(string str)
    {
        return Convert.ToSingle(str.Replace('.', ','));
    }

    public void Save() 
    {
        data.ball = ball.transform.localPosition;
        data.endPoint = endPoint.transform.localPosition;
        data.startVelocity = new(ToFloat(velocityX.text), ToFloat(velocityY.text));
        data.levelSize = new(ToFloat(sizeX.text), ToFloat(sizeY.text), 1);
        data.defaultBounciness = ToFloat(bounciness.text);
        data.gravityScale = ToFloat(gravityScale.text);

        data.panels = new(); data.destroyablePanels = new();
        data.spikes = new(); data.trajectoryPoints = new();
        data.springs = new(); data.panelRotations = new();
        data.spikeRotations = new(); data.springRotations = new();
        data.trajectoryPointRotations = new(); data.destroyablePanelRotations = new();
        data.restrictions = restrictions.Select(i => Convert.ToInt32(i.text)).ToList();

        foreach (var i in grid.GetComponentsInChildren<PositionHolder>())
        {
            switch (i.Group) 
            {
                case "panel":
                    data.panels.Add(i.transform.localPosition);
                    data.panelRotations.Add(i.transform.rotation.eulerAngles.z);
                    break;
                case "destr panel":
                    data.destroyablePanels.Add(i.transform.localPosition);
                    data.destroyablePanelRotations.Add(i.transform.rotation.eulerAngles.z);
                    break;
                case "spike":
                    data.spikes.Add(i.transform.localPosition);
                    data.spikeRotations.Add(i.transform.rotation.eulerAngles.z);
                    break;
                case "trajectory":
                    data.trajectoryPoints.Add(i.transform.localPosition);
                    data.trajectoryPointRotations.Add(i.transform.rotation.eulerAngles.z);
                    break;
                case "spring":
                    data.springs.Add(i.transform.localPosition);
                    data.springRotations.Add(i.transform.rotation.eulerAngles.z);
                    break;
            }
        }

        output.text = JsonUtility.ToJson(data);
    }

    public void Test()
    {
        try
        {           
            Save(); 

            void OpenEditor()
            {
                RewindBall.Current.AfterWin += DataTransfer.Current.OpenEditor;
                DataTransfer.Current.OnLevelLoaded -= OpenEditor;
            }

            DataTransfer.Current.OnLevelLoaded += OpenEditor;
            DataTransfer.Current.LoadLevel(data, true);       
        }
        catch
        {
            Message("Ошибка: неверный формат данных");
        }
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void UpdateSize()
    {        
        borders.transform.localScale = new(Convert.ToSingle(sizeX.text), Convert.ToSingle(sizeY.text), 1);
    }

    public void Select(GameObject selected)
    {
        this.selected = selected;
    }

    public void Rotate(bool clockwise)
    {
        selected.transform.Rotate(0, 0, (clockwise ? -1 : 1) * 22.5f);
    }

    public void Delete()
    {
        Destroy(selected);
    }

    public void Message(string msg)
    {
        IEnumerator Msg()
        {
            messageField.text = msg;
            yield return new WaitForSeconds(3);
            messageField.text = "";
        }
        StartCoroutine(Msg());
    }
}
