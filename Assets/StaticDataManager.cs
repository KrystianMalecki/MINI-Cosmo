using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticDataManager : MonoBehaviour
{
    public GameObject DieExplosion;
    public GameObject BulletBase;

    public static StaticDataManager instance;
    public void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);

        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
