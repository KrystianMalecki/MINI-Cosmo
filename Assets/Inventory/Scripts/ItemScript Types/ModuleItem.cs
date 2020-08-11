using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
[CreateAssetMenu(fileName = "New Module", menuName = "Custom/Inv/Module Item")]
public class ModuleItem : EqItem
{
    public TestModuleData tmd;
    public virtual void Setup()
    {
        ModuleEventDataStorage.instance.AddData("sid maker", tmd);
    }
}
public class TestModuleData
{
    public int a=0;
    public string b="b";
}
