using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfoBox : MonoBehaviour
{
    public TextMeshProUGUI namer;
    public TextMeshProUGUI description;
    public TextMeshProUGUI count;
    public TextMeshProUGUI value;
    public Image icon;
    public void Setup(string namer, string description, string value, string count, Sprite spr)
    {
        gameObject.SetActive(true);
        this.namer.text = StaticDataManager.instance.TMProFormater +namer;
        this.description.text = StaticDataManager.instance.TMProFormater + description;
        this.value.text = StaticDataManager.instance.TMProFormater + "Value:" +value;

        this.count.text = StaticDataManager.instance.TMProFormater + count;
        icon.sprite = spr;
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
    
}
