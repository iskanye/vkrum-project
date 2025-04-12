using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct LevelData 
{
    public Vector2 ball, endPoint;
    public List<Vector2> panels;
    public List<Vector2> destroyablePanels;
    public List<Vector2> spikes;
    public List<Vector2> trajectoryPoints;
    public List<Vector2> spring;
}