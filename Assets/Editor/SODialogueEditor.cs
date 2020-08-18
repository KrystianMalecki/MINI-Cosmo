using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(SODialogue))]
public class SODialogueEditor : Editor
{

    public override void OnInspectorGUI()
    {
        SODialogue sod = (SODialogue)target;
        if (GUILayout.Button("Open Editor"))
        {
            UDialogueNE.OpenWindow(sod);

        }
        base.OnInspectorGUI();

    }
}