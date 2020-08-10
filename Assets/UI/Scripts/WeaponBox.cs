using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponBox : MonoBehaviour
{
    public Image bar;
    public Image icon;
    public void Setup(Sprite ic)
    {
        gameObject.SetActive(true);
        icon.sprite = ic;
      //  bar.material = Instantiate(bar.material);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
