using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 
[Serializable]
public class MEDSstring : SerializableDictionary<string, string> { }
public class ModuleEventDataStorage : MonoBehaviour
{
    public static ModuleEventDataStorage instance;

    public MEDSstring Storage = new MEDSstring();
    public void AddData<T>(string sid,T data)
    {
        Storage.Add(sid, JsonUtility.ToJson(data));
    }
    public T GetData<T>(string sid)
    {
        string data = "";
        if(Storage.TryGetValue(sid,out data))
        {
            return JsonUtility.FromJson<T>(data);

        }
        return default(T);
    }
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
}
