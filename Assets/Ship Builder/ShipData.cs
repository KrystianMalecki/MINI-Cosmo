using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]

public class ShipData
{
    [Header("Changables")]
    public string name;
    [Header("Data")]
    public ShipDataBase BasedOn;
    public ShipInventory shipInventory = new ShipInventory();
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
         shipInventory = BasedOn.shipInventory;

    }
    public void ResetStats()
    {
        maxHP = BasedOn.maxHP;
        speed = BasedOn.speed;
        rotationSpeed = BasedOn.rotationSpeed;
        maxEnergy = BasedOn.maxEnergy;
        ERechargeWait = BasedOn.ERechargeWait;
        ERecharge = BasedOn.ERecharge;
    

    }
}

