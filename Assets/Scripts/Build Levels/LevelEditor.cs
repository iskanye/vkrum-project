using UnityEngine;

public class LevelEditor : MonoBehaviour
{
    public static LevelEditor Current { get; private set; }

    public float ScaleFactor { get => canvas.scaleFactor; }

    [SerializeField] private Canvas canvas;
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

    public void Save() 
    {
        data.ball = ball.transform.position;
        data.endPoint = endPoint.transform.position;
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
                    data.panels.Add(i.transform.position);
                    data.panelRotations.Add(i.transform.rotation.eulerAngles.z);
                    break;
                case "destr panel":
                    data.destroyablePanels.Add(i.transform.position);
                    data.destroyablePanelRotations.Add(i.transform.rotation.eulerAngles.z);
                    break;
                case "spike":
                    data.spikes.Add(i.transform.position);
                    data.spikeRotations.Add(i.transform.rotation.eulerAngles.z);
                    break;
                case "trajectory":
                    data.trajectoryPoints.Add(i.transform.position);
                    data.trajectoryPointRotations.Add(i.transform.rotation.eulerAngles.z);
                    break;
                case "spring":
                    data.springs.Add(i.transform.position);
                    data.springRotations.Add(i.transform.rotation.eulerAngles.z);
                    break;
            }
        }

        Debug.Log(JsonUtility.ToJson(data));
    }
}
