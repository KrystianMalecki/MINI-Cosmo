using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checker : MonoBehaviour
{
    public Collider2D c1;
    [SerializeField]
    public Collider2D c2;
    public bool isc1;
    public void OnTriggerEnter2D(Collider2D col)
    {
        if (c1.IsTouching(col)&& isc1)
        {
            Debug.LogError("c1" + transform.name);
        }
        if (c2.IsTouching(col)&&!isc1)
        {
           Debug.LogError("c2"+transform.name);
        }
        Debug.Log("tick" + col.name);
    }
}
