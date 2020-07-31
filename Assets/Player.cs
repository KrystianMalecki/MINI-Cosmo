using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float Speed;
    public float RotationSpeed;
    public Rigidbody2D r2d;
    public void RotateGunBarrel()
    {
       Vector2 mousepos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z));
       transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, Mathf.Atan2((mousepos.y - transform.position.y), (mousepos.x - transform.position.x)) * Mathf.Rad2Deg - 90), Time.deltaTime * RotationSpeed);
    }
    void Update()
    {
        RotateGunBarrel();
        if (Input.GetKey(KeyCode.W))
        {
            r2d.AddForce(transform.up * Speed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            r2d.AddForce(-transform.up * Speed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            r2d.AddForce(-transform.right * Speed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            r2d.AddForce(transform.right * Speed);
        }
    }
}
