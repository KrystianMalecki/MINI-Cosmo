using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
[CreateAssetMenu(fileName = "New Item", menuName = "Custom/Inv/Item")]
public class Item : ScriptableObject
{
    public string name;
    public Sprite texture;

    public float value;
    public bool isStackable;
    public string data;
    [Multiline(6)]
    public string description;
}
[Serializable]
public class ItemData
{
    public string id;
    public int count=1;
    public ItemData(string s,int i=1)
    {
        id = s;
        count = i;
    }
    public ItemData copy()
    {
        ItemData idd = new ItemData(this.id, this.count);
        return idd;
    }
}
