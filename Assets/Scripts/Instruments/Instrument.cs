using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Instrument : MonoBehaviour, IDragHandler, IPointerClickHandler
{
    public int Type { get; private set; }

    private Instruments manager;

    public void Initialize(Instruments manager, int type)
    {
        Type = type;
        this.manager = manager;
        RewindBall.Current.OnStartStop += Activate;
        RewindBall.Current.OnEndStop += Deactivate;
    }

    public void OnDrag(PointerEventData e)
    {
        manager.Select(this);
        transform.position = (Vector2)Camera.main.ScreenToWorldPoint(e.position);
    }

    public void OnPointerClick(PointerEventData e)
    {
        manager.Select(this);;
    }

    void OnDestroy()
    {
        RewindBall.Current.OnStartStop -= Activate;
        RewindBall.Current.OnEndStop -= Deactivate;
    }

    void Activate()
    {
        enabled = true;
    }

    void Deactivate()
    {
        enabled = false;    
    }
}
