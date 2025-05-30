using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct LevelData 
{
    public float defaultBounciness, gravityScale;
    public Vector3 levelSize;
    public Vector2 startVelocity, ball, endPoint;
    public List<Vector2> panels;
    public List<float> panelRotations;
    public List<Vector2> destroyablePanels;
    public List<float> destroyablePanelRotations;
    public List<Vector2> spikes;
    public List<float> spikeRotations;
    public List<Vector2> trajectoryPoints;
    public List<float> trajectoryPointRotations;
    public List<Vector2> springs;
    public List<float> springRotations;
    public List<int> restrictions;
}