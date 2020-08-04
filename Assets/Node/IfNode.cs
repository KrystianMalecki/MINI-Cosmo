using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;
using UnityEditor.U2D.Path.GUIFramework;
using UnityEditorInternal;
using UnityEngine.Events;
using UnityEngine.UIElements;
using UnityEditor.Build.Reporting;
using System.Linq;

[Serializable]


public class IfNode : Node
{

    public IfNodeNodeData data;
    public SerializedObject so;
    public SerializedProperty sp;
    public Condition condition;
    public override void NodeSetup(Vector2 position, string nodeInfo,int idd, List<int> resids)
    {
        base.NodeSetup(position, nodeInfo,idd,resids);
        rect = new Rect(rect.position, new Vector2(300, 85));
        NodeDataSaver.type = NodeType.If;

        if (nodeInfo == "")
        {
            data = new IfNodeNodeData(condition);
        }
        else
        {

            condition = JsonUtility.FromJson<Condition>(data.cond_json);
        }
       
        outPoints.Add(new ConnectionPoint(this, ConnectionPointType.Out, 10), NodeDataSaver.getResID(0));
        so = new SerializedObject(this);
        sp = so.FindProperty("condition");
       
    }

    public override void SetupStyles()
    {
        defaultNodeStyle = new GUIStyle();
        defaultNodeStyle.normal.background = EditorGUIUtility.Load("builtin skins/lightskin/images/node2.png") as Texture2D;
        defaultNodeStyle.border = new RectOffset(12, 12, 12, 12);

        selectedNodeStyle = new GUIStyle();
        selectedNodeStyle.normal.background = EditorGUIUtility.Load("builtin skins/lightskin/images/node2 on.png") as Texture2D;
        selectedNodeStyle.border = new RectOffset(12, 12, 12, 12);
        style = new GUIStyle(defaultNodeStyle);
        //  onActivate.AddListener(sayYO);
    }
    public void sayYO()
    {
        Debug.LogError("YO");
    }
    public override void Draw()
    {
        inPoint.Draw();

        GUI.Box(rect, title, style);
        if (this == null)
        {
            Debug.LogError("huj");
        }
        so.Update();

        EditorGUI.PropertyField(new Rect(rect.position + new Vector2(10, 10), new Vector2(280, 60)), sp);

        outPoints.ElementAt(0).Key.Draw();
        so.ApplyModifiedProperties();
    }



    public override string ToJson()
    {
       // Debug.LogError("TOJsonEnd:");
       // new IfNodeNodeData(condition);
        return JsonUtility.ToJson(new IfNodeNodeData(condition));
        
    }
    public override void StringSetup(string s)
    {
       
        data = JsonUtility.FromJson<IfNodeNodeData>(s);
    }

}
[Serializable]
public class IfNodeNodeData
{
    public string cond_json ;
    public IfNodeNodeData(Condition c)
    {
        cond_json = JsonUtility.ToJson(c);
      //  Debug.Log("packed"+cond_json);
    }
}


