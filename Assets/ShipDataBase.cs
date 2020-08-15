using UnityEngine;
using System;
[Serializable]
[CreateAssetMenu(fileName = "New Ship Data Base", menuName = "Custom/Ship Data Base")]
public class ShipDataBase : ScriptableObject
{
    public ShipGraphicData SGD;
    public ShipInventory shipInventory = new ShipInventory();
    public float maxHP;

    public float speed;
    public float rotationSpeed;


    public float maxEnergy = 20;
    public float ERechargeWait = 1;
    public float ERecharge = 0.1f;

}