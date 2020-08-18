using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;
[Serializable]
public class ItemDictionary : SerializableDictionary<string, Item> { }
[Serializable]
public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    public ItemDictionary ItemStorage = new ItemDictionary();
    public ItemInventory current_inv;
    //  public int max_stack_size=999;
    public void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);

        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    public bool AddItem(string id)
    {
        return AddItem(id, 1);
    }
    public bool AddItem(ItemData idata)
    {
        Debug.Log(idata);
        return AddItem(idata.id, idata.count);
    }
    public bool AddItem(string id, int num)
    {

        Item i = GetItemInfo(id);



        int index = current_inv.items.FindIndex(0, x => x.id == id);
        if (index == -1)
        {
            if (current_inv.items.Count + 1 > current_inv.max_size)
            {
                return false;
            }
            current_inv.items.Add(new ItemData(id, num));
            return true;
        }
        else
        {
            if (i.isStackable)
            {
                current_inv.items[index].count += num;
            }
            else
            {
                if (current_inv.items.Count + 1 > current_inv.max_size)
                {
                    return false;
                }
                current_inv.items.Add(new ItemData(id,num));
            }
            return true;
        }
    }
    public Item GetItemInfo(string id)
    {
        Item i = null;
        if (id == null)
        {
            return i;
        }
        if (!ItemStorage.TryGetValue(id, out i))
        {
            Debug.LogError("Can't find item with id:" + id);
        }
        return i;
    }
    public void RemoveItem(ItemData idata)
    {
        int a = current_inv.items.FindIndex(x => x.id == idata.id);
        if (a == -1)
        {
            return;
        }
        current_inv.items[a].count -= idata.count;
        if (current_inv.items[a].count < 1)
        {
            current_inv.items.RemoveAt(a);
        }
    }
}
[Serializable]
public class ItemInventory
{
    public List<ItemData> items = new List<ItemData>();
    public int max_size = 10;
}

