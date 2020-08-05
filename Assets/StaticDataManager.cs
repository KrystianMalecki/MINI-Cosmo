﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StaticDataManager : MonoBehaviour
{
    public GameObject DieExplosion;
    public GameObject BulletBase;
    public GameObject CSOInfo;
    public List<string> GoodTags= new List<string>();
    public List<string> NeutralTags = new List<string>();
    public List<string> EnemyTags = new List<string>();
    public List<string> IgnoreTags = new List<string>();
    public bool CombatMode=false;
    public GameObject CombatModeDisplay;
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
        CombatModeDisplay.SetActive(CombatMode);

    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            CombatMode = !CombatMode;
            CombatModeDisplay.SetActive(CombatMode);
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
