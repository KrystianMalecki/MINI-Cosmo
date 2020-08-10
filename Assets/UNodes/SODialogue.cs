using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New SODialogue", menuName = "Custom/SODialogue")]

public class SODialogue : ScriptableObject
{
    public List<UNode> nodes = new List<UNode>();
    /* [Multiline(20)]
     public string JsonPacked;
     public void JPack()
     {
         JsonPacked = JsonUtility.ToJson(new UNodePack(nodes));

     }
     public void JLoad()
     {
         JsonPacked = JsonUtility.ToJson(new UNodePack(nodes));

     }*/
    public void RemoveAt(int id)
    {
        
        foreach (UNode node in nodes)
        {

            for (int b = 0; b < node.outs.Count; b++)
            {
                if (node.outs[b] == id)
                {
                    node.outs[b] = -1;

                }else if (node.outs[b] > id)
                {
                    node.outs[b] -= 1;

                }

            }
        }
        nodes.RemoveAt(id);

        for (int a = 0; a < nodes.Count; a++)
        {
            nodes[a].id = a;

        }
    }
}
[System.Serializable]
public class UNodePack
{
    [SerializeField]
    public List<UNode> nodes = new List<UNode>();
    public UNodePack(List<UNode> list)
    {
        nodes = list;
    }
}
