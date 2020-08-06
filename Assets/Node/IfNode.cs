using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;
#if UNITY_EDITOR
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
        rect = new Rect(rect.position, new Vector2(340, 85));
        NodeDataSaver.type = NodeType.If;

        if (nodeInfo == "")
        {
            data = new IfNodeNodeData(condition);
        }
        else
        {

            condition = JsonUtility.FromJson<Condition>(data.cond_json);
        }
        ConnectionPoint cp1 = new ConnectionPoint(this, ConnectionPointType.Out, 10);
        ConnectionPoint cp2 = new ConnectionPoint(this, ConnectionPointType.Out, 50);
        cp1.state = 1;
        cp2.state = 2;

        outPoints.Add(cp1, NodeDataSaver.getResID(0));
        outPoints.Add(cp2, NodeDataSaver.getResID(1));

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
        GUI.Label(new Rect(rect.position+new Vector2(300,10),new Vector2(40,20)), "true");
        GUI.Label(new Rect(rect.position + new Vector2(300, 50), new Vector2(40, 20)), "false");
        so.Update();

        EditorGUI.PropertyField(new Rect(rect.position + new Vector2(10, 10), new Vector2(280, 60)), sp);

        outPoints.ElementAt(0).Key.Draw();
        outPoints.ElementAt(1).Key.Draw();

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

#endif
