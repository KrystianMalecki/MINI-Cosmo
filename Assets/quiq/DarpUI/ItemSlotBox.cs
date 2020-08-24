using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlotBox : MonoBehaviour, IDropHandler
{
    public DragItem ditem;
    public int pos_x;
    public int pos_y;

    public TileType type;
    public ShipBuilderUI sie;
    public bool free;
    public Image img;
    public int UI_SB_id = -1;
    public Image hover;
    public void Setup(ShipBuilderUI s, int xx, int yy, TileType tt)
    {
        gameObject.SetActive(true);
        sie = s;
        pos_x = xx;
        pos_y = yy;
        type = tt;
        img.color = StaticDataManager.TileToColor(tt);
        free = true;
    }
    public void Clicked()
    {
    }
    public void Hide()
    {
        gameObject.SetActive(false);
        UI_SB_id = -1;
    }
    public void OnDrop(PointerEventData eventData)
    {


        if (ditem == null)
        {
            if (DragItem.itemBeingDragged != null)
            {
                if (type == TileType.UI_TRASH)
                {
                    if (Inventory.instance.AddItem(DragItem.itemBeingDragged.idata))
                    {
                        sie.ditems.Remove(DragItem.itemBeingDragged);
                        Destroy(DragItem.itemBeingDragged.gameObject);
                        DragItem.itemBeingDragged = null;
                        sie.ditems.ForEach(x => x.cg.blocksRaycasts = true);
                        sie.getItems();
                        sie.displayItems();

                        return;
                    }

                }
                if (type != TileType.UI_SLOT_BOX)
                {
                    if (Check(DragItem.itemBeingDragged))
                    {
                        Add(DragItem.itemBeingDragged);


                    }
                }
            }
        }

    }
    public void Add(DragItem di)
    {

        if (type == TileType.UI_SLOT_BOX)
        {
            ditem = di;
            di.Compressor(true);

            ditem.transform.SetParent(transform);

            ditem.currentSlot = this;
            ditem.gameObject.transform.localPosition = Vector3.zero;
        }
        else
        {
            ditem = di;
            di.Compressor(false);


            ditem.transform.SetParent(transform);

            ditem.currentSlot = this;
            ditem.gameObject.transform.localPosition = Vector3.zero;
            sie.selectedShip.shipInventory.inv.inv[pos_x].line[pos_y].item = di.idata.copy();

            freerer(false);
        }
    }
    public void Remove()
    {
        if (type == TileType.UI_SLOT_BOX)
        {
            ditem = null;

        }
        else
        {
            freerer(true);
            sie.selectedShip.shipInventory.inv.inv[pos_x].line[pos_y].item = new ItemData("", 0);
            ditem = null;
        }
    }
    public void freerer(bool t)
    {
        if (ditem != null)
        {
            if (ditem.eqitem != null)
            {
                for (int x = 0; x < ditem.eqitem.size.x; x++)
                {
                    for (int y = 0; y < ditem.eqitem.size.y; y++)
                    {
                        if (ditem.eqitem.maker.inv[x].line[y])
                        {
                            sie.getAt(pos_x + x, pos_y + y).free = t;

                        }
                    }
                }
            }
        }
    }
    public bool Check(DragItem dhandler)
    {



        bool isOk = true;


        for (int x = 0; x < dhandler.eqitem.size.x; x++)
        {
            for (int y = 0; y < dhandler.eqitem.size.y; y++)
            {
                if (dhandler.eqitem.maker.inv[x].line[y])
                {
                    if (!sie.IsFree(pos_x + x, pos_y + y))
                    {

                        isOk = false;
                        break;
                    }
                    if (sie.getAt(pos_x + x, pos_y + y).type == TileType.Null)
                    {

                        isOk = false;
                        break;
                    }
                    if (sie.getAt(pos_x + x, pos_y + y).type == TileType.All)
                    {
                        continue;
                    }
                    if (dhandler.eqitem.ttype == TileType.All)
                    {
                        return true;
                    }
                    if (sie.getAt(pos_x + x, pos_y + y).type != dhandler.eqitem.ttype)
                    {

                        isOk = false;
                        break;
                    }
                }
            }
        }
        if (dhandler.eqitem.ttype == type)
        {
            return isOk;

        }
        return false;

    }



}
