using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
#if UnityEditor
using UnityEditor.U2D.Path.GUIFramework;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;
[Serializable]
public class FunctionNode : Node
{

    public SerializedObject so;
    public SerializedProperty sp;
    public UnityEvent onActivate = new UnityEvent();
    public override void NodeSetup(Vector2 position, string nodeInfo,int idd, List<int> resids)
    {
        base.NodeSetup(position, nodeInfo,idd, resids);
        rect = new Rect(rect.position,new Vector2(300,200));
        NodeDataSaver.type = NodeType.Function;

        
        outPoints.Add(new ConnectionPoint(this, ConnectionPointType.Out, 10), NodeDataSaver.getResID(0));
        so = new SerializedObject(this);
         sp = so.FindProperty("onActivate");
       
    }

    public override void SetupStyles()
    {
        defaultNodeStyle = new GUIStyle();
        defaultNodeStyle.normal.background = EditorGUIUtility.Load("builtin skins/lightskin/images/node1.png") as Texture2D;
        defaultNodeStyle.border = new RectOffset(12, 12, 12, 12);

        selectedNodeStyle = new GUIStyle();
        selectedNodeStyle.normal.background = EditorGUIUtility.Load("builtin skins/lightskin/images/node1 on.png") as Texture2D;
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
        rect = new Rect(rect.position, new Vector2(rect.width, 100 + Mathf.Max(0, (onActivate.GetPersistentEventCount()-1)) * 47));

        GUI.Box(rect, title, style);
        if (this == null)
        {
            Debug.LogError("huj");
        }
        so.Update();
       
        EditorGUI.PropertyField(new Rect(rect.position+new Vector2(10,10),new Vector2(280,30)), sp);
        /*for (int y = 0; y < onActivate.GetPersistentEventCount(); y++)
        {
            GUI.Label(new Rect(rect.position + new Vector2(10, 10 + y * 25), new Vector2(180, 20)), onActivate.GetPersistentTarget(y).name + "-" + onActivate.GetPersistentMethodName(y));
        }*/
        outPoints.ElementAt(0).Key.Draw();
        so.ApplyModifiedProperties();
    }



    public override string ToJson()
    {
        return JsonUtility.ToJson(onActivate);
    }
    public override void StringSetup(string s)
    {
     
      //  data = JsonUtility.FromJson<FunctionNodeData>(s);
        onActivate =JsonUtility.FromJson<UnityEvent> (s);
    }

}
[Serializable]
public class FunctionNodeData
{
    public string cond_json;
    public FunctionNodeData(UnityEvent c)
    {
        cond_json = JsonUtility.ToJson(c);
        //  Debug.Log("packed"+cond_json);
    }
}

#endif