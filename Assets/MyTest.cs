using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyTest : MonoBehaviour
{
    public bool b1;
    public string s;
    public void Start()
    {
        
       s= gameObject.name;
    }
    public bool BoolReturner()
    {
        return b1;
    }
    public void Say1()
    {
        Debug.LogError("say1");

    }
    public void Say2()
    {
        Debug.LogError("say2");
       
        StaticDataManager.instance.SpawnItemP(new Vector2(0, 0), new ItemData("scrap", 1));

    }
}
