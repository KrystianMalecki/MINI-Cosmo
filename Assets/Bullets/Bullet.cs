using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
public class Bullet : MonoBehaviour
{
    public bool OtherData;
    public bool CanAttackPlayer;
    public int PierceValue;
    [Header("Bullet Parameters")]
    public BulletData Data;
    [Header("Homing")]
    public GameObject Target;
    [Header("Bullet Settings")]
    public Rigidbody2D r2d;
   // public GameObject ShootPoint;
    public SpriteRenderer SR;
    public void Create()
    {
        
        if (Data.AdditionalEffect != null)
        {
            GameObject g = Instantiate(Data.AdditionalEffect, transform);
        }
        PierceValue = 0;
        SR.sprite = Data.Image;
        Material m = Instantiate(SR.material);
        m.SetColor(Shader.PropertyToID("_color"), Data.color);
        SR.material = m;
        gameObject.transform.localScale = new Vector3(Data.Size, Data.Size, Data.Size);
        //  image.color = Data.color;
        r2d.AddForce(gameObject.transform.up * Data.Speed, ForceMode2D.Impulse);
        Invoke("ActivateDestroy", Data.RangeInSeconds);
        if (Data.IsHoming|| Data.IsAutoTargeting)
        {
            r2d.drag = 1f;
            r2d.angularDrag = 1f;
            StartCoroutine("home");
        }

    }
   
    IEnumerator home()
    {
        while (true)
        {
            if (Data.IsAutoTargeting)
            {
                if (Target != null)
                {

                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, Mathf.Atan2((Target.transform.position.y - transform.position.y), (Target.transform.position.x - transform.position.x)) * Mathf.Rad2Deg - 90), Time.deltaTime * Data.HomingRotation);
                }
            }
            else
            {
                Vector3 v3 = Camera.main.ViewportToWorldPoint(Input.mousePosition);
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, Mathf.Atan2((v3.y - transform.position.y), (v3.x - transform.position.x)) * Mathf.Rad2Deg - 90), Time.deltaTime * Data.HomingRotation);

            }
            r2d.AddForce(gameObject.transform.up * Data.HomingSpeed);
            yield return new WaitForSeconds(0.1f);
        }
    }
    public void OnDestroy()
    {
        StopAllCoroutines();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        //if (Homing)

        if (!collision.isTrigger)
        {
            // if (collision.transform.CompareTag("Enemy"))
            {
                if (StaticDataManager.instance.isOkTarget(!CanAttackPlayer, collision.tag)/*(CanAttackPlayer && collision.transform.CompareTag("Player")) || (!CanAttackPlayer && collision.transform.CompareTag("Enemy"))*/)
                {
                    if (!Data.IsExplosive)
                    {
                        Hit(collision.transform);
                    }
                    else
                    {
                        ActivateDestroy();
                    }
                }
                else
                {
                    //  Hit(collision.transform);
                }
            }
        }

    }

    public void Explode()
    {
        // Debug.Log("explode");
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, Data.ExplosionRange);
        foreach (Collider2D collider in colliders)
        {
            if (!collider.isTrigger)
            {
                //  Debug.Log(collider.name);
                DealDMG(collider.transform);
            }
        }

    }
    public bool DealDMG(Transform transformer)
    {

        Entity e;

        if (transformer.TryGetComponent(out e))
        {
           
            if (StaticDataManager.instance.isOkTarget(!CanAttackPlayer, transformer.tag))
            {
                e.Damage(Data.Damage);
                if (e.r2d != null)
                {
                    e.r2d.AddForce((transform.position - transformer.position).normalized*Data.Knockback);
                }
            }
            return true;
        }
       
        return false;
    }
    public void Hit(Transform transformer)
    {

        if (DealDMG(transformer))
        {
            if (Data.IsPiercing)
            {
                PierceValue++;
                if (PierceValue >= Data.PierceCount)
                {
                    ActivateDestroy();
                }
            }
            else
            {
                ActivateDestroy();
            }
        }
    }
    public void ActivateDestroy()
    {
        if (Data.IsExplosive)
        {
            Explode();

            GameObject go = Instantiate(Data.ExplosionEffect, gameObject.transform.position, Quaternion.identity);
            /* if (go.GetComponent<SpriteRenderer>() != null)
             {
                 SpriteRenderer srr = go.GetComponent<SpriteRenderer>();
                 Material m = Instantiate(srr.material);
                 m.SetColor(Shader.PropertyToID("_color"), Data.Explosioncolor);
                 srr.material = m;

             }
             else if (go.GetComponent<ParticleSystemRenderer>() != null)
             {
                 ParticleSystemRenderer srr = go.GetComponent<ParticleSystemRenderer>();
                 Material m = Instantiate(srr.material);
                 m.SetColor(Shader.PropertyToID("_color"), Data.Explosioncolor);
                 srr.material = m;

             }*/
           // Destroy(go, 3f);
        }


        Destroy(gameObject);
    }
    public float Distance(Transform tr)
    {
        return (tr.position - transform.position).magnitude;
    }
    public void OnDrawGizmosSelected()
    {
#if UNITY_EDITOR

        if (Data.IsExplosive)
        {
            Handles.color = Color.red;

            Handles.DrawWireDisc(gameObject.transform.position, new Vector3(0, 0, 180), Data.ExplosionRange);

        }
#endif
    }




}