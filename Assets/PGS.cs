using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Attributes;
using System;

public enum ParticleShapeType { Ring, Sphere }
public class PGS : MonoBehaviour
{
    public PlanetGraphicsData PGD;
    public GameObject InfoCross;
    public SpriteRenderer PlanetTextureRenderer;
    public List<GameObject> ParticlesMade = new List<GameObject>();
    public void SetupData()
    {
        if (ParticlesMade.Count > 0)
        {
            for(int a = 0; a < ParticlesMade.Count; a++)
            {
                Destroy(ParticlesMade[a]);
                ParticlesMade.RemoveAt(a);
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
        mat.SetVector("_rotation_speed", new Vector4(UnityEngine.Random.Range(-PGD.rotation_speed_range, PGD.rotation_speed_range), UnityEngine.Random.Range(-PGD.rotation_speed_range, PGD.rotation_speed_range)));
        mat.SetFloat("_high", PGD.high_add);
        mat.SetVector("_noises", PGD.noise_layers_scales);
        PlanetTextureRenderer.material = mat;
        //Particles
        foreach (ParticlePGD particledata in PGD.particle_layers)
        {
            GameObject go = Instantiate(StaticDataManager.instance.ParticleBase, transform);
            ParticlesMade.Add(go);
            go.transform.rotation = Quaternion.Euler(
                UnityEngine.Random.Range(particledata.ring_rotation_range.x, particledata.ring_rotation_range.y),
                  UnityEngine.Random.Range(particledata.ring_rotation_range.x, particledata.ring_rotation_range.y),
                  UnityEngine.Random.Range(particledata.ring_rotation_range.x, particledata.ring_rotation_range.y));
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
            sf.constantMax = Mathf.CeilToInt( particledata.random_sprites.Count);
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
                volt.orbitalZ = UnityEngine.Random.Range(-particledata.rotation_direction_ranges.z, particledata.rotation_direction_ranges.z);
                volt.orbitalX = UnityEngine.Random.Range(-particledata.rotation_direction_ranges.x, particledata.rotation_direction_ranges.x);
                volt.orbitalY = UnityEngine.Random.Range(-particledata.rotation_direction_ranges.y, particledata.rotation_direction_ranges.y);

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
    }
}
[System.Serializable]
public class PlanetGraphicsData
{
    [Header("Main Info")]
    [Range(0.1f, 20)]
    public float size;
    [Range(0, 5f)]
    public float rotation_speed_range;
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
    [ConditionalField("PStype", ParticleShapeType.Ring)] public Vector2 ring_rotation_range = new Vector2(0, 0);

    [Header("rotation_direction_ranges should be in range of 0f-1f")]
    [ConditionalField("PStype", ParticleShapeType.Sphere)] public Vector3 rotation_direction_ranges;
    public Vector2 rotation_speed_range;
    [Header("Over Lifetime")]
    public AnimationCurve size_OLT_curve;
    public Gradient color_OLT;
}
[CustomEditor(typeof(PGS))]
public class Editor0 : Editor
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
