using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenHeightMap : MonoBehaviour
{

    public int mapWidth = 1280;
    public int mapHeight = 720;
    public float mapScale = 0.2f;
    public float[,] map;

    public Renderer texture;
    // Start is called before the first frame update
    void Start()
    {
        float[,] shit = generateNoise();
        map = generateNoise();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float[,] generateNoise()
    {
        float[,] map = new float[mapWidth, mapHeight];
        if (mapScale <= 0)
            mapScale = Mathf.Epsilon;

        for(int j = 0; j < mapHeight; j++)
        {
            for(int i = 0;i < mapWidth; i++)
            {
                float x = i / mapScale;
                float y = j / mapScale;

                //float perlin = Mathf.Lerp(0.0f,1.0f,Mathf.PerlinNoise(x, y));
                float perlin = Mathf.PerlinNoise(x, y);
                //Debug.Log(perlin);
                map[i, j] = perlin;
            }
        }

        return map;
    }

    public void generateMap()
    {
        float[,] map = generateNoise();

        Texture2D tex = new Texture2D(mapWidth, mapHeight);

        Color[] grayScaleMap = new Color[mapWidth * mapHeight];
        for (int j = 0; j < mapHeight; j++)
        {
            for (int i = 0; i < mapWidth; i++)
            {
                grayScaleMap[j * mapWidth + i] = Color.Lerp(Color.black, Color.white, map[i, j]);
            }
        }

        tex.SetPixels(grayScaleMap);
        tex.Apply();
        texture.sharedMaterial.mainTexture = tex;
        texture.transform.localScale = new Vector3(mapWidth, 1, mapHeight);

    }
}
