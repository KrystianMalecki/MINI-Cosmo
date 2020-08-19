using Attributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[Serializable]
public class ShipInventory
{


    [SerializeField]
    public ShipInventoryProp inv = new ShipInventoryProp();
    public ShipInventory copy()
    {
        ShipInventory si = new ShipInventory();

        si.inv = new ShipInventoryProp(new List<STline>(inv.inv));
        return si;
    }
    public void OnValidate()
    {
        if (inv == null)
        {
            inv = new ShipInventoryProp();
        }
        else if (inv.inv.Count == 0)
        {
            inv = new ShipInventoryProp();

        }
        else if (inv.inv[0] == null)
        {
            inv.inv[0] = new STline(new List<ShipTile> { new ShipTile(TileType.Null) });

        }
        else if (inv.inv[0].line.Count == 0)
        {
            inv.inv[0] = new STline(new List<ShipTile> { new ShipTile(TileType.Null) });

        }
    }
}
[Serializable]
public class ShipInventoryProp
{
    [SerializeField]
    public List<STline> inv = new List<STline> { new STline(new List<ShipTile> { new ShipTile(TileType.Null) }) };
    public ShipInventoryProp(List<STline> lines)
    {
        inv = lines;
    }
    public ShipInventoryProp()
    {
        inv = new List<STline> { new STline(new List<ShipTile> { new ShipTile(TileType.Null) }) };
    }
}
[Serializable]
public class STline
{
    public List<ShipTile> line = new List<ShipTile>();
    public STline(List<ShipTile> lin)
    {
        line = lin;
    }
}

