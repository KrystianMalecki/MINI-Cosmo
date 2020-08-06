using System;
using UnityEditor;
using UnityEngine;

public enum ConnectionPointType { In, Out }

public class ConnectionPoint
{
#if UNITY_EDITOR
    public Rect rect;

    public ConnectionPointType type;

    public Node node;
    public Node conectedto=null;
    public GUIStyle style;

    public int pos;
    public int state = 0;
    public void SetupStyle(ConnectionPointType type)
    {
        if (type == ConnectionPointType.In)
        {
            style = new GUIStyle();
            style.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/cmd left.png") as Texture2D;
            style.active.background = EditorGUIUtility.Load("builtin skins/darkskin/images/cmd left on.png") as Texture2D;
            style.border = new RectOffset(4, 4, 12, 12);
        }
        else
        {
            style = new GUIStyle();
            style.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/cmd right.png") as Texture2D;
            style.active.background = EditorGUIUtility.Load("builtin skins/darkskin/images/cmd right on.png") as Texture2D;
            style.border = new RectOffset(4, 4, 12, 12);
        }
    }
    public ConnectionPoint(Node node, ConnectionPointType type,int offset)
    {
        SetupStyle(type);
        this.node = node;
        this.type = type;

        rect = new Rect(0, 0, 15f, 20f);
        pos = offset;
    }

    public void Draw()
    {
        rect.y = node.rect.y +pos ;

        switch (type)
        {
            case ConnectionPointType.In:
                rect.x = node.rect.x - rect.width + 8f;
                break;

            case ConnectionPointType.Out:
                rect.x = node.rect.x + node.rect.width - 8f;
                break;
        }
        if(state == 1)
        {
            GUI.color = Color.green;

        }
        else if (state == 2)
        {
            GUI.color = Color.red;
        }
        if (GUI.Button(rect, "", style))
        {
            if (NodeBasedEditor.window != null)
            {
                NodeBasedEditor.window.OnClickPoint(this);
            }
        }
        GUI.color = Color.white;
    }
#endif
}
