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
    public List<ItemData> items = new List<ItemData>();
    public int max_size = 10;
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
    public bool AddItem(string id, int num)
    {

        Item i = GetItemInfo(id);

       

        int index = items.FindIndex(0, x => x.id == id);
        if (index == -1)
        {
            if (items.Count + 1 > max_size)
            {
                return false;
            }
            items.Add(new ItemData(id, num));
            return true;
        }
        else
        {
            if (i.isStackable)
            {
                items[index].count += num;
            }
            else
            {
                if (items.Count + 1 > max_size)
                {
                    return false;
                }
                items.Add(new ItemData(id, 1));
            }
            return true;
        }
    }
    public Item GetItemInfo(string id)
    {
        Item i = null;

        if (!ItemStorage.TryGetValue(id, out i))
        {
            Debug.LogError("Can't find item with id:" + id);
        }
        return i;
    }
}

