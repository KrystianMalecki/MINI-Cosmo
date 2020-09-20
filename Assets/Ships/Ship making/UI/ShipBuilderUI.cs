using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class ShipBuilderUI : UIBase
{
    public Image shipImag; 
    public GameObject TileBase;
    public GameObject DragItemBase;

    public ShipData selectedShip;

    public List<ItemSlotBox> boxes = new List<ItemSlotBox>();

    public GridLayoutGroup transformer;
    public List<DragItem> ditems = new List<DragItem>();
    [Header("UI display")]
    public List<ItemSlotBox> invitemboxes = new List<ItemSlotBox>();

    public List<ItemData> eqitems = new List<ItemData>();
    public Transform t;

    public override void OpenThis()
    {
        //  selectedShip = 
        base.OpenThis();
        displayGrid();
        getItems();
        displayItems();
        shipImag.sprite = selectedShip.BasedOn.SGD.icon;

    }
    public void displayItems()
    {
        for (int u = 0; u < invitemboxes.Count; u++)
        {
            invitemboxes[u].Hide();
        }

        for (int a = 0; a < eqitems.Count; a++)
        {
            DragItem di = makeDI(eqitems[a]);

            if (a < invitemboxes.Count)
            {
                ItemSlotBox isb = invitemboxes[a];
                isb.Setup(this, -1, -1, TileType.UI_SLOT_BOX);
                isb.Add(di);
                isb.UI_SB_id = a;
            }
            else
            {
                GameObject go = Instantiate(TileBase, t);
                ItemSlotBox isb = go.GetComponent<ItemSlotBox>();
                isb.Setup(this, -1, -1, TileType.UI_SLOT_BOX);
                invitemboxes.Add(isb);
                isb.Add(di);
                isb.UI_SB_id = a;


            }
        }



    }
    public void Count()
    {
        selectedShip.CountStats();
        ShipHangar.instance.pl.Damage(0);
        ShipHangar.instance.pl.AddEnergy(0);

    }
    public void getItems()
    {
        eqitems.Clear();
        for (int a = 0; a < Inventory.instance.current_inv.items.Count; a++)
        {
            if (Inventory.instance.GetItemInfo(Inventory.instance.current_inv.items[a].id) is EqItem)
            {
                eqitems.Add(Inventory.instance.current_inv.items[a]);
            }
        }
    }

    public void displayGrid()
    {
        transformer.constraintCount = selectedShip.shipInventory.inv.inv.Count;
        StringBuilder sb = new StringBuilder();

        int c = 0;
        for (int u = 0; u < boxes.Count; u++)
        {
            boxes[u].Hide();
        }
        for (int a = 0; a < selectedShip.shipInventory.inv.inv.Count; a++)
        {
            for (int b = 0; b < selectedShip.shipInventory.inv.inv[a].line.Count; b++)
            {
                if (c < boxes.Count)
                {
                    sb.Append((int)selectedShip.shipInventory.inv.inv[a].line[b].type);
                    ItemSlotBox isb = boxes[c];
                    isb.Setup(this, a, b, selectedShip.shipInventory.inv.inv[a].line[b].type);

                    //   boxes.Add(isb);
                }
                else
                {
                    sb.Append((int)selectedShip.shipInventory.inv.inv[a].line[b].type);
                    GameObject go = Instantiate(TileBase, transformer.transform);
                    ItemSlotBox isb = go.GetComponent<ItemSlotBox>();
                    isb.Setup(this, a, b, selectedShip.shipInventory.inv.inv[a].line[b].type);
                    boxes.Add(isb);
                }

                c++;
            }
            sb.Append("\n");
        }
        Debug.Log(sb);
        for(int j =0;j< ditems.Count; j++)
        {
            Destroy(ditems[j].gameObject);
            ditems.RemoveAt(j);
            j--;
        }
        for (int a = 0; a < selectedShip.shipInventory.inv.inv.Count; a++)
        {
            for (int b = 0; b < selectedShip.shipInventory.inv.inv[a].line.Count; b++)
            {
                if (selectedShip.shipInventory.inv.inv[a].line[b].item != null)
                {
                    if (selectedShip.shipInventory.inv.inv[a].line[b].item.id != "")
                    {
                        if (selectedShip.shipInventory.inv.inv[a].line[b].item.id != null)
                        {
                            DragItem di = makeDI(selectedShip.shipInventory.inv.inv[a].line[b].item);
                            getAt(a, b).Add(di);
                        }
                    }
                }

            }
        }

    }
    public DragItem makeDI(ItemData idata)
    {
        GameObject go = Instantiate(DragItemBase, transform);
        DragItem di = go.GetComponent<DragItem>();
        di.idata = idata;
        di.sbui = this;
        ditems.Add(di);
        di.Setup();
        return di;
    }
    public bool IsFree(int x, int y)
    {
        ItemSlotBox isb = getAt(x, y);
        if (isb != null)
        {
            if (isb.free)
            {
                return true;
            }
        }
        return false;
    }
    public ItemSlotBox getAt(int x, int y)
    {
        return boxes.Find(a => (a.pos_x == x && a.pos_y == y));
    }
}
