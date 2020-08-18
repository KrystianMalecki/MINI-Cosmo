using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class Save 
{
    [SerializeField]
    public List<ShipData> hangar_ships = new List<ShipData>();
    public Save copy()
    {
        Save s = new Save();
        s.hangar_ships = new List<ShipData>(hangar_ships);
        //hangar_ships.CopyTo(s.hangar_ships.ToArray());
        return s;
    }
}
