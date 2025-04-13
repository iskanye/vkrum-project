using UnityEngine;

public class LevelBuilder : MonoBehaviour
{
    [SerializeField] [TextArea] private string levelData;

    [SerializeField] private Transform grid;
    [SerializeField] private Transform endPoint;  
    [SerializeField] private RewindBall ball;  
    [Header("Prefabs")]
    [SerializeField] private GameObject panel; 
    [SerializeField] private GameObject destroyablePanel; 
    [SerializeField] private GameObject spike;
    [SerializeField] private GameObject trajectoryPoint; 
    [SerializeField] private GameObject spring;

    void Awake() 
    {
        BuildLevel(levelData);
    }

    public void BuildLevel(string json) 
    {
        var data = JsonUtility.FromJson<LevelData>(json);
        ball.Initialize(data);
        endPoint.localPosition = data.endPoint;

        for (int i = 0; i < data.panels.Count; i++)
        {
            Instantiate(panel, data.panels[i] + (Vector2)grid.position, 
                Quaternion.Euler(0, 0, data.panelRotations[i]), grid);
        }

        for (int i = 0; i < data.destroyablePanels.Count; i++)
        {
            Instantiate(destroyablePanel, data.destroyablePanels[i] + (Vector2)grid.position, 
                Quaternion.Euler(0, 0, data.destroyablePanelRotations[i]), grid);
        }

        for (int i = 0; i < data.spikes.Count; i++)
        {
            Instantiate(spike, data.spikes[i] + (Vector2)grid.position, 
                Quaternion.Euler(0, 0, data.spikeRotations[i]), grid);
        }

        for (int i = 0; i < data.trajectoryPoints.Count; i++)
        {
            Instantiate(trajectoryPoint, data.trajectoryPoints[i] + (Vector2)grid.position, 
                Quaternion.Euler(0, 0, data.trajectoryPointRotations[i]), grid);
        }

        for (int i = 0; i < data.springs.Count; i++)
        {
            Instantiate(spring, data.springs[i] + (Vector2)grid.position, 
                Quaternion.Euler(0, 0, data.springRotations[i]), grid);
        }
    }
}
