using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResponseBox : MonoBehaviour
{
    public TextMeshProUGUI text;
    public DialogueUI dui;
    public int id;
    public void Setup(string txt ,DialogueUI d,int idd)
    {
        gameObject.SetActive(true);
        dui = d;
        text.text = StaticDataManager.instance.TMProFormater + txt;
        id = idd;
    }
    public void Clicked()
    {
        dui.Clicked(id);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
