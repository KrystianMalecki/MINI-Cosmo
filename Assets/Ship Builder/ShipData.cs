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
  
    [System.NonSerialized]
    public ShipDataBase BasedOn;
    public int SDBid;
    public ShipInventory shipInventory = new ShipInventory();
    public ItemInventory itemInventory;
    public float HP;
    public float energy;

    public Stats stats;

    public void ResetStats()
    {

        stats = BasedOn.stats.copy();

        itemInventory.max_size =stats.maxSpace;

    }
    public void CountStats()
    {
        ResetStats();
        for (int i = 0; i < shipInventory.inv.inv.Count; i++)
        {
            for (int j = 0; j < shipInventory.inv.inv[i].line.Count; j++)
            {


                ShipTile tile = shipInventory.inv.inv[i].line[j];
                if (tile.item != null)
                {
                    if (tile.item.id != "")
                    {
                        Debug.Log("step1" + tile);

                        EqItem eqi = null;
                        if (Inventory.instance.GetItemInfo(tile.item.id) is EqItem)
                        {
                            eqi = (EqItem)Inventory.instance.GetItemInfo(tile.item.id);
                        }
                        Debug.Log("step2" + eqi);

                        if (eqi != null)
                        {

                            stats.add(eqi.stats);
                            Debug.Log("(" + i + "," + j + ") =" + tile.item.id);
                        }
                    }
                }
        
            }
        }
        itemInventory.max_size = stats.maxSpace;

    }
    public void makeNew()
    {
        ResetStats();
        shipInventory = BasedOn.shipInventory;
    }
}
[Serializable]
public class Stats
{
    [Header("hps")]
    public float maxHP;
    [Header("Speeds")]
    public float speed;
    public float rotationSpeed;


    [Header("Energy")]
    public float maxEnergy = 20;
    public float ERechargeWait = 1;
    [Tooltip("times 10 per second")]
    public float ERecharge = 0.1f;

    public int maxSpace=10;
    public Stats copy()
    {
        Stats copy = new Stats();
        copy.maxHP = this.maxHP;
        copy.speed = this.speed;
        copy.rotationSpeed = this.rotationSpeed;
        copy.maxEnergy = this.maxEnergy;
        copy.ERechargeWait = this.ERechargeWait;
        copy.ERecharge = this.ERecharge;
        copy.maxSpace = this.maxSpace;

        return copy;
    }
    public void add(Stats s)
    {
        maxHP += s.maxHP;
        speed += s.speed;
        rotationSpeed += s.rotationSpeed;
        maxEnergy += s.maxEnergy;
        ERechargeWait += s.ERechargeWait;
        ERecharge += s.ERecharge;
        maxSpace += s.maxSpace;
       









    }
}


