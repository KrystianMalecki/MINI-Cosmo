using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
public class Entity : MonoBehaviour
{
    public float HP;
    public float MaxHP;
    private GameObject Explosion;
    public Rigidbody2D r2d;
    public Image hpbar;
    public virtual void Start()
    {
        HP = MaxHP;
        Damage(0);
    }
    public virtual void Damage(float value)
    {
        HP -= value;
        if (hpbar != null)
        {
            hpbar.fillAmount = HP / MaxHP;
        }
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
