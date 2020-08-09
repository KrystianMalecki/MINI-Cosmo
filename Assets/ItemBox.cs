using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ItemBox : MonoBehaviour
{
    public TextMeshProUGUI count_txt;
    public Image icon;
    public int id;
    public InventoryUI IUI;
    public void Setup(Sprite spr, int count,bool IsStackable,int idd,InventoryUI iui)
    {
        gameObject.SetActive(true);

        count_txt.text = StaticDataManager.instance.TMProFormater+( IsStackable? count.ToString() : "");
        icon.sprite = spr;
        id = idd;
        this.IUI = iui;
    }
    public void select()
    {
        IUI.boxClicked(id);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
