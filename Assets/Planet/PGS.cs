using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Attributes;
using System;

public enum ParticleShapeType { Ring, Sphere }
public class PGS : MonoBehaviour
{
#if UNITY_EDITOR
    private List<Color> colos = new List<Color> { Color.red, Color.yellow, Color.green, Color.cyan, Color.blue, Color.magenta };
#endif
    public PlanetGraphicsData PGD;
    public GameObject InfoCross;
    public SpriteRenderer PlanetTextureRenderer;
    public List<GameObject> ParticlesMade = new List<GameObject>();
    public List<GameObject> ObitersMade = new List<GameObject>();

    public void SetupData()
    {
        if (ParticlesMade.Count > 0)
        {
            for (int a = 0; a < ParticlesMade.Count; a++)
            {
                Destroy(ParticlesMade[a]);
                ParticlesMade.RemoveAt(a);
                a--;
            }
        }
        if (ObitersMade.Count > 0)
        {
            for (int a = 0; a < ObitersMade.Count; a++)
            {
                Destroy(ObitersMade[a]);
                ObitersMade.RemoveAt(a);
                a--;
            }
        }
        transform.localScale = new Vector3(PGD.size, PGD.size, PGD.size);
        InfoCross.transform.localScale = new Vector3(Mathf.CeilToInt(PGD.size), Mathf.CeilToInt(PGD.size), Mathf.CeilToInt(PGD.size));
        //Material
        Material mat = Instantiate(PlanetTextureRenderer.material);
        mat.SetColor("_color1", PGD.color1);
        mat.SetColor("_color2", PGD.color2);
        mat.SetColor("_color3", PGD.color3);
        mat.SetFloat("_pix_num", PGD.size * 32);
        mat.SetVector("_start_offset", new Vector4(UnityEngine.Random.Range(-1000.0f, 1000.0f), UnityEngine.Random.Range(-1000.0f, 1000.0f)));
        mat.SetVector("_rotation_speed", PGD.rotation_speed);
        mat.SetFloat("_high", PGD.high_add);
        mat.SetVector("_noises", PGD.noise_layers_scales);
        PlanetTextureRenderer.material = mat;
        //Particles
        foreach (ParticlePGD particledata in PGD.particle_layers)
        {
            GameObject go = Instantiate(StaticDataManager.instance.ParticleBase, transform);
            ParticlesMade.Add(go);
            go.transform.rotation = Quaternion.Euler(
               particledata.ring_rotation);
            ParticleSystem ps = go.GetComponent<ParticleSystem>();
            ps.Stop();
            foreach (Sprite spr in particledata.random_sprites)
            {
                ps.textureSheetAnimation.AddSprite(spr);
            }
            var main = ps.main;
            var tsa = ps.textureSheetAnimation;
            var sf = tsa.startFrame;
            sf.mode = ParticleSystemCurveMode.TwoConstants;
            sf.constantMin = 0;
            // Debug.LogError(particledata.random_sprites.Count);
            sf.constantMax = Mathf.CeilToInt(particledata.random_sprites.Count);
            tsa.startFrame = sf;
            var ss = main.startSize;
            ss.constantMin = particledata.size_range.x;
            ss.constantMax = particledata.size_range.y;
            main.startColor = particledata.start_color;
            main.startSize = ss;
            var em = ps.emission;
            var emissionRate = em.rateOverTime;
            emissionRate.constantMin = particledata.rate_range.x;
            emissionRate.constantMax = particledata.rate_range.y;
            em.rateOverTime = emissionRate;
            var shape = ps.shape;
            var volt = ps.velocityOverLifetime;
            if (particledata.PStype == ParticleShapeType.Ring)
            {
                shape.shapeType = ParticleSystemShapeType.Donut;
                volt.orbitalZ = 1;

            }
            else if (particledata.PStype == ParticleShapeType.Sphere)
            {
                shape.shapeType = ParticleSystemShapeType.Sphere;
                volt.orbitalZ = particledata.rotation_direction.z;
                volt.orbitalX = particledata.rotation_direction.x;
                volt.orbitalY = particledata.rotation_direction.y;

            }

            var sm = volt.speedModifier;
            sm.constantMin = particledata.rotation_speed_range.x;
            sm.constantMax = particledata.rotation_speed_range.y;
            volt.speedModifier = sm;
            shape.radius = particledata.radius;
            shape.radiusThickness = particledata.radius_thickness;
            shape.scale = particledata.scale;

            var colt = ps.colorOverLifetime;
            colt.color = particledata.color_OLT;

            var solt = ps.sizeOverLifetime;
            solt.size = new ParticleSystem.MinMaxCurve(1, particledata.size_OLT_curve);
            ps.Play();
        }
        foreach (OrbiterPGD orbdata in PGD.orbiters)
        {
            GameObject orbiterer = Instantiate(StaticDataManager.instance.OrbiterBase, transform.position, Quaternion.identity);
            orbiterer.transform.Translate(new Vector3( orbdata.axis.normalized.z, orbdata.axis.normalized.x, orbdata.axis.normalized.y) * orbdata.distance);
            orbiterer.transform.parent = gameObject.transform;
            orbiterer.transform.localScale = orbdata.size;

            Orbiter orbiterscr = orbiterer.GetComponent<Orbiter>();
            orbiterscr.target = gameObject.transform;
            orbiterscr.axis = orbdata.axis;
            orbiterscr.smallerer = true;
            orbiterscr.speed = orbdata.speed;
            SpriteRenderer sr = orbiterer.GetComponent<SpriteRenderer>();
            sr.sprite = orbdata.sprite;
           Material m = Instantiate(sr.material);
            m.SetColor("_color", orbdata.color);
            sr.material = m;
            orbiterscr.setdis();
            ObitersMade.Add(orbiterer);
        }
    }
    public void OnDrawGizmos()
    {
#if UNITY_EDITOR
        int a = 0;
        foreach (OrbiterPGD orb in PGD.orbiters)
        {
            Handles.color = colos[a%colos.Count];

            Handles.DrawWireDisc(transform.position, orb.axis.normalized, orb.distance);
            a++;
        }

#endif
    }
}
[System.Serializable]
public class PlanetGraphicsData
{
    [Header("Main Info")]
    [Range(0.1f, 20)]
    public float size;
    public Vector3 rotation_speed;
    [Range(0f, 1f)]
    public float high_add;
    [Header("-1 to disable layer")]

