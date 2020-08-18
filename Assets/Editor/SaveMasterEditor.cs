using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SaveMaster))]
public class SaveMasterEditor : Editor
{

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        SaveMaster sod = (SaveMaster)target;
        if (GUILayout.Button("Save"))
        {
            sod.SaveSave(sod.currentSave, sod.saveName);

        }
        if (GUILayout.Button("Load"))
        {
            sod.currentSave =  sod.LoadSave( sod.saveName);

        }

    }
}
