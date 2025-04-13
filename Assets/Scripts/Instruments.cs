using UnityEngine;

public class Instruments : MonoBehaviour
{
    [SerializeField] private GameObject instrument;

    private bool spawning;

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
                Instantiate(instrument, (Vector2)Camera.main.ScreenToWorldPoint(pos), Quaternion.identity);
                spawning = false;
            }
        }
    }

    public void SpawnInstruments() 
    {
        spawning = true;
    }
}
