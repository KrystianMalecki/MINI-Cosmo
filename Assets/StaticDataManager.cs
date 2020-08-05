using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StaticDataManager : MonoBehaviour
{
    public GameObject DieExplosion;
    public GameObject BulletBase;
    public List<string> GoodTags= new List<string>();
    public List<string> NeutralTags = new List<string>();
    public List<string> EnemyTags = new List<string>();
    public List<string> IgnoreTags = new List<string>();

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
