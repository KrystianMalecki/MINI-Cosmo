using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : UIBase
{
    public List<ItemBox> boxes = new List<ItemBox>();
    public GameObject IBbase;
    public Transform mainTransform;
    public override void OpenThis()
    {
        base.OpenThis();
        displayInfo();
    }
    public void displayInfo()
    {
        List<ItemData> items = Inventory.instance.items;

        int offset = 0;
        for (int a = 0; a < boxes.Count; a++)
        {
            // Destroy(boxes[a].gameObject);
            if (items.Count > a)
            {
                boxes[a].Setup(Inventory.instance.GetItemInfo(items[a].id).texture, items[a].count, Inventory.instance.GetItemInfo(items[a].id).isStackable, a);

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
            ib.Setup(Inventory.instance.GetItemInfo(items[a].id).texture, items[a].count, Inventory.instance.GetItemInfo(items[a].id).isStackable, a);
            boxes.Add(ib);
        }
    }
}
