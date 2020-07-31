using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : ScriptedEntity
{
    public bool RelativeMovement;
    public void FollowMouse()
    {
        Vector2 mousepos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z));
        LookAt(mousepos);
    }
    public void move(Vector3 v3)
    {
        if (RelativeMovement)
        {
            r2d.AddForce(transform.up * Speed);
        }
        else
        {
            r2d.AddRelativeForce(transform.up * Speed);

        }
    }
    void Update()
    {
        FollowMouse();

        if (Input.GetKey(KeyCode.W))
        {
            move(transform.up * Speed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            move(-transform.up * Speed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            move(-transform.right * Speed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            move(transform.right * Speed);
        }
    }
}
