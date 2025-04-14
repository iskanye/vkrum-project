using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Instruments : MonoBehaviour
{    
    [Header("Prefabs")]
    [SerializeField] private GameObject panel; 
    [SerializeField] private GameObject destroyablePanel; 
    [SerializeField] private GameObject spring;    
    [SerializeField] private Button panelButton, destroyablePanelButton, springButton;
    [SerializeField] private Button rotate1, rotate2, delete;

    private Instrument selected = null;
    private int[] restrictions = new int[3];

    public void Initialize(LevelData data)
    {
        data.restrictions.CopyTo(restrictions);
    }

    void Update() 
    {
        rotate1.interactable = rotate2.interactable = delete.interactable = selected != null;
        panelButton.interactable = restrictions[0] != 0;
        destroyablePanelButton.interactable = restrictions[1] != 0;
        springButton.interactable = restrictions[2] != 0;
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
            .AddComponent<Instrument>().Initialize(this, index);

        restrictions[index]--;
    }

    public void Select(Instrument selected)
    {
        this.selected = selected;
    }

    public void Rotate(bool clockwise)
    {
        selected.transform.Rotate(0, 0, (clockwise ? -1 : 1) * 22.5f);
    }

    public void Delete()
    {
        restrictions[selected.Type]++;
        Destroy(selected);
    }
}
