using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[Serializable]
public class ShipInventory 
{
   
   
    [SerializeField]
    public ShipInventoryProp inv=new  ShipInventoryProp();
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
    }
}
[Serializable]
public class ShipInventoryProp
{
    [SerializeField]
    public List<STline> inv = new List<STline> { new STline(new List<ShipTile> { new ShipTile(TileType.Null) }) };
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
public enum TileType { Null, Power, Weapon, Engine }
[Serializable]
public class ShipTile
{
    public TileType type;
    public ItemData item;
    public ShipTile(TileType tt)
    {
        type = tt;
    }
}
