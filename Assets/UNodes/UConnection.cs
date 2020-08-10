using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class UConnection 
{
    public UConnector inPoint;
    public UConnector outPoint;
    public Color color = Color.white;
    public float thickness=5;
    public UConnection(UConnector inPoint, UConnector outPoint)
    {
        this.inPoint = inPoint;
        this.outPoint = outPoint;
    }
    public UConnection(UConnector inPoint, UConnector outPoint, Color c)
    {
        this.inPoint = inPoint;
        this.outPoint = outPoint;
        color = c;
    }
    public void Draw()
    {
       if (UDialogueNE.window != null)
        {
            UDialogueNE.window.DrawLine(inPoint.rect.center, outPoint.rect.center,color);
        }

        Handles.color = Color.red;
        if (Handles.Button((inPoint.rect.center + outPoint.rect.center) * 0.5f, Quaternion.identity, 4, 16, Handles.RectangleCap))
        {
            if (UDialogueNE.window != null)
            {
                UDialogueNE.window.RemoveConnection(inPoint,outPoint);
            }
        }

    }
}
