using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class ShipBuilderUI : UIBase
{
    public GameObject TileBase;
    public ShipInventory si = new ShipInventory();
    public List<ItemSlotBox> boxes = new List<ItemSlotBox>();
    public GridLayoutGroup transformer;
    public List<DragItem> ditems = new List<DragItem>();
    public GameObject DragItemBase;
    public override void OpenThis()
    {
        base.OpenThis();
        displayGrid();

    }
    public void displayGrid()
    {
        transformer.constraintCount = si.inv.inv.Count;
        StringBuilder sb = new StringBuilder();

        int c = 0;
        for (int u = 0; u < boxes.Count; u++)
        {
            boxes[u].Hide();
        }
        for (int a = 0; a < si.inv.inv.Count; a++)
        {
            for (int b = 0; b < si.inv.inv[a].line.Count; b++)
            {
                if (c < boxes.Count)
                {
                    sb.Append((int)si.inv.inv[a].line[b].type);
                    ItemSlotBox isb = boxes[c];
                    isb.Setup(this, a, b, si.inv.inv[a].line[b].type);

                    //   boxes.Add(isb);
                }
                else
                {
                    sb.Append((int)si.inv.inv[a].line[b].type);
                    GameObject go = Instantiate(TileBase, transformer.transform);
                    ItemSlotBox isb = go.GetComponent<ItemSlotBox>();
                    isb.Setup(this, a, b, si.inv.inv[a].line[b].type);
                    boxes.Add(isb);
                }

                c++;
            }
            sb.Append("\n");
        }
        Debug.Log(sb);
        for (int a = 0; a < si.inv.inv.Count; a++)
        {
            for (int b = 0; b < si.inv.inv[a].line.Count; b++)
            {
                if (si.inv.inv[a].line[b].item != null)
                {
                    if (si.inv.inv[a].line[b].item.id != "")
                    {
                        DragItem di = makeDI(si.inv.inv[a].line[b].item);
                        getAt(a,b).Add(di);

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
