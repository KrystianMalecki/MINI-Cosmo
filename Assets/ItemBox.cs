using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemBox : MonoBehaviour
{
    public Text count_txt;
    public Image icon;
    public int id;
    public void Setup(Sprite spr, int count,bool IsStackable,int idd)
    {
        gameObject.SetActive(true);

        count_txt.text = IsStackable ? count.ToString() : "";
        icon.sprite = spr;
        id = idd;
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
