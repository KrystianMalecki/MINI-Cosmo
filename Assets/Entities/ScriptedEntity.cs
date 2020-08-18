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
        data.HP = data.stats.maxHP;
        Damage(0);
        base.Start();
        data.energy = data.stats.maxEnergy;
        AddEnergy(0);

    }
    public override void Damage(float value)
    {

        data.HP -= value;
        if (hpbar != null)
        {
            hpbar.fillAmount = data.HP / data.stats.maxHP;
        }
        if (data.HP <= 0)
        {
            Die();
        }


    }
    public void AddEnergy(float number)
    {
        data.energy += number;
        if (data.energy > data.stats.maxEnergy)
        {
            StopCoroutine("EnergyOverload");

            StartCoroutine("EnergyOverload");
        }
        if (energytxt != null)
        {
            energytxt.text = StaticDataManager.instance.TMProFormater + "Energy: " + data.energy.ToString("0") + "/" + data.stats.maxEnergy.ToString("0");
            if (energybar != null)
            {
                energybar.fillAmount = data.energy / data.stats.maxEnergy;
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

        while (data.energy > data.stats.maxEnergy)
        {
            if (data.energy - 0.1f < data.stats.maxEnergy)
            {
                data.energy = data.stats.maxEnergy;
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

        yield return new WaitForSeconds(data.stats.ERechargeWait);
        while (data.energy <= data.stats.maxEnergy)
        {
            AddEnergy(data.stats.ERecharge);
            yield return new WaitForSeconds(0.1f);

        }

    }
    public void LookAt(Vector2 v2)
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, Mathf.Atan2((v2.y - transform.position.y), (v2.x - transform.position.x)) * Mathf.Rad2Deg - 90), Time.deltaTime * data.stats.rotationSpeed);
    }
}
