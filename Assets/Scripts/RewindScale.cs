using UnityEngine;
using UnityEngine.UI;

public class RewindScale : MonoBehaviour
{
    [SerializeField] private Slider scaler;
    [SerializeField] private RewindBall rewindObject;

    void Update()
    {
        scaler.maxValue = rewindObject.MaxMemorySize;
        scaler.value = rewindObject.RewindMemory.Count;
    }
}
