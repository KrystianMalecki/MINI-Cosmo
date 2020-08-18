using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class ShipHangarUI : UIBase
{
    public GameObject ShipBoxBase;

    public ShipHangar hangar;
    public ShipData selectedData;
    public ShipBuilderUI sbui;
    public List<ShipBox> boxes = new List<ShipBox>();
    public TextMeshProUGUI namer;
    public TextMeshProUGUI hp;
    public TextMeshProUGUI energy;
    public TextMeshProUGUI speed;
    public TextMeshProUGUI space;
    public GameObject infobox;
    public Transform transformer;
    public Image img;
    public override void OpenThis()
    {
        base.OpenThis();
        displayShips();
        if (selectedData.BasedOn == null)
        {


            infobox.SetActive(false);
        }
        else
        {
            infobox.SetActive(true);

            ShowSelected();
        }
    }
    public void Clicked(int id)
    {
        infobox.SetActive(true);

        selectedData = hangar.ships[id];
        ShowSelected();
    }
    public void ShowSelected()
    {
        infobox.SetActive(true);

        img.sprite = selectedData.BasedOn.SGD.icon;
        namer.text = StaticDataManager.instance.TMProFormater + selectedData.name;
        hp.text = StaticDataManager.instance.TMProFormater + "HP:" + selectedData.HP.ToString("0") + "/" + selectedData.stats.maxHP.ToString("0");
        energy.text = StaticDataManager.instance.TMProFormater + "Energy:" + selectedData.energy.ToString("0") + "/" + selectedData.stats.maxEnergy.ToString("0");
        speed.text = StaticDataManager.instance.TMProFormater + "Spd:" + selectedData.stats.speed.ToString("0") + " Rot Spd:" + selectedData.stats.rotationSpeed.ToString("0");
        space.text = StaticDataManager.instance.TMProFormater + "Space: [ZMIEŃ TO]";
    }
    public void Edit()
    {
        sbui.selectedShip = selectedData;

    }
    public void displayShips()
    {
        for (int u = 0; u < boxes.Count; u++)
        {
            boxes[u].Hide();
        }
        for (int a = 0; a < hangar.ships.Count; a++)
        {
            if (a < boxes.Count)
            {
                ShipBox sb = boxes[a];
                sb.Setup(a, hangar.ships[a].BasedOn.SGD.icon, hangar.ships[a].name,this);
            }
            else
            {
                GameObject go = Instantiate(ShipBoxBase, transformer.transform);
                ShipBox sb = go.GetComponent<ShipBox>();
                sb.Setup(a, hangar.ships[a].BasedOn.SGD.icon, hangar.ships[a].name,this);

                boxes.Add(sb);
            }

            
        }
    }
}
