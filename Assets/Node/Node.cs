using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
#if UnityEditor
[Serializable]
public class Node : ScriptableObject
{
    public int id;
    public Rect rect;
    public string title = "";
    public bool isDragged;
    public bool isSelected;

    public ConnectionPoint inPoint;
    public Dictionary<ConnectionPoint,int> outPoints;

    public GUIStyle style;
    public GUIStyle defaultNodeStyle;
    public GUIStyle selectedNodeStyle;

    public NodeDataSaver NodeDataSaver;
    public virtual void SetupStyles()
    {
        defaultNodeStyle = new GUIStyle();
        defaultNodeStyle.normal.background = EditorGUIUtility.Load("builtin skins/lightskin/images/node0.png") as Texture2D;
        defaultNodeStyle.border = new RectOffset(12, 12, 12, 12);

        selectedNodeStyle = new GUIStyle();
        selectedNodeStyle.normal.background = EditorGUIUtility.Load("builtin skins/lightskin/images/node0 on.png") as Texture2D;
        selectedNodeStyle.border = new RectOffset(12, 12, 12, 12);
        style = new GUIStyle(  defaultNodeStyle);
        
    } 
   public void addCon(NodeBasedEditor nbe)
    {
        foreach (KeyValuePair<ConnectionPoint, int> cp in outPoints.ToArray())
        {
           // NodeBasedEditor nbe = NodeBasedEditor.window;
            
            if (cp.Value == -1) { continue; }
           // Debug.Log("worked");
          //  Debug.Log(nbe);
           // Debug.Log(cp.Value);
            ConnectionPoint cp1 = nbe.nodes[cp.Value].inPoint;
           // Debug.Log(cp1);
            ConnectionPoint cp2 = cp.Key;
          //  Debug.Log(cp2);
           
           
            nbe.CreateConnection(cp1, cp2);



        }
    }
    public virtual void NodeSetup(Vector2 position, string nodeInfo,int idd, List<int> resids)
    {
        id = idd;
        NodeDataSaver = new NodeDataSaver(position, nodeInfo,NodeType.Text, resids);
        rect = new Rect(position.x, position.y, 100, 100);
       
        inPoint = new ConnectionPoint(this, ConnectionPointType.In, 10);
        outPoints = new Dictionary<ConnectionPoint, int>();
        SetupStyles();
        StringSetup(nodeInfo);
        
    }
    public virtual void StringSetup(string s)
    {

    }
    public void Drag(Vector2 delta)
    {
        rect.position += delta;
    }

    public virtual void Draw() { }
    
    public bool ProcessEvents(Event e)
    {
        bool sel = false;
        if(NodeBasedEditor.window != null )
        {
            if (NodeBasedEditor.window.select_multiple)
            {
                sel = true;
            }
        }
        switch (e.type)
        {
            case EventType.MouseDown:
                if (e.button == 0)
                {
                    if (rect.Contains(e.mousePosition))
                    {
                        isDragged = true;
                        GUI.changed = true;
                        isSelected = true;
                        style = selectedNodeStyle;
                    }
                    else if (sel)
                    {
                        isSelected = !isSelected;
                            GUI.changed = true;
                       
                    }
                    else if(!((new Rect(10, 10, 150, 20).Contains(e.mousePosition))|| (new Rect(10, 30, 150, 20).Contains(e.mousePosition))))
                    {
                        GUI.changed = true;
                        isSelected = false;
                        style = defaultNodeStyle;
                    }
                }

                if (e.button == 1 && isSelected && rect.Contains(e.mousePosition))
                {
                    ProcessContextMenu();
                    e.Use();
                }
                break;

            case EventType.MouseUp:
                isDragged = false;
                break;

            case EventType.MouseDrag:
                if (e.button == 0 && isDragged)
                {
                    Drag(e.delta);
                    e.Use();
                    return true;
                }
                break;
        }

        return false;
    }

    private void ProcessContextMenu()
    {
        GenericMenu genericMenu = new GenericMenu();
        genericMenu.AddItem(new GUIContent("Remove node"), false, OnClickRemoveNode);
        genericMenu.ShowAsContext();
    }

    public void OnClickRemoveNode()
    {
        if (NodeBasedEditor.window != null)
        {
            NodeBasedEditor.window.OnClickRemoveNode(this);
        }
    }
    public virtual string ToJson()
    {
        return "";
    }
    public NodeDataSaver getNodeDS()
    {
        NodeDataSaver.ResponseID.Clear();
        NodeDataSaver.position = rect.position;
        foreach (KeyValuePair<ConnectionPoint, int> cp in outPoints)
        {
           // Debug.Log(id + "-" + cp.Value);
            NodeDataSaver.ResponseID.Add(cp.Value);
        }

       
           // Debug.Log("NDS:"+NodeDataSaver.ResponseID.Count);
        
            NodeDataSaver nds = NodeDataSaver;
      //  Debug.Log("nds"+nds.ResponseID.Count);

        nds.data = ToJson();
        return nds;
    }
}



#endif
[Serializable]
public class NodeDataSaver
{
    public Vector2 position;
    public NodeType type;
    public string data;
    [SerializeField]

    public List<int> ResponseID = new List<int> ();

    public NodeDataSaver(Vector2 pos, string s,NodeType t ,List<int>inds)
    {
        position = pos;

        data = s;
        type = t;
        ResponseID = inds;
    }
    public int getResID(int id)
    {
        if (ResponseID.Count <= id)
        {
            return -1;
        }
        return ResponseID[id];
    }
}
