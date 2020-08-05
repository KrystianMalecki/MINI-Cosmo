using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbiter : MonoBehaviour
{
    public Transform target;
    public float speed=20;
    public Vector3 axis;
    void Update()
    {
        transform.RotateAround(target.transform.position, axis.normalized, speed * Time.deltaTime);
        transform.rotation = Quaternion.identity;

    }
}
