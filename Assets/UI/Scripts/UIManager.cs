using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

[Serializable] 
public class UIDictionary : SerializableDictionary<string, UIBase> { }
public class UIManager : MonoBehaviour
{
    public float animtime=1f;
    public Animator anim;
    public UIDictionary UI_dictionary;
    public void Start()
    {
        HideAll();

        StaticDataManager.instance.setCombatMode(true);
        anim.Play("GoBack",0,1.0f);
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!GetMenu("Menu").IsOpen)
            {
                HideAll();
                OpenMenu("Menu");
            }
            else
            {
                HideAll();

                CloseMenu("Menu");

            }
        }
    }
    public void HideAll()
    {
        foreach(KeyValuePair<string,UIBase> pairs in UI_dictionary)
        {
            pairs.Value.CloseThis();
        }
    }
    public void quit()
    {
        CloseMenuTime();
        SceneManager.LoadScene(0);
    }
    public UIBase GetMenu(string s)
    {
        UIBase uib = null;
        UI_dictionary.TryGetValue(s, out uib);
        return uib;
    }
    public void OpenMenu(string s)
    {
        UIBase uib = GetMenu(s);
        uib.OpenThis();
        OpenMenuTime();
    }
    public void CloseMenu(string s)
    {
        UIBase uib = GetMenu(s);
        CloseMenuTime();

        StartCoroutine(Cwait(uib));
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
