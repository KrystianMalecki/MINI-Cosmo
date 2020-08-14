using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class UENText : UEditorNode
{
    /* public GUIStyle defaultStyle;
     public GUIStyle selectedStyle;*/
    public SerializedObject so;
    public SerializedProperty sp;
    public SerializedProperty colsp;

    public Sprite CharacterTexture;
    public Texture2D t;
    public Color col;
    public override void Setup(UNode data)
    {
        base.Setup(data);
        setRect();
        for (int a = 0; a < nodeData.outs.Count; a++)
        {
            ConOuts.Add(new UConnector(UConnectorType.Out, this, a, a * 25 + 15 + 25 + 105 + 65, Color.white));
        }
        so = new SerializedObject(this);
        sp = so.FindProperty("CharacterTexture");
        colsp = so.FindProperty("col");

        if (nodeData.charSpr != null)
        {
            CharacterTexture = nodeData.charSpr;
        }
    }
    public void setRect()
    {
        nodeData.rect = new Rect(nodeData.rect.position, new Vector2(250, 200 + (nodeData.responses.Count - 1) * 25 + 5 + 65));

    }
    public override void StyleSetup()
    {
        base.StyleSetup();
        style = new GUIStyle(style);

        off.background = EditorGUIUtility.Load("builtin skins/lightskin/images/node0.png") as Texture2D;
        on.background = EditorGUIUtility.Load("builtin skins/lightskin/images/node0 on.png") as Texture2D;
        style.normal = off;
    }

    public override void Draw()
    {
        y = 0;
        base.Draw();
        nodeData.characterName = GUI.TextField(makeRect(0, y, 20), nodeData.characterName);
        nodeData.text = GUI.TextField(makeRect(0, y, 100), nodeData.text);
        so.Update();

        EditorGUI.BeginChangeCheck();
        EditorGUI.PropertyField(makeRect(0, y, 60), sp);

        GUI.color = new Color(0.35f, 0.35f, 0.35f);
        GUI.DrawTexture(new Rect(nodeData.rect.x + 15, nodeData.rect.y + y - 35, 50, 50), t);
        GUI.color = Color.white;
        if (nodeData.charSpr)
        {
            GUI.DrawTexture(new Rect(nodeData.rect.x + 15, nodeData.rect.y + y - 35, 50, 50), nodeData.charSpr.texture);
        }

        for (int a = 0; a < nodeData.outs.Count; a++)
        {
            nodeData.responses[a] = GUI.TextField(makeRect(0, y, 20, 195), nodeData.responses[a]);
            GUI.color = Color.red;
          
            y -= 25;
            if (GUI.Button(makeRect(200, y, 20, 20), "X"))
            {
                nodeData.responses.RemoveAt(a);
                nodeData.outs.RemoveAt(a);
                ConOuts = new List<UConnector>();
                for (int h = 0; h < nodeData.outs.Count; h++)
                {
                    ConOuts.Add(new UConnector(UConnectorType.Out, this, h, h * 25 + 15 + 25 + 105 + 65, Color.white));
                }
                UDialogueNE.window.cons = new List<UConnection>();

                for (int c = 0; c < UDialogueNE.window.nodes.Count; c++)
                {

                    if (UDialogueNE.window.nodes[c].nodeData.outs != null)
                    {
                        for (int d = 0; d < UDialogueNE.window.nodes[c].nodeData.outs.Count; d++)
                        {
                            if (UDialogueNE.window.nodes[c].nodeData.outs[d] != -1)
                            {
                                UDialogueNE.window.AddConnection(UDialogueNE.window.nodes[c].nodeData.id, d, UDialogueNE.window.nodes[c].nodeData.outs[d]);
                            }
                        }
                    }
                }
                setRect();
            }
            GUI.color = Color.white;
            //  ConOuts[a].Draw();
        }
        if (GUI.Button(makeRect(0, y, 20), "Add new response"))
        {
            AddResponse();
        }

        so.ApplyModifiedProperties();
        if (EditorGUI.EndChangeCheck())
        {
            // nodeData.funcData = JsonUtility.ToJson(funcs);
            nodeData.charSpr = CharacterTexture;

            setRect();
        }


    }
    public void AddResponse()
    {
        nodeData.outs.Add(-1);
        nodeData.responses.Add("Response number " + nodeData.responses.Count);
        ConOuts.Add(new UConnector(UConnectorType.Out, this, nodeData.responses.Count - 1, (nodeData.responses.Count - 1) * 25 + 15 + 25 + 105 + 65, Color.white));
        setRect();

    }
}
