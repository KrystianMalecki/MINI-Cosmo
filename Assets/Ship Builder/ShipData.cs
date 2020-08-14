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
    public void ResetToBase()
    {
        ResetStats();
         shipInventory = BasedOn.data.shipInventory;

    }
    public void ResetStats()
    {
        HP = BasedOn.data.HP;
        maxHP = BasedOn.data.maxHP;
        speed = BasedOn.data.speed;
        rotationSpeed = BasedOn.data.rotationSpeed;
        maxEnergy = BasedOn.data.maxEnergy;
        energy = BasedOn.data.energy;
        ERechargeWait = BasedOn.data.ERechargeWait;
        ERecharge = BasedOn.data.ERecharge;
    

    }
}
[Serializable]
[CreateAssetMenu(fileName = "New Ship Data Base", menuName = "Custom/New Ship Data Base")]
public class ShipDataBase : ScriptableObject
{
    [Header("ignore BasedOn here")]
    public ShipData data;

}
