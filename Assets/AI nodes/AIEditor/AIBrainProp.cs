using UnityEditor;
using UnityEngine;
[CustomPropertyDrawer(typeof(AIBrain))]

public class AIBrainProp : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {

        EditorGUI.PropertyField(position, property, label, true);
        if (property != null)
        {
            if (property.objectReferenceValue != null)
            {
                if (GUI.Button(new Rect(position.x, position.y + 20, position.width, 20), "Edit"))
                {
                    AIEditor.OpenWindow((AIBrain)property.objectReferenceValue);
                }
            }
        }
    }
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        if (property != null)
        {
            if (property.objectReferenceValue != null)
            {
                return base.GetPropertyHeight(property, label)+20;

            }
        }
                return base.GetPropertyHeight(property, label);
    }
}

