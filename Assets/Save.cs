using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class Save 
{
    [SerializeField]
    public List<ShipData> hangar_ships = new List<ShipData>();
    [SerializeField]
    public PlayerDataStorage PDS = new PlayerDataStorage();
    
}
