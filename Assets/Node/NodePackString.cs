using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
[CreateAssetMenu(fileName = "New NodePackString", menuName = "NodePackString")]

public class NodePackString : ScriptableObject
{
    [SerializeField]
    public NodePack NodePack= new NodePack();
    public string save="";
    public void saver()
    {
        save = JsonUtility.ToJson(NodePack);

    }
    public void loader()
    {
        NodePack = JsonUtility.FromJson<NodePack>(save);
    }
}
public class NodePack 
{
    [SerializeField]
    public List<NodeDataSaver> nodes = new List<NodeDataSaver>();

}