using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class Homingv2 : MonoBehaviour
{
    public Collider2D colliderer;
    public List<GameObject> objects;
    public GameObject Target;
    public bool IsPlayer;
    public void OnTriggerEnter2D(Collider2D col)
    {
        if (colliderer.IsTouching(col))
        {
            objects.Add(col.gameObject);
        }
       
    }
    public void OnTriggerExit2D(Collider2D col)
    {
        if (!colliderer.IsTouching(col))
        {
            objects.Remove(col.gameObject);
        }

    }
    public void Start()
    {
        StartCoroutine(HomeTick());
    }
    IEnumerator HomeTick()
    {
        while (true)
        {
            if (objects.Count > 0)
            {
                int distPos = -1;
                float dist = 100000;

                for (int a = 0; a < objects.Count; a++)
                {
                    if (objects[a] == null)
                    {
                        objects.RemoveAt(a);
                        a--;
                    }
                    if (StaticDataManager.instance.isOkTarget(IsPlayer, objects[a].tag))
                    {

                        if (Vector2.Distance(objects[a].transform.position, transform.position) <= dist)
                        {
                            dist = Vector2.Distance(objects[a].transform.position, transform.position);
                            distPos = a;
                        }
                    }
                }
                if (distPos != -1)
                {
                    Target = objects[distPos].gameObject;
                }
                else
                {
                    Target = null;
                }
            }
            else
            {
                Target = null;
            }
            yield return new WaitForSeconds(1f);
        }
    }
}
