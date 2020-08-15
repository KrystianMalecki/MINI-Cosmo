using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShipHangar : MonoBehaviour
{
    public static ShipHangar instance;
    public List<ShipData> ships = new List<ShipData>();
    [Header("Bases")]
    public GameObject playerShipBase;
    public GameObject spriteLayerBase;
    public GameObject trailLayerBase;
    public GameObject shootPointBase;
    

    public void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);

        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

    }
    public GameObject makePlayerShip(ShipData data, Vector3 position)
    {
        GameObject go = Instantiate(playerShipBase, position, Quaternion.identity);
        for(int a = 0; a < data.BasedOn.SGD.layers.Count; a++)
        {
            GameObject spr = Instantiate(spriteLayerBase, go.transform);
            SpriteRenderer sr =spr.GetComponent<SpriteRenderer>();
            sr.sortingOrder = (a+1);
            sr.material = Instantiate(sr.material);

            sr.sprite = data.BasedOn.SGD.layers[a].texture;
            sr.material.SetColor("_color", data.BasedOn.SGD.layers[a].color);
        }
        Shooting s = go.GetComponent<Shooting>();
        s.Weapons.Clear();
        for (int a = 0; a < data.BasedOn.SGD.trailPositions.Count; a++)
        {
            GameObject spr = Instantiate(trailLayerBase, go.transform);
            TrailRenderer tr = spr.GetComponent<TrailRenderer>();
           tr.transform.localPosition = data.BasedOn.SGD.trailPositions[a].offset;
            tr.material = Instantiate(tr.material);
            tr.material.SetColor("_color", data.BasedOn.SGD.trailPositions[a].color);
        }
        for (int a = 0; a < data.BasedOn.SGD.shootPoints.Count; a++)
        {
            GameObject sp = Instantiate(shootPointBase, go.transform);
            sp.transform.localPosition =new Vector3( data.BasedOn.SGD.shootPoints[a].x, data.BasedOn.SGD.shootPoints[a].y, data.BasedOn.SGD.shootPoints[a].z);
            sp.transform.rotation = Quaternion.Euler(new Vector3(0, 0, data.BasedOn.SGD.shootPoints[a].w));
            s.Weapons.Add(new WeaponEq(null,sp.transform));
        }

        Player p = go.GetComponent<Player>();
        p.data = data;
        p.hpbar = StaticDataManager.instance.hpbar;
        p.hptxt = StaticDataManager.instance.hptxt;
        p.energybar = StaticDataManager.instance.energybar;
        p.energytxt = StaticDataManager.instance.energytxt;
        p.Start();

        return go;
    }
    public void Start()
    {
       // ships[0].ResetToBase();
        makePlayerShip(ships[0], new Vector3(0, 0, 0));
    }
}
