using UnityEngine;

public class LevelBuilder : MonoBehaviour
{
    [SerializeField] private GameObject editorButton;
    [SerializeField] private GameObject borders;
    [SerializeField] private Instruments instruments;
    [SerializeField] private Transform grid;
    [SerializeField] private EndPoint endPoint;  
    [SerializeField] private RewindBall ball;  
    [SerializeField] private ControlButtons buttons;  
    [SerializeField] private MusicController music;  
    [Header("Prefabs")]
    [SerializeField] private GameObject panel; 
    [SerializeField] private GameObject destroyablePanel; 
    [SerializeField] private GameObject spike;
    [SerializeField] private TrajectoryPoint trajectoryPoint; 
    [SerializeField] private GameObject spring;

    public void BuildLevel(LevelData data, bool editor = false) 
    {
        editorButton.SetActive(editor);
        ball.transform.localPosition = data.ball;
        endPoint.transform.localPosition = data.endPoint;
        borders.transform.localScale = data.levelSize;

        ball.AfterDestroy += DataTransfer.Current.Reload;
        ball.Initialize(data);
        instruments.Initialize(data);
        buttons.Initialize();
        endPoint.Initialize();
        music.Initialize();

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
                Quaternion.Euler(0, 0, data.trajectoryPointRotations[i]), grid).Initialize();
        }

        for (int i = 0; i < data.springs.Count; i++)
        {
            Instantiate(spring, data.springs[i] + (Vector2)grid.position, 
                Quaternion.Euler(0, 0, data.springRotations[i]), grid);
        }
    }
}
