using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

[CustomPropertyDrawer(typeof(ShipInventoryProp))]
public class ShipInventoryEditor : PropertyDrawer
{
    private bool showInfo;
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // base.OnGUI(position, property, label);
        //EditorGUI.PrefixLabel(position, label);
        //   public enum TileType { Null, Power, Weapon, Engine, All, Defense, UI_SLOT_BOX, UI_TRASH }

        SerializedProperty arrayProp = property.FindPropertyRelative("inv");
        Rect r = new Rect(position);

        showInfo = GUI.Toggle(new Rect(r.x, r.y, 200, 20), showInfo, "Show Help");
        r.y += 25;
        if (showInfo)
        {
            GUI.backgroundColor = StaticDataManager.TileToColor(TileType.Null);

            GUI.Button(new Rect(r.x, r.y, 200, 20), "0 Null");
            r.y += 25;
            GUI.backgroundColor = StaticDataManager.TileToColor(TileType.Power);

            GUI.Button(new Rect(r.x, r.y, 200, 20), "1 Power");
            r.y += 25;
            GUI.backgroundColor = StaticDataManager.TileToColor(TileType.Weapon);

            GUI.Button(new Rect(r.x, r.y, 200, 20), "2 Weapon");
            r.y += 25;
            GUI.backgroundColor = StaticDataManager.TileToColor(TileType.Engine);

            GUI.Button(new Rect(r.x, r.y, 200, 20), "3 Engine");
            r.y += 25;
            GUI.backgroundColor = StaticDataManager.TileToColor(TileType.All);

            GUI.Button(new Rect(r.x, r.y, 200, 20), "4 All");
            r.y += 25;
            GUI.backgroundColor = StaticDataManager.TileToColor(TileType.Defense);

            GUI.Button(new Rect(r.x, r.y, 200, 20), "5 Defense");
            r.y += 25;
          
           
          
           
        }
        GUI.backgroundColor = Color.white;

        int max = 0;
        

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
        GUI.backgroundColor = Color.white;

        int c = 0;
        for (int a = 0; a < arrayProp.arraySize; a++)
        {
            SerializedProperty lineProp = arrayProp.GetArrayElementAtIndex(a).FindPropertyRelative("line");
            // Debug.Log(lineProp.arraySize);

            int d = 0;
            for (int b = 0; b < lineProp.arraySize; b++)
            {
                SerializedProperty typeProp = lineProp.GetArrayElementAtIndex(b).FindPropertyRelative("type");
                //  EditorGUI.PropertyField(new Rect(d,  c,  10, 10), lineProp.GetArrayElementAtIndex(b), GUIContent.none);
                GUI.backgroundColor = StaticDataManager.TileToColor((TileType)typeProp.enumValueIndex);
                if (GUI.Button(new Rect(r.x + d, r.y + c, 20, 20), typeProp.enumValueIndex.ToString("0")))
                {
                    if (typeProp.enumValueIndex + 1 > 5)
                    {
                        typeProp.enumValueIndex = 0;
                    }
                    else
                    {
                        typeProp.enumValueIndex++;

                    }
                }
                GUI.backgroundColor = Color.white;
                d += 25;
            }
            max = d;

            c += 25;
        }
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
        float a = 25;
        if (showInfo)
        {
            a += 6 * 25f;
        }
        SerializedProperty arrayProp = property.FindPropertyRelative("inv");
        return (arrayProp.arraySize + 2) * 25f+a;
    }

}
