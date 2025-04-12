using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder : MonoBehaviour
{
    [SerializeField] private string levelData;

    [SerializeField] private Transform grid;
    [SerializeField] private Transform endPoint;  
    [SerializeField] private RewindBall ball;  
    [Header("Prefabs")]
    [SerializeField] private GameObject panel; 
    [SerializeField] private GameObject destroyablePanel; 
    [SerializeField] private GameObject spike;
    [SerializeField] private GameObject trajectoryPoint; 
    [SerializeField] private GameObject spring;

    public void BuildLevel(string json) 
    {
        var data = JsonUtility.FromJson<LevelData>(json);
        endPoint.position = data.endPoint;
        ball.transform.position = data.ball;
        ball.StartVelocity = data.ball;

        for (int i = 0; i < data.panels.Count; i++)
        {
            Instantiate(panel, data.panels[i], 
                Quaternion.Euler(0, 0, data.panelRotations[i]), grid);
        }

        for (int i = 0; i < data.destroyablePanels.Count; i++)
        {
            Instantiate(destroyablePanel, data.destroyablePanels[i], 
                Quaternion.Euler(0, 0, data.destroyablePanelRotations[i]), grid);
        }

        for (int i = 0; i < data.spikes.Count; i++)
        {
            Instantiate(spike, data.spikes[i], 
                Quaternion.Euler(0, 0, data.spikeRotations[i]), grid);
        }

        for (int i = 0; i < data.trajectoryPoints.Count; i++)
        {
            Instantiate(trajectoryPoint, data.trajectoryPoints[i], 
                Quaternion.Euler(0, 0, data.trajectoryPointRotations[i]), grid);
        }

        for (int i = 0; i < data.springs.Count; i++)
        {
            Instantiate(spring, data.springs[i], 
                Quaternion.Euler(0, 0, data.springRotations[i]), grid);
        }
    }
}
