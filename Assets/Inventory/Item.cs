using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public string name;
    public Sprite texture;

    public float value;
    public bool stackable;
}
[Serializable]
public class ItemData
{
    public string id;
    public int count;
    public ItemData(string s,int i)
    {
        id = s;
        count = i;
    }
}
