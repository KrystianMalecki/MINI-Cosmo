using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponUIManager : MonoBehaviour
{
    public List<WeaponBox> boxes = new List<WeaponBox>();
    public GameObject WBbase;
    public Transform mainTransform;
    public Shooting s;
    public List<WeaponItem> Wi = new List<WeaponItem>();
    public Sprite err;
    public float time=100;
    public AnimationCurve g;
    public AnimationCurve r;

    public void Start()
    {
        time = 100000;
        displayItems();
        Debug.Log(time);
        StartCoroutine("tick");

    }
    IEnumerator tick()
    {
        while (s != null)
        {
            updateBars();
            yield return new WaitForSeconds(time);
        }
    }
    public Color c(float f)
    {
        Color cc = Color.black;
    
        cc.r = r.Evaluate(f);
        cc.g = g.Evaluate(f);

        return cc;
    }
    public void displayItems()
    {

        int offset = 0;
        for (int a = 0; a < boxes.Count; a++)
        {
            if (s.Weapons.Count > a)
            {
                if(((1/s.Weapons[a].Data.FireRate)/16)  < time)
                {
                    time = ((1/s.Weapons[a].Data.FireRate) / 16);
                }
                if (Wi.Count > a)
                {
                    boxes[a].Setup(Wi[a].texture);


                }
                else
                {
                    boxes[a].Setup(err);

                }
                offset++;
            }
            else
            {
                boxes[a].Hide();
            }
            boxes[a].bar.color = c((1f - s.Weapons[a].Data.FireRate * s.Weapons[a].FireRateTimer));

        }

        for (int a = offset; a < s.Weapons.Count; a++)
        {
            GameObject go = Instantiate(WBbase, mainTransform);
            WeaponBox wb = go.GetComponent<WeaponBox>();
            if (Wi.Count > a)
            {
                wb.Setup(Wi[a].texture);


            }
            else
            {
                wb.Setup(err);

            }
            if ((1 / s.Weapons[a].Data.FireRate) / 16 < time)
            {
                time =  (1 / s.Weapons[a].Data.FireRate) / 16;
            }
            boxes.Add(wb);
            boxes[a].bar.color = c((1f - s.Weapons[a].Data.FireRate * s.Weapons[a].FireRateTimer));

        }
    }
    public void updateBars()
    {

        for (int a = 0; a < boxes.Count; a++)
        {
            if ( s.Weapons.Count > a)
            {
               // Debug.Log(1f - s.Weapons[a].Data.FireRate * s.Weapons[a].FireRateTimer);
                boxes[a].bar.fillAmount = (1f- s.Weapons[a].Data.FireRate* s.Weapons[a].FireRateTimer);
              
                boxes[a].bar.color=c((1f - s.Weapons[a].Data.FireRate * s.Weapons[a].FireRateTimer));
            }

        }

    }
   
}
