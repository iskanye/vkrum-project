using UnityEngine;

public class Instruments : MonoBehaviour
{
    [SerializeField] private GameObject instrument;

    private bool spawning;

    void Update() 
    {
        if (spawning && Input.touchCount > 0) 
        {
            var t = Input.GetTouch(0);
            if (t.phase == TouchPhase.Began) 
            {
                Instantiate(instrument, (Vector2)Camera.main.ScreenToWorldPoint(t.position), Quaternion.identity);
                spawning = false;
            }
        }
    }

    public void SpawnInstruments() 
    {
        spawning = true;
    }
}
