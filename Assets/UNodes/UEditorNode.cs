using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class UEditorNode : ScriptableObject
{
    public UNode nodeData;
    public GUIStyle style;
    public List<UConnector> ConOuts = new List<UConnector>();
    public UConnector ConIn;
    public float y = 15;
    public bool isDragged;
    public bool isSelected;
    public GUIStyleState on = new GUIStyleState();
    public GUIStyleState off = new GUIStyleState();

    public virtual void Setup(UNode data)
    {
        nodeData = data;
        StyleSetup();
        ConIn = new UConnector(UConnectorType.In, this, -1,15, Color.white);
    }
    public virtual void StyleSetup()
    {
        style = new GUIStyle();

        off.background = EditorGUIUtility.Load("builtin skins/lightskin/images/node0.png") as Texture2D;
        on.background = EditorGUIUtility.Load("builtin skins/lightskin/images/node0 on.png") as Texture2D;
        style.normal = off;
        style.border = new RectOffset(12, 12, 12, 12);
    }
    public void Drag(Vector2 delta)
    {
        nodeData.rect.position += delta;
    }
    public virtual void Draw()
    {
    
        ConIn.Draw();
        drawOuts();
        GUI.Box(nodeData.rect, "", style);
        
       

    }
    public void drawOuts()
    {
        foreach (UConnector ucon in ConOuts)
        {
            ucon.Draw();
        }
    }
    public Rect makeRect(float offset_x, float offset_y, float height, float width)
    {
        Rect r = new Rect(nodeData.rect);
        r.width = width;
        r.height = height;
        r.y += offset_y + 15;
        r.x += offset_x + 15;
        y += height + 5;
        return r;
    }
    public Rect makeRect(float offset_x, float offset_y, float height)
    {
        return makeRect(offset_x, offset_y, height, nodeData.rect.width - 30);
    }
    public bool ProcessEvents(Event e)
    {
        /*bool sel = false;
        if (NodeBasedEditor.window != null)
        {
            if (NodeBasedEditor.window.select_multiple)
            {
                sel = true;
            }
        }*/
        switch (e.type)
        {
            case EventType.MouseDown:
                if (e.button == 0)
                {
                    if (nodeData.rect.Contains(e.mousePosition))
                    {
                        isDragged = true;
                        GUI.changed = true;
                        isSelected = true;
                        style.normal = on;

                    }
                    /*  else if (sel)
                      {
                          isSelected = !isSelected;
                          GUI.changed = true;

                      }*/
                    /* else if (!((new Rect(10, 10, 150, 20).Contains(e.mousePosition)) || (new Rect(10, 30, 150, 20).Contains(e.mousePosition))))
                     {
                         GUI.changed = true;
                         isSelected = false;
                         style = off;
                     }*/
                    else
                    {
                        GUI.changed = true;
                        isSelected = false;
                        style.normal = off;
                    }
                }

                if (e.button == 1 && isSelected && nodeData.rect.Contains(e.mousePosition))
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
        genericMenu.AddItem(new GUIContent("Remove node"), false, ()=>UDialogueNE.window.RemoveNode(nodeData.id));
        genericMenu.ShowAsContext();
    }
}
