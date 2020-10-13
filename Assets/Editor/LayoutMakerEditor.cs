using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

[CustomPropertyDrawer(typeof(LayoutMaker))]
public class LayoutMakerEditor : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
       
        SerializedProperty arrayProp = property.FindPropertyRelative("inv");
        int max = 0;
        Rect r = new Rect(position);

        r.x += 25;
        r.y += 25;
        {
            GUI.backgroundColor = Color.red;

            int f = 0;

            for (int a = 0; a < arrayProp.arraySize; a++)
            {

                int d = 0;
                if (a > 0)
                {
                    if (GUI.Button(new Rect(r.x - 25, r.y + f, 20, 20), "-"))
                    {
                        //lineProp.GetArrayElementAtIndex(b).boolValue = !lineProp.GetArrayElementAtIndex(b).boolValue;
                        arrayProp.DeleteArrayElementAtIndex(a);
                    }
                }
                //  max = d;
                if (arrayProp.arraySize > a)
                {
                    SerializedProperty lineProp = arrayProp.GetArrayElementAtIndex(a).FindPropertyRelative("line");

                    for (int b = 0; b < lineProp.arraySize; b++)
                    {
                        if (b > 0)
                        {
                            if (GUI.Button(new Rect(r.x + d, r.y - 25, 20, 20), "-"))
                            {
                                for (int h = 0; h < arrayProp.arraySize; h++)
                                {
                                    SerializedProperty lineProp2 = arrayProp.GetArrayElementAtIndex(h).FindPropertyRelative("line");

                                    lineProp2.DeleteArrayElementAtIndex(b);
                                }
                            }
                        }
                        d += 25;
                    }
                    f += 25;
                }
            }
        }

        int c = 0;
        GUI.backgroundColor = Color.white;

        for (int a = 0; a < arrayProp.arraySize; a++)
        {
            SerializedProperty lineProp = arrayProp.GetArrayElementAtIndex(a).FindPropertyRelative("line");
            // Debug.Log(lineProp.arraySize);

            int d = 0;

            for (int b = 0; b < lineProp.arraySize; b++)
            {
                GUI.backgroundColor = lineProp.GetArrayElementAtIndex(b).boolValue ? Color.green : Color.black;
                if (GUI.Button(new Rect(r.x + d, r.y + c, 20, 20), /*((int)lineProp.GetArrayElementAtIndex(b).boolValue).ToString()*/""))
                {
                    lineProp.GetArrayElementAtIndex(b).boolValue = !lineProp.GetArrayElementAtIndex(b).boolValue;
                }
                GUI.backgroundColor = Color.white;
                d += 25;
            }
            max = d;

            c += 25;
        }
        GUI.backgroundColor = Color.white;

        if (GUI.Button(new Rect(r.x + max, r.y, 20, 20), "+"))
        {
            for (int a = 0; a < arrayProp.arraySize; a++)
            {
                SerializedProperty lineProp = arrayProp.GetArrayElementAtIndex(a).FindPropertyRelative("line");

                lineProp.InsertArrayElementAtIndex(lineProp.arraySize);
            }
        }


        if (GUI.Button(new Rect(r.x, r.y + c, 20, 20), "+"))
        {
            arrayProp.InsertArrayElementAtIndex(arrayProp.arraySize);
        }


        //  base.OnGUI(position, property, label);
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        // return 15f * 8;
        SerializedProperty arrayProp = property.FindPropertyRelative("inv");
        return (arrayProp.arraySize + 2) * 25f;
    }

}
