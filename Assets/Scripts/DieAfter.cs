using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieAfter : MonoBehaviour
{
    public float time;
    public void OnEnable()
    {
        StartCoroutine("waiter_and_dier");
    }
    IEnumerator waiter_and_dier()
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
    public void OnDestroy()
    {
        StopAllCoroutines();

    }
}
