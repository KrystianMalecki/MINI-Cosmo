using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScriptedEntity : Entity
{
    public ShipData data;


    public TextMeshProUGUI energytxt;
    public Image energybar;
    public override void Start()
    {
        data.HP = data.maxHP;
        Damage(0);
        base.Start();
        data.energy = data.maxEnergy;
        AddEnergy(0);

    }
    public override void Damage(float value)
    {

        data.HP -= value;
        if (hpbar != null)
        {
            hpbar.fillAmount = data.HP / data.maxHP;
        }
        if (data.HP <= 0)
        {
            Die();
        }


    }
    public void AddEnergy(float number)
    {
        data.energy += number;
        if (data.energy > data.maxEnergy)
        {
            StopCoroutine("EnergyOverload");

            StartCoroutine("EnergyOverload");
        }
        if (energytxt != null)
        {
            energytxt.text = StaticDataManager.instance.TMProFormater + "Energy: " + data.energy.ToString("0") + "/" + data.maxEnergy.ToString("0");
            if (energybar != null)
            {
                energybar.fillAmount = data.energy / data.maxEnergy;
            }
        }
        if (number < 0)
        {
            StopCoroutine("EnergycountDown");
            StartCoroutine("EnergycountDown");
        }
    }
    IEnumerator EnergyOverload()
    {

        StopCoroutine("EnergycountDown");

        while (data.energy > data.maxEnergy)
        {
            if (data.energy - 0.1f < data.maxEnergy)
            {
                data.energy = data.maxEnergy;
                AddEnergy(0);

            }
            else
            {
                AddEnergy(-0.1f);
            }

            yield return new WaitForSeconds(0.1f);

        }


    }
    IEnumerator EnergycountDown()
    {
        StopCoroutine("EnergyOverload");

        yield return new WaitForSeconds(data.ERechargeWait);
        while (data.energy <= data.maxEnergy)
        {
            AddEnergy(data.ERecharge);
            yield return new WaitForSeconds(0.1f);

        }

    }
    public void LookAt(Vector2 v2)
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, Mathf.Atan2((v2.y - transform.position.y), (v2.x - transform.position.x)) * Mathf.Rad2Deg - 90), Time.deltaTime * data.rotationSpeed);
    }
}
