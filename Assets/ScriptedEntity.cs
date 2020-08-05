using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScriptedEntity : Entity
{
    public float Speed = 5;
    public float RotationSpeed = 1;
    [Header("Energy")]
    public float MaxEnergy = 20;
    public float Energy;
    public float ERechargeWaitTime = 1;
    [Tooltip("times 10 per second")]
    public float ERecharge = 0.1f;

    public Text energytxt;
    public Image energybar;
    public override void Start()
    {
        base.Start();
        Energy = MaxEnergy;
        AddEnergy(0);
    }
    public void AddEnergy(float number)
    {
        Energy += number;
        if (energytxt != null)
        {
            energytxt.text = "Energy: " + Energy.ToString("0") + "/" + MaxEnergy.ToString("0");
            if (energybar != null)
            {
                energybar.fillAmount = Energy / MaxEnergy;
            }
        }
        if (number < 0)
        {
            StopCoroutine("EnergycountDown");
            StartCoroutine("EnergycountDown");
        }
    }
    IEnumerator EnergycountDown()
    {

        yield return new WaitForSeconds(ERechargeWaitTime);
        while (Energy + ERecharge <= MaxEnergy)
        {
            AddEnergy(ERecharge);
            yield return new WaitForSeconds(0.1f);

        }

    }
    public void LookAt(Vector2 v2)
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, Mathf.Atan2((v2.y - transform.position.y), (v2.x - transform.position.x)) * Mathf.Rad2Deg - 90), Time.deltaTime * RotationSpeed);
    }
}
