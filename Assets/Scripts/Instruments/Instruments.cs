using UnityEngine;
using UnityEngine.UI;

public class Instruments : MonoBehaviour
{    
    [Header("Prefabs")]
    [SerializeField] private GameObject panel; 
    [SerializeField] private GameObject destroyablePanel; 
    [SerializeField] private GameObject spring;    
    [SerializeField] private Button rotate1, rotate2, delete;

    private GameObject selected = null;

    void Update() 
    {
        rotate1.interactable = rotate2.interactable = delete.interactable = selected != null;
    }

    public void SpawnInstruments(int index) 
    {
        var instrument = index switch 
        {
            0 => panel,
            1 => destroyablePanel,
            2 => spring,
            _ => null
        };
        Instantiate(instrument, Vector2.zero, instrument.transform.rotation)
            .AddComponent<Instrument>().Initialize(this);
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
}
