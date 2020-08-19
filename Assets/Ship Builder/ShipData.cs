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
        ShipHangar.instance.pl.shooting.Weapons.Clear();
        WeaponUIManager wuim = GameManager.instance.wuim;
        wuim.Wi.Clear();
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
                        
                        
                        if (Inventory.instance.GetItemInfo(tile.item.id) is WeaponItem)
                        {
                            WeaponItem wi = (WeaponItem)Inventory.instance.GetItemInfo(tile.item.id);
                            // Weapons.Add(new WeaponEq(wi.bulletData, null,tile.weaponShootPointID));
                            WeaponEq we = new WeaponEq(wi.bulletData, ShipHangar.instance.pl.shootpoints[tile.weaponShootPointID]);
                            we.FireButton = Shooting.codes[ShipHangar.instance.pl.shooting.Weapons.Count];
                            ShipHangar.instance.pl.shooting.Weapons.Add(we);
                            wuim.Wi.Add(wi);
                        }
                    }
                }
        
            }
        }
        itemInventory.max_size = stats.maxSpace;
    
        wuim.s = ShipHangar.instance.pl.shooting;
       
            wuim.Start();
    }
    public void makeNew()
    {
        ResetStats();
        shipInventory = BasedOn.shipInventory.copy();
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
    public float maxEnergy ;
    public float ERechargeWait ;
    [Tooltip("times 10 per second")]
    public float ERecharge ;

    public int maxSpace;
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


