using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class SimplePlanetGenerator : MonoBehaviour
{
    public static int map_size = 32;
    public float scale;
    public float[,] f_map = new float[map_size, map_size];
    public Texture2D texture_map;
    public Gradient ColorMap;
    public SpriteRenderer sr;
    public Texture2D makeMapTexture(float[,] float_map, Gradient ColorMaper)
    {

        Texture2D Surfacetexture = new Texture2D(map_size, map_size);


        Color[] colourMap = new Color[map_size * map_size];
        for (int y = 0; y < map_size; y++)
        {
            for (int x = 0; x < map_size; x++)
            {

                Color n = ColorMap.Evaluate(float_map[x, y]);

                colourMap[y * map_size + x] = n;

            }
        }
        Surfacetexture.filterMode = FilterMode.Point;
        Surfacetexture.SetPixels(colourMap);
        Surfacetexture.Apply();

        // Surfacetexture = Resized(Surfacetexture, width * MapQuality, height * MapQuality);
        Surfacetexture.wrapMode = TextureWrapMode.Mirror;

        return Surfacetexture;
    }
    public float[,] makeFMap()
    {
        Vector2 offset = new Vector2(UnityEngine.Random.Range(-1,1), UnityEngine.Random.Range(-1, 1));
        float[,] map = new float[map_size, map_size];
        for (int y = 0; y < map_size; y++)
        {
            for (int x = 0; x < map_size; x++)
            {
                 float xCoord = 0 + (float)x / map_size * scale;
                 float yCoord = 0 + (float)y / map_size * scale;
                 float sample = Mathf.PerlinNoise(xCoord, yCoord);
                 //float f =Mathf.Clamp( Mathf.PerlinNoise(x, y),0.0f,1.0f);
                 map[x, y] = (int)(sample * 10)/10f;
                 Debug.Log(map[x, y]+"="+sample);
                //map[x, y] = x >= y ? 0 : 1;

            }
        }
        return map;
    }
    public void Start()
    {
        f_map = makeFMap();
        texture_map = makeMapTexture(f_map, ColorMap);
        sr.material.SetTexture("_map", texture_map);
       // sr.material.set("_map", texture_map);

    }
}
