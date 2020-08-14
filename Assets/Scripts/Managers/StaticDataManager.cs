using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;
[Serializable]
public class TileColors : SerializableDictionary<TileType, Color> { }
public class StaticDataManager : MonoBehaviour
{
    [Header("Static data")]
    public string TMProFormater = "<mspace=3.7>";
    public bool CombatMode = false;
    public TileColors colors;
    public static Dictionary<TileType, Color> yo = new Dictionary<TileType, Color> {
    { TileType.Null, new Color(0, 0, 0, 1f) } ,
    { TileType.Engine, new Color(0.5157232f, 1f, 0.4292453f, 1f) },
    { TileType.Weapon, new Color(1f, 0.4700375f, 0.427451f, 1f) },
    { TileType.Power, new Color(0.9721145f, 1f, 0.427451f, 1f) },
    { TileType.All, new Color(0.8773585f, 0.8773585f, 0.8773585f, 1f) },
    { TileType.Defense, new Color(0.3490566f, 0.3490566f, 0.3490566f, 1f) },

    };
    [Header("Tag lists")]
    public List<string> GoodTags = new List<string>();
    public List<string> NeutralTags = new List<string>();
    public List<string> EnemyTags = new List<string>();
    public List<string> IgnoreTags = new List<string>();
    [Header("GameObject Bases")]
    public GameObject ParticleBase;
    public GameObject OrbiterBase;
    public GameObject ItemPickUpBase;
    public GameObject DieExplosion;
    public GameObject BulletBase;
    public GameObject CSOInfo;

    public void SpawnItemP(Vector2 position, ItemData iD)
    {
        GameObject go = Instantiate(ItemPickUpBase, position, Quaternion.identity);
        go.GetComponent<ItemPickUp>().Setup(iD);
    }
    public static StaticDataManager instance;
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
        setCombatMode(true);

    }
    public void setCombatMode(bool t)
    {
        CombatMode = t;
        if (CombatMode)
        {
            Time.timeScale = 1f;
        }
        else
        {
            Time.timeScale = 0.05f;

        }
    }
    public bool isOkTarget(bool IsPlayer, string tag)
    {
        /*  Debug.Log(IsPlayer + " " + tag);
          Debug.Log(IsPlayer + " " + EnemyTags.Contains(tag) + " " + !IgnoreTags.Contains(tag));
          Debug.Log(!IsPlayer + " " + GoodTags.Contains(tag) + " " + !IgnoreTags.Contains(tag));
          Debug.Log((tag != null) + " " + (tag != "")+" "+(tag != "Untagged"));
          Debug.Log((IsPlayer && EnemyTags.Contains(tag) && !IgnoreTags.Contains(tag))+"-"+ (tag != null && tag != ""));*/
        return (((IsPlayer && EnemyTags.Contains(tag) && !IgnoreTags.Contains(tag)) ||
            (!IsPlayer && GoodTags.Contains(tag) && !IgnoreTags.Contains(tag))) && (tag != null && tag != ""));


    }
    public static Color TileToColor(TileType tt)
    {
        Color c = Color.white;
        if (yo.TryGetValue(tt, out c))
        {
            return c;
        }
        return c;
    }
}
