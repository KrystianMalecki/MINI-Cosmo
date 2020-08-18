using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public void OnEnable()
    {
        if (PlayerPrefs.GetString("new_game") == "true")
        {

          
        }
        ShipHangar.instance.ships = new List<ShipData>();
        ShipHangar.instance.ships = SaveMaster.instance.currentSave.hangar_ships;
        Inventory.instance.current_inv = ShipHangar.instance.ships[0].itemInventory;
    }
}
