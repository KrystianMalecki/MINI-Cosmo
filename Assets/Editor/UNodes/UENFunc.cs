
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Events;

public class UENFunc : UEditorNode
{
    public SerializedObject so;
    public SerializedProperty sp;
    [SerializeField]
    public UnityEvent funcs = new UnityEvent();
    public override void Setup(UNode data)
    {
        base.Setup(data);

        if (nodeData.outs.Count < 1)
        {
            nodeData.outs.Add(-1);
        }
        ConOuts.Add(new UConnector(UConnectorType.Out, this, 0, 15, Color.white));
        so = new SerializedObject(this);
        sp = so.FindProperty("funcs");
        /*if (nodeData.funcData != "")
        {
            funcs = JsonUtility.FromJson<UnityEvent>(nodeData.funcData);
        }*/
          if (nodeData.funcData2 != null)
          {
              funcs = nodeData.funcData2;
          }
        setRect();
    }
    public void setRect()
    {
        int a = 0;
        if (funcs != null)
        {
            a = funcs.GetPersistentEventCount();
        }

        nodeData.rect = new Rect(nodeData.rect.position, new Vector2(300, 120 + Mathf.Max(0, (a - 1)) * 47));

    }
    public override void StyleSetup()
    {
        base.StyleSetup();
        style = new GUIStyle(style);

        off.background = EditorGUIUtility.Load("builtin skins/lightskin/images/node1.png") as Texture2D;
        on.background = EditorGUIUtility.Load("builtin skins/lightskin/images/node1 on.png") as Texture2D;
        style.normal = off;
    }

    public override void Draw()
    {
        y = 0;
        base.Draw();

        so.Update();
        EditorGUI.BeginChangeCheck();
        EditorGUI.PropertyField(makeRect(0, 0, 30), sp); ;

        so.ApplyModifiedProperties();
        if (EditorGUI.EndChangeCheck())
        {
            // nodeData.funcData = JsonUtility.ToJson(funcs);
            nodeData.funcData2 = funcs;

            setRect();
        }


    }

}

