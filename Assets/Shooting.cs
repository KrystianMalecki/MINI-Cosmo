using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;

[Serializable]
public class WeaponEq
{
    public BulletData Data;
    public Transform ShootPoint;
    public KeyCode FireButton = KeyCode.Keypad0;
    public float FireRateTimer;
}
public class Shooting : MonoBehaviour
{
    public bool IsPlayer;
    [SerializeField]
    public List<WeaponEq> Weapons;
    private GameObject BulletBase;
    public Rigidbody2D ShipR2D;
    public bool HasAutoTargeting;
    public GameObject Target = null;
    public void Start()
    {
        BulletBase = StaticDataManager.instance.BulletBase;
    }
    public void Shoot(int WeaponID)
    {



        if (Weapons[WeaponID].FireRateTimer <= 0)
        {
            ShipR2D.AddForce(-Weapons[WeaponID].ShootPoint.transform.up * Weapons[WeaponID].Data.Recoil);
            for (int i = 0; i < Weapons[WeaponID].Data.BulletCount; i++)
            {


                GameObject bullet = Instantiate(BulletBase, Weapons[WeaponID].ShootPoint.position, Weapons[WeaponID].ShootPoint.rotation);
                bullet.transform.Rotate(new Vector3(0f, 0f, UnityEngine.Random.Range(-Weapons[WeaponID].Data.Inaccuracity, Weapons[WeaponID].Data.Inaccuracity)));
                Bullet bulletCode = bullet.GetComponent<Bullet>();
                bulletCode.CanAttackPlayer = !IsPlayer;

                bulletCode.Data = Weapons[WeaponID].Data;
                bulletCode.Create();
                if (Weapons[WeaponID].Data.IsHoming)
                {
                    Target = GetTarget(10);
                    if (Target != null)
                    {
                        bulletCode.Target = Target;
                    }
                }


            }

            Weapons[WeaponID].FireRateTimer = 1f / Weapons[WeaponID].Data.FireRate;

        }

    }
    public void CoolDownCount(int id)
    {
        if (Weapons[id].FireRateTimer > 0f)
        {

            Weapons[id].FireRateTimer -= Time.deltaTime;


        }
    }
    public GameObject GetTarget(float radius)
    {
        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, radius);
        if (targets.Length > 0)
        {
            int distPos = 0;
            float dist = -1;

            for (int a = 0; a < targets.Length; a++)
            {
                if (Vector2.Distance(targets[a].transform.position, transform.position) <= dist)
                {
                    dist = Vector2.Distance(targets[a].transform.position, transform.position);
                    distPos = a;
                }
            }
            return targets[distPos].gameObject;
        }
        return null;
    }
    public void Update()
    {
        for (int i = 0; i < Weapons.Count; i++)
        {



            CoolDownCount(i);


        }
        if (IsPlayer)
        {
            for (int i = 0; i < Weapons.Count; i++)
            {

                if (Input.GetKey(Weapons[i].FireButton))
                {

                    Shoot(i);

                }
            }
        }
    }
    public void OnDrawGizmosSelected()
    {
#if UNITY_EDITOR


        Handles.color = Color.blue;

        Handles.DrawWireDisc(gameObject.transform.position, new Vector3(0, 0, 180), 10);


#endif
    }
}
