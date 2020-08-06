using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Player : ScriptedEntity
{
    public bool RelativeMovement;
    public Text hptxt;

    public override void Damage(float value)
    {
        base.Damage(value);
        if (hptxt != null)
        {
            hptxt.text = "HP: " + HP.ToString("0") + "/" + MaxHP.ToString("0");
        }
    }
    public void LateUpdate()
    {
        Vector2 newPosition = new Vector2(transform.position.x, transform.position.y);
        float nextX = Mathf.Round(32 * newPosition.x);
        float nextY = Mathf.Round(32 * newPosition.y);
        Camera.main.transform.position = new Vector3(nextX / 32, nextY / 32, -100);
    }
    public void Update()
    {
        FollowMouse();

        if (Input.GetKey(KeyCode.W))
        {
            move(Vector3.up * Speed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            move(-Vector3.up * Speed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            move(-Vector3.right * Speed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            move(Vector3.right * Speed);
        }
    }
    public void FollowMouse()
    {
        Vector2 mousepos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z));
        LookAt(mousepos);
    }
    public void move(Vector3 v3)
    {
        if (!RelativeMovement)
        {
            r2d.AddForce(v3 * Speed);
        }
        else
        {
            r2d.AddRelativeForce(v3 * Speed);

        }
    }
   
}
