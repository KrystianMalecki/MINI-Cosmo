using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System;
using System.Net.NetworkInformation;
public enum NodeType { Text,Function,If }
public class NodeBasedEditor : EditorWindow
{
    public List<Node> nodes = new List<Node>();
    public List<Connection> connections = new List<Connection>();


    private ConnectionPoint selectedInPoint;
    private ConnectionPoint selectedOutPoint;

    private Vector2 offset;
    private Vector2 drag;
    public static NodeBasedEditor window;
    [MenuItem("Window/Node Based Editor")]
    private static void OpenWindow()
    {
         window = GetWindow<NodeBasedEditor>();
       
        window.titleContent = new GUIContent("Node Based Editor");

    }

    private void OnEnable()
    {
       

        LoadData();
    }
    private void LoadData()
    {
        if (PlayerPrefs.HasKey("nodes"))
        {
            if (nodes == null)
            {
                nodes = new List<Node>();
            }
            nodes.Clear();
            NodePack np = new NodePack();
            np = JsonUtility.FromJson<NodePack>(PlayerPrefs.GetString("nodes"));
            foreach (NodeDataSaver n in np.nodes)
            {
               AddNewNode(n.type, n.position, n.data,n.ResponseID);

            }
            connections = new List<Connection>();
            foreach (Node n in nodes)
            {
                n.addCon(this);
            }
            // nodes = np.nodes;

        }
    }
    private void OnGUI()
    {
        DrawGrid(20, 0.2f, Color.gray);
        DrawGrid(100, 0.4f, Color.gray);

        DrawNodes();
        DrawConnections();

        DrawConnectionLine(Event.current);

        ProcessNodeEvents(Event.current);
        ProcessEvents(Event.current);
        
        if (GUI.changed) Repaint();
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

    private void AddNewNode(NodeType type, Vector2 pos,  string s, List<int> ids)
    {
        if (nodes == null)
        {
            nodes = new List<Node>();
        }
        switch (type)
        {
            case NodeType.Text:
                {
                    TextNode tn = CreateInstance<TextNode>();
                    tn.NodeSetup(pos, s, nodes.Count, ids);
                  nodes.Add(tn);

                    break;
                }
            case NodeType.Function:
                {
                    FunctionNode fn = CreateInstance<FunctionNode>();
                    fn.NodeSetup(pos, s, nodes.Count, ids);
                    nodes.Add(fn);

                    break;
                }
            case NodeType.If:
                {
                    IfNode fn = CreateInstance<IfNode>();
                    fn.NodeSetup(pos, s, nodes.Count, ids);
                    nodes.Add(fn);

                    break;
                }
        }

    }
    private void DrawNodes()
    {
        if (nodes != null)
        {
            for (int i = 0; i < nodes.Count; i++)
            {
                nodes[i].Draw();
            }
        }
    }

    private void DrawConnections()
    {
        if (connections != null)
        {
            for (int i = 0; i < connections.Count; i++)
            {
                connections[i].Draw();
            }
        }
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
                    ProcessContextMenu(e.mousePosition);
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

    private void DrawConnectionLine(Event e)
    {
        if (selectedInPoint != null && selectedOutPoint == null)
        {
            Handles.DrawBezier(
                selectedInPoint.rect.center,
                e.mousePosition,
                selectedInPoint.rect.center + Vector2.left * 50f,
                e.mousePosition - Vector2.left * 50f,
                Color.white,
                null,
                2f
            );

            GUI.changed = true;
        }

        if (selectedOutPoint != null && selectedInPoint == null)
        {
            Handles.DrawBezier(
                selectedOutPoint.rect.center,
                e.mousePosition,
                selectedOutPoint.rect.center - Vector2.left * 50f,
                e.mousePosition + Vector2.left * 50f,
                Color.white,
                null,
                2f
            );

            GUI.changed = true;
        }
    }

    private void ProcessContextMenu(Vector2 mousePosition)
    {
        GenericMenu genericMenu = new GenericMenu();
        genericMenu.AddItem(new GUIContent("Add Text Node"), false, () => AddNewNode(NodeType.Text, mousePosition, "",new List<int>()));
        genericMenu.AddItem(new GUIContent("Add Function Node"), false, () => AddNewNode(NodeType.Function, mousePosition, "", new List<int>()));
        genericMenu.AddItem(new GUIContent("Add If Node"), false, () => AddNewNode(NodeType.If, mousePosition, "", new List<int>()));

        genericMenu.ShowAsContext();
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

    public void OnClickPoint(ConnectionPoint cp)
    {
        if(cp.type == ConnectionPointType.In)
        {
            OnClickInPoint(cp);
        }
        else
        {
            OnClickOutPoint(cp);

        }
    }
    private void OnClickInPoint(ConnectionPoint inPoint)
    {
        selectedInPoint = inPoint;

        if (selectedOutPoint != null)
        {
            if (selectedOutPoint.node != selectedInPoint.node && selectedOutPoint.conectedto == null)
            {
                CreateConnection(selectedInPoint, selectedOutPoint);
                ClearConnectionSelection();
            }
            else
            {
                ClearConnectionSelection();
            }
        }
    }

    private void OnClickOutPoint(ConnectionPoint outPoint)
    {
        selectedOutPoint = outPoint;

        if (selectedInPoint != null)
        {
            if (selectedOutPoint.node != selectedInPoint.node&&selectedOutPoint.conectedto==null)
            {
                CreateConnection(selectedInPoint,selectedOutPoint);
                ClearConnectionSelection();
            }
            else
            {
                ClearConnectionSelection();
            }
        }
    }

    public void OnClickRemoveNode(Node node)
    {
        if (connections != null)
        {
            List<Connection> connectionsToRemove = new List<Connection>();

            for (int i = 0; i < connections.Count; i++)
            {
                if (connections[i].inPoint == node.inPoint/* || connections[i].outPoint == node.outPoint*/)
                {
                    connectionsToRemove.Add(connections[i]);
                }
            }

            for (int i = 0; i < connectionsToRemove.Count; i++)
            {
                connections.Remove(connectionsToRemove[i]);
            }

            connectionsToRemove = null;
        }

        nodes.Remove(node);
    }

    public void OnClickRemoveConnection(Connection connection)
    {
        connection.outPoint.conectedto = null;
        connections.Remove(connection);

    }

    public void CreateConnection(ConnectionPoint iner, ConnectionPoint outer)
    {
        if (connections == null)
        {
            connections = new List<Connection>();
        }
        outer.conectedto = iner.node;
        outer.node.outPoints[outer] = iner.node.id;
        connections.Add(new Connection(iner, outer));
    }

    private void ClearConnectionSelection()
    {
        selectedInPoint = null;
        selectedOutPoint = null;
    }
    public void OnDestroy()
    {
        NodePack np = new NodePack();
        foreach (Node n in nodes)
        {
            np.nodes.Add(n.getNodeDS());
        }
        string s = JsonUtility.ToJson(np);
        PlayerPrefs.SetString("nodes", s);
        window = null;
    }
}
