using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;

public class SaveMaster : MonoBehaviour
{
    public static SaveMaster instance;
    public string saveName = "1";
    public Save startSave;
    [Header("Current")]
    public Save currentSave = new Save();
    public List<ShipDataBase> shipbases = new List<ShipDataBase>();
    public void SaveSave(Save save, string name)
    {
        BinaryFormatter bf = new BinaryFormatter();

        FileStream file = File.Create(Application.persistentDataPath
                     + "/Save-" + name + ".dat");
     
       // save.hangar_ships.ForEach(x => x.SDBid = shipbases.FindIndex(y => y == x.BasedOn));
        bf.Serialize(file, save);
        file.Close();
        Debug.Log("Saved!");
    }
    public Save LoadSave(string name)
    {
        Save data = null;
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file =
                   File.Open(Application.persistentDataPath
                     + "/Save-" + name + ".dat", FileMode.Open);
        data = (Save)bf.Deserialize(file);
        foreach(ShipData v in data.hangar_ships)
        {
            if (v.SDBid == -1)
            {
                Debug.LogError("ERROR! Base for ship: " + v.name + " not found!");
            }
            v.BasedOn = shipbases[v.SDBid];
        }
        file.Close();

        Debug.Log("Game data loaded!");

        return data;


    }
    public void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        if (!PlayerPrefs.HasKey("new_game"
           
       ))
        {
            PlayerPrefs.SetString("new_game", "true");
            SaveSave(startSave, saveName);
            currentSave=LoadSave(saveName);
            currentSave.hangar_ships[0].makeNew();
        }
        else
        {
            currentSave = LoadSave(saveName);

        }

    }
    public void OnDestroy()
    {

        SaveSave(currentSave, saveName);

    }

}
