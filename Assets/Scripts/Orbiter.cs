using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class Orbiter : MonoBehaviour
{
    public Transform target;
    public float speed = 20;
    public Vector3 axis;
    public bool smallerer;
    public Vector3 start;
    public float dis;
    public void OnEnable()
    {
        start = transform.localScale;
        if (target != null)
        {
            setdis();
        }
    }
    void Update()
    {
        transform.RotateAround(target.transform.position, axis.normalized, speed * Time.deltaTime);
        transform.rotation = Quaternion.identity;
        if (smallerer)
        {
            float num = Mathf.Lerp(0.5f, 1.5f, Mathf.Clamp01(((transform.position.z / -dis) / 2) + 0.5f));
            transform.localScale = new Vector3(num * start.x, num * start.y, num * start.z);
        }

    }
    public void OnDrawGizmosSelected()
    {
#if UNITY_EDITOR


        Handles.color = Color.gray;

        Handles.DrawWireDisc(target.transform.position, axis.normalized, dis);


#endif
    }
    public void setdis()
    {
        dis = Vector3.Distance(target.transform.position, transform.position);

    }
}
#if UNITY_EDITOR
[CustomEditor(typeof(Orbiter))]
public class EditorOrbiter : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        Orbiter script = (Orbiter)target;
        if (GUILayout.Button("set dis"))
        {
            script.setdis();
        }
    }
}
#endif
