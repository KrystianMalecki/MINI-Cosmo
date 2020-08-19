using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Player : ScriptedEntity, ICollector
{
    public bool RelativeMovement;
    public TextMeshProUGUI hptxt;
    public float collect_speed=10;
    [HideInInspector]
    public List<Transform> shootpoints = new List<Transform>();
    public Shooting shooting;
    public override void Damage(float value)
    {
        base.Damage(value);
        if (hptxt != null)
        {
            hptxt.text = StaticDataManager.instance.TMProFormater + "HP: " + data.HP.ToString("0") + "/" + data.stats.maxHP.ToString("0");
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
            move(Vector3.up );
        }
        if (Input.GetKey(KeyCode.S))
        {
            move(-Vector3.up );
        }
        if (Input.GetKey(KeyCode.A))
        {
            move(-Vector3.right );
        }
        if (Input.GetKey(KeyCode.D))
        {
            move(Vector3.right );
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
            r2d.AddForce(v3 * data.stats.speed);
        }
        else
        {
            r2d.AddRelativeForce(v3 * data.stats.speed);

        }
    }

    public float get_collection_speed()
    {
        return collect_speed;
    }

    public bool Collect(ItemData it)
    {
     return Inventory.instance.AddItem(it.id, it.count);
        
    }
}
public interface ICollector
{
     float get_collection_speed();
     bool Collect(ItemData it);

}
