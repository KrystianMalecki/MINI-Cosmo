using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class ShipGraphicData
{
    public List<ShipGraphicLayer> layers = new List<ShipGraphicLayer>();
    public Sprite icon;

    public List<TrailLayer> trailPositions = new List<TrailLayer>();
    public List<Vector4> shootPoints = new List<Vector4>();

}
[Serializable]
public class ShipGraphicLayer 
{
    [ColorUsage(true,true)]
    public Color color = Color.white;
    public Sprite texture;
}
[Serializable]
public class TrailLayer
{
    [ColorUsage(true, true)]
    public Color color = Color.white;
    public Vector3 offset;
}
