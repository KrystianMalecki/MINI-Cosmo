using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StaticDataManager : MonoBehaviour
{
    [Header("Static data")]
    public string TMProFormater="<mspace=3.7>";
    public bool CombatMode = false;

    [Header("Tag lists")]
    public List<string> GoodTags= new List<string>();
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
    public bool isOkTarget(bool IsPlayer,string tag)
    {
      /*  Debug.Log(IsPlayer + " " + tag);
        Debug.Log(IsPlayer + " " + EnemyTags.Contains(tag) + " " + !IgnoreTags.Contains(tag));
        Debug.Log(!IsPlayer + " " + GoodTags.Contains(tag) + " " + !IgnoreTags.Contains(tag));
        Debug.Log((tag != null) + " " + (tag != "")+" "+(tag != "Untagged"));
        Debug.Log((IsPlayer && EnemyTags.Contains(tag) && !IgnoreTags.Contains(tag))+"-"+ (tag != null && tag != ""));*/
        return (((IsPlayer && EnemyTags.Contains(tag) && !IgnoreTags.Contains(tag)) ||
            (!IsPlayer && GoodTags.Contains(tag) && !IgnoreTags.Contains(tag)))&&(tag!=null&&tag!=""));


    }
  
}
