using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class UENText : UEditorNode
{
    /* public GUIStyle defaultStyle;
     public GUIStyle selectedStyle;*/
    public override void Setup(UNode data)
    {
        base.Setup(data);
        setRect();
        for (int a = 0; a < nodeData.outs.Count; a++)
        {
            ConOuts.Add(new UConnector(UConnectorType.Out, this, a, a * 25 + 15 + 25 + 105, Color.white));
        }
    }
    public void setRect()
    {
        nodeData.rect = new Rect(nodeData.rect.position, new Vector2(200, 200 + (nodeData.responses.Count - 1) * 25 + 5));

    }
    public override void StyleSetup()
    {
        base.StyleSetup();
        style = new GUIStyle(style);

        off.background = EditorGUIUtility.Load("builtin skins/lightskin/images/node0.png") as Texture2D;
        on.background = EditorGUIUtility.Load("builtin skins/lightskin/images/node0 on.png") as Texture2D;
        style.normal = off;
    }

    public override void Draw()
    {
        y = 0;
        base.Draw();
        nodeData.characterName = GUI.TextField(makeRect(0, y, 20), nodeData.characterName);
        nodeData.text = GUI.TextField(makeRect(0, y, 100), nodeData.text);
        for (int a = 0; a < nodeData.outs.Count; a++)
        {
            nodeData.responses[a] = GUI.TextField(makeRect(0, y, 20), nodeData.responses[a]);
            //  ConOuts[a].Draw();
        }
        if (GUI.Button(makeRect(0, y, 20), "Add new response"))
        {
            AddResponse();
        }


        
    }
    public void AddResponse()
    {
        nodeData.outs.Add(-1);
        nodeData.responses.Add("Response number " + nodeData.responses.Count);
        ConOuts.Add(new UConnector(UConnectorType.Out, this, nodeData.responses.Count - 1, (nodeData.responses.Count - 1) * 25 + 15 + 25 + 105, Color.white));
        setRect();

    }
}
