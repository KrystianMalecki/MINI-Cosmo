using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public float HP;
    public float MaxHP;
    private GameObject Explosion;
    public void Start()
    {
        Die();
    }
    public virtual void Damage(float value, Transform t)
    {
        HP -= value;
        if (HP <= 0)
        {
            Die();
        }

    }

    public void Die()
    {
        Explosion = StaticDataManager.instance.DieExplosion;
        GameObject go = Instantiate(Explosion, transform.position, transform.rotation);
        Destroy(go, 21f);
       Destroy(gameObject,0.01f);
    }

}
