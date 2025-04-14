using UnityEngine;
using TMPro;
using System;

public class LevelEditor : MonoBehaviour
{
    public static LevelEditor Current { get; private set; }
    
    [SerializeField] private TMP_InputField velocityX, velocityY, bounciness, gravityScale, output;
    [SerializeField] private Transform grid;
    [SerializeField] private PositionHolder endPoint, ball;    
    [Header("Prefabs")]
    [SerializeField] private GameObject panel; 
    [SerializeField] private GameObject destroyablePanel; 
    [SerializeField] private GameObject spike;
    [SerializeField] private GameObject trajectoryPoint; 
    [SerializeField] private GameObject spring;

    private LevelData data;

    void Awake()
    {
        Current = this;
    }

    public void SpawnObject(int index) 
    {
        GameObject obj = index switch
        {
            0 => Instantiate(panel, new(1, 1, 0), Quaternion.identity, grid),
            1 => Instantiate(destroyablePanel, new(1, 1, 0), Quaternion.identity, grid),
            2 => Instantiate(spike, new(1, 1, 0), Quaternion.identity, grid),
            3 => Instantiate(trajectoryPoint, new(1, 1, 0), Quaternion.identity, grid),
            4 => Instantiate(spring, new(1, 1, 0), Quaternion.identity, grid),
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
        DataTransfer.Current.LevelData = JsonUtility.FromJson<LevelData>(output.text);
        DataTransfer.Current.OpenEditor();
    }

    public void Load(LevelData data) 
    {
        ball.transform.localPosition = data.ball;
        endPoint.transform.localPosition = data.endPoint;
        
        velocityX.text = data.startVelocity.x.ToString(); velocityY.text = data.startVelocity.y.ToString();
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

    public void Save() 
    {
        data.ball = ball.transform.localPosition;
        data.endPoint = endPoint.transform.localPosition;
        data.startVelocity = new(Convert.ToSingle(velocityX.text), Convert.ToSingle(velocityY.text));
        data.defaultBounciness = Convert.ToSingle(bounciness.text);
        data.gravityScale = Convert.ToSingle(gravityScale.text);

        data.panels = new(); data.destroyablePanels = new();
        data.spikes = new(); data.trajectoryPoints = new();
        data.springs = new(); data.panelRotations = new();
        data.spikeRotations = new(); data.springRotations = new();
        data.trajectoryPointRotations = new(); data.destroyablePanelRotations = new();

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
        Save();
        DataTransfer.Current.StartTesting(data);
    }
}
