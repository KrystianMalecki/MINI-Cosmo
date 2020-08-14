using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEditor;
using System.Xml.Serialization;
using UnityEngine.UI;

public class DragItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public static DragItem itemBeingDragged;
  
    public RectTransform rt;
    public CanvasGroup cg;
    private ItemSlotBox startSlot;
    public ItemSlotBox currentSlot;

    public ItemData idata;
    public EqItem eqitem;
    public ShipBuilderUI sbui;
    public Image img;
    public Image bck;

    public void Compressor(bool IsCompressing)
    {
        if (IsCompressing)
        {
            rt.pivot = new Vector2(0.5f,0.5f);
            rt.sizeDelta = new Vector2(16f, 16f);
            img.sprite = eqitem.texture;
        }
        else
        {
            rt.pivot = new Vector2(1f / (eqitem.size.y * 2), 1f - (1f / (eqitem.size.x * 2)));
            rt.sizeDelta = new Vector2(eqitem.size.y * 16, eqitem.size.x * 16);
            img.sprite = eqitem.textureInShip;

        }
    }
    public void Setup()
    {
       Item item = Inventory.instance.GetItemInfo(idata.id);
        if ((item is EqItem))
        {
            eqitem = (EqItem)item;

        }
        bck.GetComponent<Canvas>().sortingLayerName = "ui";
        Color c = StaticDataManager.TileToColor(eqitem.ttype);
        c.a = 0.4f;
        bck.color = c;
        bck.enabled = false;

    }

    public void OnBeginDrag(PointerEventData eventData)
    {

        Compressor(false);

        itemBeingDragged = this;

        sbui.ditems.ForEach(x => x.cg.blocksRaycasts = false);
        cg.blocksRaycasts = false;
        startSlot = currentSlot;
        if (currentSlot != null)
        {

            currentSlot.Remove();
        }
        bck.enabled = true;
    }



    public void OnDrag(PointerEventData eventData)
    {
        Vector3 v3 = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));
        /*v3.x += (eqitem.size.x-1)*8;
		v3.y += (eqitem.size.x-1) *8;*/

        transform.position = new Vector3(v3.x, v3.y, -1);

    }



    public void OnEndDrag(PointerEventData eventData)
    {
        itemBeingDragged = null;
        sbui.ditems.ForEach(x => x.cg.blocksRaycasts = true);
        cg.blocksRaycasts = true;

        bck.enabled = false;

        if (currentSlot == null)
        {
            return;
        }
        if (currentSlot == startSlot)
        {

            transform.position = startSlot.transform.position;
            startSlot.Add(this);

        }
        else
        {
            if(startSlot.type == TileType.UI_SLOT_BOX)
            {
                startSlot.Hide();
                Inventory.instance.RemoveItem(idata);
            }
           // currentSlot.
        }

    }




}