using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipTileBox : MonoBehaviour
{
    public ShipBuilderUI sie;
    public int id;
    public void Setup(ShipBuilderUI s, int idd)
    {
        gameObject.SetActive(true);
        sie = s;
        id = idd;
    }
    public void Clicked()
    {
      //  sie.Clicked(id);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
