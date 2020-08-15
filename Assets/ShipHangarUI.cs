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
        infobox.SetActive(false);
    }
    public void Clicked(int id)
    {
        infobox.SetActive(true);
        img.sprite = hangar.ships[id].BasedOn.SGD.icon;
        namer.text = StaticDataManager.instance.TMProFormater + hangar.ships[id].name;
        hp.text = StaticDataManager.instance.TMProFormater +"HP:"+ hangar.ships[id].HP.ToString("0")+"/"+ hangar.ships[id].maxHP.ToString("0");
        energy.text = StaticDataManager.instance.TMProFormater + "Energy:" + hangar.ships[id].energy.ToString("0") + "/" + hangar.ships[id].maxEnergy.ToString("0");
        speed.text = StaticDataManager.instance.TMProFormater + "Speed: [ZMIEŃ TO]";
        space.text = StaticDataManager.instance.TMProFormater + "Space: [ZMIEŃ TO]";

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