    public Vector4 noise_layers_scales;
    [Header("Colors")]
    [ColorUsage(true, true)]
    public Color color1;
    [ColorUsage(true, true)]
    public Color color2;
    [ColorUsage(true, true)]
    public Color color3;
    [Header("Particles")]
    public List<ParticlePGD> particle_layers = new List<ParticlePGD>();
    [Header("Orbiters")]
    public List<OrbiterPGD> orbiters = new List<OrbiterPGD>();

}
[System.Serializable]
public class ParticlePGD
{
    [Header("name is purely for a visual in inspector")]
    public string name;
    public List<Sprite> random_sprites;
    public Vector2 size_range;
    public Gradient start_color;
    public Vector2 rate_range;
    [Header("Shape")]
    public ParticleShapeType PStype;
    public float radius;
    [Range(0, 1f)]
    public float radius_thickness;

    public Vector3 scale;
    [ConditionalField("PStype", ParticleShapeType.Ring)] public Vector3 ring_rotation = new Vector3(0, 0,0);

    [ConditionalField("PStype", ParticleShapeType.Sphere)] public Vector3 rotation_direction;
    public Vector2 rotation_speed_range;
    [Header("Over Lifetime")]
    public AnimationCurve size_OLT_curve;
    public Gradient color_OLT;
}
[System.Serializable]
public class OrbiterPGD
{
    [Header("name is purely for a visual in inspector")]
    public string name;
    public Sprite sprite;
    public Vector2 size;
    [ColorUsage(true, true)]
    public Color color;
    public float speed;
    public Vector3 axis;
    public float distance;
}
#if UNITY_EDITOR

[CustomEditor(typeof(PGS))]
public class EditorPGS : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        PGS pgs = (PGS)target;
        if (GUILayout.Button("Setup"))
        {
            pgs.SetupData();
        }
    }


}
#endif
