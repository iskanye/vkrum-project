using UnityEngine;

public class LevelEditor : MonoBehaviour
{
    public static LevelEditor Current { get; private set; }

    public float ScaleFactor { get => canvas.scaleFactor; }

    [SerializeField] private Canvas canvas;
    [SerializeField] private Transform grid;
    [SerializeField] private GameObject endPoint, ball;    
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
    }
}
