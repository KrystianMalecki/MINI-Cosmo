using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEditor;

public class DragItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public static DragItem itemBeingDragged;
    //Vector3 startPosition;
    //Transform startParent;
    public RectTransform rt;
    public CanvasGroup cg;
    private ItemSlotBox startSlot;
    public ItemSlotBox currentSlot;

    public ItemData idata;
    public EqItem eqitem;
    public ShipBuilderUI sbui;
    public void Start()
    {
        Item item = Inventory.instance.GetItemInfo(idata.id);
        if ((item is EqItem))
        {
            eqitem = (EqItem)item;

        }
        rt.pivot = new Vector2(1f / (eqitem.size.y * 2), 1f - (1f / (eqitem.size.x * 2)));
        rt.sizeDelta = new Vector2(eqitem.size.y * 16, eqitem.size.x * 16);

    }

    public void OnBeginDrag(PointerEventData eventData)
    {


        itemBeingDragged = this;

        sbui.ditems.ForEach(x => x.cg.blocksRaycasts = false);

        startSlot = currentSlot;
        if (currentSlot != null)
        {

            currentSlot.Remove();
        }
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


        if (currentSlot == null)
        {
            return;
        }
        if (currentSlot == startSlot)
        {

            transform.position = startSlot.transform.position;
            startSlot.Add(this); 

        }

    }




}