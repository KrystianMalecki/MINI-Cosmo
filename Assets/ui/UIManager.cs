using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable] 
public class UIDictionary : SerializableDictionary<string, UIBase> { }
public class UIManager : MonoBehaviour
{
    public float animtime=1f;
    public Animator anim;
    public UIDictionary UI_dictionary;
    public void Start()
    {
        foreach (KeyValuePair<string, UIBase> kvp in UI_dictionary)
        {
            kvp.Value.CloseThis();
        }
        StaticDataManager.instance.setCombatMode(true);
        anim.Play("GoBack",0,1.0f);
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!GetMenu("Inventory").IsOpen)
            {
                OpenMenu("Inventory");
            }
            else
            {
                CloseMenu("Inventory");

            }
        }
    }
    public UIBase GetMenu(string s)
    {
        UIBase uib = null;
        UI_dictionary.TryGetValue(s, out uib);
        return uib;
    }
    public UIBase OpenMenu(string s)
    {
        UIBase uib = GetMenu(s);
        uib.OpenThis();
        OpenMenuTime();
        return uib;
    }
    public UIBase CloseMenu(string s)
    {
        UIBase uib = GetMenu(s);
        CloseMenuTime();

        StartCoroutine(Cwait(uib));
        return uib;
    }
    public IEnumerator Cwait(UIBase uib)
    {
        yield return new WaitForSeconds(animtime);

        uib.CloseThis();

    }
    public void OpenMenuTime()
    {
        StaticDataManager.instance.setCombatMode(false);
        anim.Play("GoBlack");
    }
    public void CloseMenuTime()
    {
        StaticDataManager.instance.setCombatMode(true);
        anim.Play("GoBack");

    }
   
}
