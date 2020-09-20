using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Attributes;
using System;
public enum TileType { Null, Power, Weapon, Engine, All, Defense, UI_SLOT_BOX, UI_TRASH }
[Serializable]
public class ShipTile
{
    public TileType type;
    public ItemData item;
    [ConditionalField("type", TileType.Weapon)] public int weaponShootPointID;
    public ShipTile(TileType tt)
    {
        type = tt;
       
    }
}
