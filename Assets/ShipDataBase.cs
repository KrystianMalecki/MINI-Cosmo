using UnityEngine;
using System;
[Serializable]
[CreateAssetMenu(fileName = "New Ship Data Base", menuName = "Custom/Ship Data Base")]
public class ShipDataBase : ScriptableObject
{
 
    public ShipGraphicData SGD;
    public ShipInventory shipInventory = new ShipInventory();
    public Stats stats;

   
}