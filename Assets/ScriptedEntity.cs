using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptedEntity : Entity
{
    public float Speed;
    public float RotationSpeed;
    public Rigidbody2D r2d;
    public void LookAt(Vector2 v2)
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, Mathf.Atan2((v2.y - transform.position.y), (v2.x - transform.position.x)) * Mathf.Rad2Deg - 90), Time.deltaTime * RotationSpeed);
    }
}
