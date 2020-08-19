using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public WeaponUIManager wuim;

    public void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);

        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

    }
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
