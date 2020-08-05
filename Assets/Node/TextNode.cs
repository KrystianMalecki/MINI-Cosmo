using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
#if UnityEditor
[Serializable]
public class TextNode : Node
{


    public TextNodeData data ;
    public override void NodeSetup(Vector2 position, string nodeInfo,int idd, List<int> resids) 
    {
        base.NodeSetup(position, nodeInfo,idd,  resids);
        NodeDataSaver.type = NodeType.Text;
        if (nodeInfo == "")
        {
            data = new TextNodeData();
        }
        rect = new Rect(rect.position.x, rect.position.y, 200, 185 + NodeDataSaver.ResponseID.Count * 25);

        for (int a = 0; a < NodeDataSaver.ResponseID.Count; a++)
        {
            AddResponse();
        }
    }

    public override void SetupStyles()
    {
        defaultNodeStyle = new GUIStyle();
        defaultNodeStyle.normal.background = EditorGUIUtility.Load("builtin skins/lightskin/images/node0.png") as Texture2D;
        defaultNodeStyle.border = new RectOffset(12, 12, 12, 12);

        selectedNodeStyle = new GUIStyle();
        selectedNodeStyle.normal.background = EditorGUIUtility.Load("builtin skins/lightskin/images/node0 on.png") as Texture2D;
        selectedNodeStyle.border = new RectOffset(12, 12, 12, 12);
        style = new GUIStyle(defaultNodeStyle);
        //for(int a=0;a<)
    }

    public override void Draw()
    {
        inPoint.Draw();

        GUIStyle gs = new GUIStyle();
        GUI.Box(rect, title, style);
        data.reponse = GUI.TextField(new Rect(rect.position + new Vector2(10, 10), new Vector2(180, 20)), data.reponse);

        data.namer = GUI.TextField(new Rect(rect.position + new Vector2(10, 35), new Vector2(180, 20)), data.namer);

        data.text = GUI.TextField(new Rect(rect.position + new Vector2(10, 55), new Vector2(180, 90)), data.text);
        int y = 150;
        foreach (KeyValuePair< ConnectionPoint,int> cp in outPoints.ToArray())
        {
            
            cp.Key.Draw();
            string n = "";
            if (cp.Key.conectedto != null)
            {


                if (cp.Key.conectedto is TextNode)
                {
                    n = ((TextNode)cp.Key.conectedto).data.reponse;
                }


            }
            GUI.Label(new Rect(rect.position + new Vector2(10, y), new Vector2(180, 20)), "Response: " + n);
            y += 25;
        }
        /*for (int a = 0; a < NodeDataSaver.ResponseID.Count; a++)
        {
            
        }*/
        if (GUI.Button(new Rect(rect.position + new Vector2(10, y), new Vector2(180, 20)), "Add new response"))
        {
            AddResponse();
        }
    }
    public void AddResponse()
    {
        
            outPoints.Add(new ConnectionPoint(this, ConnectionPointType.Out, 150 + 25 * outPoints.Count), NodeDataSaver.getResID(outPoints.Count));

            rect = new Rect(rect.position.x, rect.position.y, 200, 185 + outPoints.Count * 25);
        
    }

    public override string ToJson()
    {
        return JsonUtility.ToJson(data);
    }
    public override void StringSetup(string s)
    {
        data = JsonUtility.FromJson<TextNodeData>(s);
    }

}
[Serializable]
public class TextNodeData
{
    public Texture2D icon;
    public string namer;
    public string text;
    public string reponse;

    public TextNodeData()
    {
        icon = null;
        namer = "[CHARACTER NAME]";
        text = "[DIALOGUE TEXT]";
        reponse = "[RESPONSE]";
    }
}
#endif