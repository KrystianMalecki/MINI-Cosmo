
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Events;
using System;

[Serializable]
public class conditioner : SerializableCallback< bool>
{
    public conditioner()
    {

    }
}
public class UENIf : UEditorNode
{
    public SerializedObject so;
    public SerializedProperty sp;
    public conditioner funcs = new conditioner();
    public override void Setup(UNode data)
    {
        base.Setup(data);

        while(nodeData.outs.Count < 2)
        {
            nodeData.outs.Add(-1);
        }
        setRect();
        ConOuts.Add(new UConnector(UConnectorType.Out, this, 0, 15, Color.green));
        ConOuts.Add(new UConnector(UConnectorType.Out, this, 1, 70, Color.red));

      

        so = new SerializedObject(this);
        sp = so.FindProperty("funcs");
        if (nodeData.funcData != "")
        {
            funcs = JsonUtility.FromJson<conditioner>(nodeData.ifData);
        }
        setRect();
    }
    public void setRect()
    {
        nodeData.rect = new Rect(nodeData.rect.position, new Vector2(350, 105));

    }
    public override void StyleSetup()
    {
        base.StyleSetup();
        style = new GUIStyle(style);

        off.background = EditorGUIUtility.Load("builtin skins/lightskin/images/node2.png") as Texture2D;
        on.background = EditorGUIUtility.Load("builtin skins/lightskin/images/node2 on.png") as Texture2D;
        style.normal = off;
    }

    public override void Draw()
    {
        y = 0;
        base.Draw();

        so.Update();
        EditorGUI.BeginChangeCheck();
        EditorGUI.PropertyField(makeRect(0, 0, 75), sp); 
        
        so.ApplyModifiedProperties();
        if (EditorGUI.EndChangeCheck())
        {
            nodeData.ifData = JsonUtility.ToJson(funcs);
            setRect();
        }

    }

}

