using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class UDialogueNE : EditorWindow
{
    public List<UEditorNode> nodes = new List<UEditorNode>();
    public SODialogue dialogueSelected;
    private SerializedProperty sp;
    private SerializedObject so;
    public static UDialogueNE window;
    private Vector2 offset;
    private Vector2 drag;
    public Texture t;
    private Rect ToolBoxRect;
    public List<UConnection> cons = new List<UConnection>();
    private UConnector selectedInPoint;
    private UConnector selectedOutPoint;
    private float thickness = 5;

    [MenuItem("Window/Dialogue Node Editor")]
    public static void OpenWindow()
    {

        window = GetWindow<UDialogueNE>();
        window.titleContent = new GUIContent("Dialogue Node Editor");
    }
    public static void OpenWindow(SODialogue sod)
    {

        OpenWindow();
        window.dialogueSelected = sod;
    }
    public void LoadData()
    {
        if (dialogueSelected != null)
        {
            if (dialogueSelected.nodes.Count < 1)
            {
                AddNode(new Vector2(0, 0), UNodeType.Start);
            }
            nodes = new List<UEditorNode>();
            cons = new List<UConnection>();
            int b = 0;
            for (int a = 0; a < nodes.Count; a++)
            {
                if (dialogueSelected.nodes.Count > a)
                {
                    nodes[a].Setup(dialogueSelected.nodes[a]);
                    b++;

                }
            }
            for (int a = b; a < dialogueSelected.nodes.Count; a++)
            {
                AddEditorNode(dialogueSelected.nodes[a]);
            }
            for (int a = 0; a < nodes.Count; a++)
            {

                if (nodes[a].nodeData.outs != null)
                {
                    for (int c = 0; c < nodes[a].nodeData.outs.Count; c++)
                    {
                        if (nodes[a].nodeData.outs[c] != -1)
                        {
                            AddConnection(nodes[a].nodeData.id, c, nodes[a].nodeData.outs[c]);
                        }
                    }
                }
            }
        }
    }
    public void OnEnable()
    {
        ToolBoxRect = new Rect(0, 0, position.width, 20);

        so = new SerializedObject(this);
        sp = so.FindProperty("dialogueSelected");
        if (dialogueSelected != null)
        {
            LoadData();
        }/* UEditorNode uen = CreateInstance<UEditorNode>();
         dialogueSelected.nodes.Add(new UNode(new Vector2(0, 0)));
         uen.Setup(dialogueSelected.nodes[0]);
         nodes.Add(uen);*/
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

    public void OnGUI()
    {

        DrawGrid(20, 0.2f, Color.gray);
        DrawMenu();
        if (dialogueSelected != null)
        {
            DrawNodes();
            DrawConnections();
        }
        DrawConnectionLine(Event.current);

        ProcessNodeEvents(Event.current);
        ProcessEvents(Event.current);

    }
    private void DrawMenu()
    {
        GUI.color = Color.grey;
        GUI.DrawTexture(ToolBoxRect, t);
        GUI.color = Color.white;

        so.Update();
        EditorGUI.BeginChangeCheck();
        EditorGUI.PropertyField(new Rect(0, 0, 280, 20), sp);
        so.ApplyModifiedProperties();

        if (EditorGUI.EndChangeCheck())
        {
            LoadData();

        }
    }
    public void warp()
    {
        //  OnDrag(Vector2.zero-)
    }
    private void OnDrag(Vector2 delta)
    {
        drag = delta;

        if (nodes != null)
        {
            for (int i = 0; i < nodes.Count; i++)
            {
                nodes[i].Drag(delta);
            }
        }

        GUI.changed = true;
    }
    private void ProcessEvents(Event e)
    {
        drag = Vector2.zero;

        switch (e.type)
        {
            case EventType.MouseDown:
                if (e.button == 0)
                {
                    ClearConnectionSelection();
                }

                if (e.button == 1)
                {
                    ShowMenu(e.mousePosition);
                }
                break;

            case EventType.MouseDrag:
                if (e.button == 0)
                {
                    OnDrag(e.delta);
                }
                break;
        }
    }
    private void DrawNodes()
    {

        for (int i = 0; i < nodes.Count; i++)
        {
            nodes[i].Draw();
        }

    }
    private void ShowMenu(Vector2 mousePosition)
    {
        GenericMenu genericMenu = new GenericMenu();
        genericMenu.AddItem(new GUIContent("Add Text Node"), false, () => AddNode(mousePosition, UNodeType.Text));
        genericMenu.AddItem(new GUIContent("Add Function Node"), false, () => AddNode(mousePosition, UNodeType.Function));
        genericMenu.AddItem(new GUIContent("Add If Node"), false, () => AddNode(mousePosition, UNodeType.If));
        genericMenu.AddItem(new GUIContent("Add Exit Node"), false, () => AddNode(mousePosition, UNodeType.Exit));

        genericMenu.ShowAsContext();
    }
    public void AddNode(Vector2 position, UNodeType type)
    {
        UNode node = new UNode(position, dialogueSelected.nodes.Count);
        node.type = type;
        dialogueSelected.nodes.Add(node);
        AddEditorNode(node);

    }
    public void AddEditorNode(UNode nodeData)
    {
        switch (nodeData.type)
        {
            case UNodeType.Text:
                {
                    UENText uen = CreateInstance<UENText>();
                    uen.Setup(nodeData);
                    nodes.Add(uen);
                    break;
                }
            case UNodeType.Function:
                {
                    UENFunc uen = CreateInstance<UENFunc>();
                    uen.Setup(nodeData);
                    nodes.Add(uen);
                    break;
                }
            case UNodeType.If:
                {
                    UENIf uen = CreateInstance<UENIf>();
                    uen.Setup(nodeData);
                    nodes.Add(uen);
                    break;
                }
            case UNodeType.Start:
                {
                    UENStarter uen = CreateInstance<UENStarter>();
                    uen.Setup(nodeData);
                    nodes.Add(uen);
                    break;
                }
            case UNodeType.Exit:
                {
                    UENExit uen = CreateInstance<UENExit>();
                    uen.Setup(nodeData);
                    nodes.Add(uen);
                    break;
                }
        }

    }
    public void RemoveConnection(UConnector outUC, UConnector inUC)
    {

        outUC.node.nodeData.outs[outUC.ConnectionId] = -1;
        cons.Remove(cons.Find(x => x.inPoint == outUC && x.outPoint == inUC));
    }

    public void AddConnection(int nodeID, int conID, int toID)
    {

        UConnection con = new UConnection(nodes[nodeID].ConOuts[conID], nodes[toID].ConIn, nodes[nodeID].ConOuts[conID].color);

        nodes[nodeID].nodeData.outs[conID] = toID;
        cons.Add(con);
    }
    public void DrawConnections()
    {
        foreach (UConnection ucon in cons.ToArray())
        {
            ucon.Draw();
        }
    }
    public void DrawLine(Vector2 p2, Vector2 p1, Color c)
    {
        Handles.DrawBezier(p1, p1 + Vector2.left * 20f, p1, p1 + Vector2.left * 20f, c, null, thickness);
        Handles.DrawBezier(p1 + Vector2.left * 19f, p2 - Vector2.left * 19f, p1 + Vector2.left * 19f, p2 - Vector2.left * 19f, c, null, thickness);
        Handles.DrawBezier(p2 - Vector2.left * 20f, p2, p2 - Vector2.left * 20f, p2, c, null, thickness);

    }
    private void DrawConnectionLine(Event e)
    {
        if (selectedInPoint != null && selectedOutPoint == null)
        {

            DrawLine(e.mousePosition, selectedInPoint.rect.center, selectedInPoint.color);
            GUI.changed = true;
        }

        if (selectedOutPoint != null && selectedInPoint == null)
        {

            DrawLine(selectedOutPoint.rect.center, e.mousePosition, selectedOutPoint.color);

            GUI.changed = true;
        }
    }
    public void OnClickPoint(UConnector uc)
    {

        if (uc.type == UConnectorType.In)
        {
            selectedInPoint = uc;


        }
        else
        {
            selectedOutPoint = uc;



        }
        if (selectedOutPoint != null && selectedInPoint != null)
        {
            if (selectedOutPoint.node != selectedInPoint.node)
            {
                if (selectedOutPoint.type == UConnectorType.Out)
                {
                    if (selectedOutPoint.node.nodeData.outs[selectedOutPoint.ConnectionId] < 0)
                    {

                        AddConnection(selectedOutPoint.node.nodeData.id, selectedOutPoint.ConnectionId, selectedInPoint.node.nodeData.id);
                        ClearConnectionSelection();
                    }
                }
                else
                {
                    AddConnection(selectedOutPoint.node.nodeData.id, selectedOutPoint.ConnectionId, selectedInPoint.node.nodeData.id);
                    ClearConnectionSelection();
                }

            }
            else
            {
                ClearConnectionSelection();
            }
        }
    }
    public void ClearConnectionSelection()
    {
        selectedInPoint = null;
        selectedOutPoint = null;
    }
    public void RemoveNode(int id)
    {
        dialogueSelected.RemoveAt(id);
        LoadData();
    }
    private void ProcessNodeEvents(Event e)
    {
        if (nodes != null)
        {
            for (int i = nodes.Count - 1; i >= 0; i--)
            {
                bool guiChanged = nodes[i].ProcessEvents(e);

                if (guiChanged)
                {
                    GUI.changed = true;
                }
            }
        }
    }
}

