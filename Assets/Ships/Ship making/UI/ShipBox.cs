using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShipBox : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI namer;
    public int id;
    public ShipHangarUI shui;
    public void Hide()
    {
        gameObject.SetActive(false);
    }
    public void Setup(int idd, Sprite spr, string namerr,ShipHangarUI ui)
    {
        gameObject.SetActive(true);
        icon.sprite = spr;
        namer.text = namerr;
        id = idd;
        shui = ui;
    }
    public void Clicked()
    {
        shui.Clicked(id);
    }
}
