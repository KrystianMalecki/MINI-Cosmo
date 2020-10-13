using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AIEditor : EditorWindow
{
    private Vector2 offset;
    private Vector2 drag;

    public static AIEditor window;
    public AIBrain current;
    public List<AINode> nodes = new List<AINode>();
    [MenuItem("Window/AI Node Editor")]
    public static void OpenWindow()
    {

        window = GetWindow<AIEditor>();
        window.titleContent = new GUIContent("AI Node Editor");
    }
    public static void OpenWindow(AIBrain aib)
    {

        OpenWindow();
        window.current = aib;
    }
    public void OnEnable()
    {
        LoadNodes(current.mainNode);
    }
    public void LoadNodes(AINode node)
    {
      
        nodes.Add(node);
        foreach(AINode noder in node.outputs)
        {
            LoadNodes(noder);
        }
    }
    public void DrawNodes()
    {

    }
    public void OnGUI()
    {

        DrawGrid(20, 0.2f, Color.gray);
        DrawNodes();

    }
    private void DrawGrid(float gridSpacing, float gridOpacity, Color gridColor)
    {
        int widthDivs = Mathf.CeilToInt(position.width / gridSpacing);
        int heightDivs = Mathf.CeilToInt(position.height / gridSpacing);

        Handles.BeginGUI();
        Handles.color = new Color(gridColor.r, gridColor.g, gridColor.b, gridOpacity);

        offset += drag * 0.5f;
        Vector3 newOffset = new Vector3(offset.x % gridSpacing, offset.y % gridSpacing, 0);

        for (int i = 0; i < widthDivs; i++)
        {
            Handles.DrawLine(new Vector3(gridSpacing * i, -gridSpacing, 0) + newOffset, new Vector3(gridSpacing * i, position.height, 0f) + newOffset);
        }

        for (int j = 0; j < heightDivs; j++)
        {
            Handles.DrawLine(new Vector3(-gridSpacing, gridSpacing * j, 0) + newOffset, new Vector3(position.width, gridSpacing * j, 0f) + newOffset);
        }

        Handles.color = Color.white;
        Handles.EndGUI();
    }
}
