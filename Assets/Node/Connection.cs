using System;
using UnityEditor;
using UnityEngine;

public class Connection
{
#if UNITY_EDITOR
    public ConnectionPoint inPoint;
    public ConnectionPoint outPoint;
    public Color color = Color.white;
    public Connection(ConnectionPoint inPoint, ConnectionPoint outPoint)
    {
        this.inPoint = inPoint;
        this.outPoint = outPoint;
    }
    public Connection(ConnectionPoint inPoint, ConnectionPoint outPoint,Color c)
    {
        this.inPoint = inPoint;
        this.outPoint = outPoint;
        color = c;
    }
    public void Draw()
    {
        Handles.DrawBezier(inPoint.rect.center, inPoint.rect.center + Vector2.left * 20f, inPoint.rect.center, inPoint.rect.center + Vector2.left * 20f,Color.white,null,8);
        Handles.DrawBezier( inPoint.rect.center + Vector2.left * 19f, outPoint.rect.center - Vector2.left * 19f, inPoint.rect.center + Vector2.left * 19f, outPoint.rect.center - Vector2.left * 19f, Color.white, null, 8);
        Handles.DrawBezier( outPoint.rect.center - Vector2.left * 20f, outPoint.rect.center, outPoint.rect.center - Vector2.left * 20f, outPoint.rect.center, Color.white, null, 8);

        /*   Handles.DrawBezier(
               inPoint.rect.center,
               outPoint.rect.center,
               inPoint.rect.center + Vector2.left * 50f,
               outPoint.rect.center - Vector2.left * 50f,
               color,
               null,
               2f
           );*/
        Handles.color = Color.red;
        if (Handles.Button((inPoint.rect.center + outPoint.rect.center) * 0.5f, Quaternion.identity, 4, 8, Handles.RectangleCap))
        {
            if (NodeBasedEditor.window != null)
            {
                NodeBasedEditor.window.OnClickRemoveConnection(this);
            }
        }

    }
#endif
}
