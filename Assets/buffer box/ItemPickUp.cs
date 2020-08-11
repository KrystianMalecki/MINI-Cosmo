using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public ItemData item;
    public GameObject go;
    public Rigidbody2D r2d;
    public ICollector icoll;
    public SpriteRenderer sr;
    public void Setup(ItemData iD)
    {
        item = iD;
        sr.sprite = Inventory.instance.GetItemInfo(iD.id).texture;
    }
    public void OnTriggerEnter2D(Collider2D col)
    {
        if(!col.isTrigger)
        {


            if (StaticDataManager.instance.isOkTarget(false, col.tag))
            {

                if (col.gameObject.GetComponent<ICollector>() != null)
                {

                    go = col.gameObject;
                    icoll = col.gameObject.GetComponent<ICollector>();
                }
            }
        }
    }
    public void LookAt(Vector2 v2)
    {
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2((v2.y - transform.position.y), (v2.x - transform.position.x)) * Mathf.Rad2Deg - 90);
    }
    public void Update()
    {
        if (go != null)
        {
          //  LookAt(go.transform.position);
            r2d.AddRelativeForce((go.transform.position- transform.position).normalized * icoll.get_collection_speed());
           // transform.rotation = Quaternion.identity;
            if (Vector2.Distance(transform.position, go.transform.position) < 2)
            {
                if (icoll.Collect(item))
                {
                    Destroy(gameObject);

                }
            }
        }
    }   
}
