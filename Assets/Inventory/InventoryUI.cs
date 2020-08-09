using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum IUImode { itemDisplay}
public class InventoryUI : UIBase
{
    public List<ItemBox> boxes = new List<ItemBox>();
    public GameObject IBbase;
    public Transform mainTransform;
    public ItemInfoBox IIB;
    public IUImode mode;
    public override void OpenThis()
    {
        base.OpenThis();
        displayItems();
        IIB.Hide();
    }
    public void boxClicked(int id)
    {
        switch (mode)
        {
            default:
            case IUImode.itemDisplay:
                {
                    displayInfo(id);
                    break;
                }
        }
    }
    public void displayItems()
    {
        List<ItemData> items = Inventory.instance.items;

        int offset = 0;
        for (int a = 0; a < boxes.Count; a++)
        {
            // Destroy(boxes[a].gameObject);
            if (items.Count > a)
            {
                boxes[a].Setup(Inventory.instance.GetItemInfo(items[a].id).texture, items[a].count, Inventory.instance.GetItemInfo(items[a].id).isStackable, a,this);

                offset++;
            }
            else
            {
                boxes[a].Hide();
            }
        }

        for (int a = offset; a < items.Count; a++)
        {
            GameObject go = Instantiate(IBbase, mainTransform);
            ItemBox ib = go.GetComponent<ItemBox>();
            ib.Setup(Inventory.instance.GetItemInfo(items[a].id).texture, items[a].count, Inventory.instance.GetItemInfo(items[a].id).isStackable, a,this);
            boxes.Add(ib);
        }
    }
    public void displayInfo(int id)
    {
        ItemData idata = Inventory.instance.items[id];
        Item it = Inventory.instance.GetItemInfo(idata.id);
        IIB.Setup(it.name,it.description,it.value.ToString("0"),idata.count.ToString("0"),it.texture);
    }
}
