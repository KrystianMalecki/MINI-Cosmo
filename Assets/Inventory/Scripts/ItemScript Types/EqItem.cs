using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
[CreateAssetMenu(fileName = "New Eq Item", menuName = "Custom/Inv/Equipment Item")]
public class EqItem : Item
{
    
    public Vector2Int size = new Vector2Int(1,1);
    public TileType ttype;

    public LayoutMaker maker = new LayoutMaker();

    public void OnValidate()
    {
        if (maker == null)
        {
            maker = new LayoutMaker();
        }
        else if(maker.inv.Count==0)
        {
            maker = new LayoutMaker();

        }
        size = new Vector2Int( maker.inv.Count, maker.inv[0].line.Count);
    }
}
[Serializable]
public class LayoutMaker
{
    [SerializeField]
    public List<smline> inv = new List<smline> { new smline(new List<bool> { false }) };
   
}
[Serializable]
public class smline
{
    public List<bool> line = new List<bool>();
    public smline(List<bool> lin)
    {
        line = lin;
    }
}

