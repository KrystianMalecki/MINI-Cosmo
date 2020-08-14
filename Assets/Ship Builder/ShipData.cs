using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]

public class ShipData
{
    public ShipDataBase BasedOn;
    public ShipInventory shipInventory;
    [Header("hps")]
    public float HP;
    public float maxHP;
    [Header("Speeds")]
    public float speed;
    public float rotationSpeed;


    [Header("Energy")]
    public float maxEnergy = 20;
    public float energy;
    public float ERechargeWait = 1;
    [Tooltip("times 10 per second")]
    public float ERecharge = 0.1f;
}
[Serializable]
[CreateAssetMenu(fileName = "New Ship Data Base", menuName = "Custom/New Ship Data Base")]
public class ShipDataBase : ScriptableObject
{
    [Header("ignore BasedOn here")]
    public ShipData data;

}
