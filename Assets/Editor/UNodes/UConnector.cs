using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public enum UConnectorType { In, Out }
public class UConnector
{
    public UEditorNode node;
    public Rect rect;
    public int ConnectionId = -1;
    public UConnectorType type;
    public GUIStyle style;
    public Color color = Color.white;
    private float f;
    public float offset;
    public UConnector(UConnectorType type, UEditorNode n, int conid, float off, Color cc)
    {
        color = cc;
        rect = new Rect(0, 0, 15f, 20f);
        node = n;
        offset = off;
        style = new GUIStyle();
        style.border = new RectOffset(4, 4, 12, 12);
        ConnectionId = conid;
        this.type = type;
        switch (type)
        {
            case UConnectorType.In:
                {
                    f = -rect.width + 8f;

                    style.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/cmd left.png") as Texture2D;
                    style.active.background = EditorGUIUtility.Load("builtin skins/darkskin/images/cmd left on.png") as Texture2D;
                    break;
                }
            case UConnectorType.Out:
                {
                    f = node.nodeData.rect.width - 8f;
                    style.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/cmd right.png") as Texture2D;
                    style.active.background = EditorGUIUtility.Load("builtin skins/darkskin/images/cmd right on.png") as Texture2D;

                    break;
                }
        }
       
    }

    public void Draw()
    {


        rect.y = node.nodeData.rect.y + offset;
        rect.x = node.nodeData.rect.x + f;
        GUI.color = color;


        if (GUI.Button(rect, "", style))
        {
            if (UDialogueNE.window != null)
            {
                UDialogueNE.window.OnClickPoint(this);
            }
        }
        GUI.color = Color.white;
    }
}
