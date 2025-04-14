using UnityEngine;

public class Instruments : MonoBehaviour
{
    
    [Header("Prefabs")]
    [SerializeField] private GameObject panel; 
    [SerializeField] private GameObject destroyablePanel; 
    [SerializeField] private GameObject spring;

    private bool spawning;
    private int index;

    void Update() 
    {
        if (spawning) 
        {
            Vector2 pos;
            if (Input.touchCount > 0)
            {
                pos = Input.GetTouch(0).position;
            }
            else 
            {
                pos = Input.mousePosition;
            }

            if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)) 
            {
                var instrument = index switch 
                {
                    0 => panel,
                    1 => destroyablePanel,
                    2 => spring,
                    _ => null
                };
                Instantiate(instrument, (Vector2)Camera.main.ScreenToWorldPoint(pos), instrument.transform.rotation);
                spawning = false;
            }
        }
    }

    public void SpawnInstruments(int index) 
    {
        spawning = true;
        this.index = index;
    }
}
