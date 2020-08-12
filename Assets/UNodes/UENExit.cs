
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Events;
using System;

public class UENExit : UEditorNode
{
    public GUIStyle guic = new GUIStyle();
    public override void Setup(UNode data)
    {
        base.Setup(data);

         ConIn = new UConnector(UConnectorType.In, this, 0, 17.5f, Color.white);



        setRect();
    }
    public void setRect()
    {
        nodeData.rect = new Rect(nodeData.rect.position, new Vector2(80, 55));

    }
    public override void StyleSetup()
    {
        base.StyleSetup();
        style = new GUIStyle(style);

        off.background = EditorGUIUtility.Load("builtin skins/lightskin/images/node6.png") as Texture2D;
        on.background = EditorGUIUtility.Load("builtin skins/lightskin/images/node6 on.png") as Texture2D;
        guic.fontSize = 20;
        guic.fontStyle = FontStyle.Bold;

        style.normal = off;
    }

    public override void Draw()
    {
        y = 0;
        base.Draw();
     

        GUI.Label(makeRect(0, 0, 30), "Exit", guic);


    }

}

