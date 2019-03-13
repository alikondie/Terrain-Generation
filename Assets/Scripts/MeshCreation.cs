using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MeshCreation : MonoBehaviour
{

    Mesh mesh;
    public GenHeightMap generatedMap;
    public Texture2D heightMap;
    public Material mat;

    int width,height;
    float[,] map;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.AddComponent<MeshFilter>();
        width = generatedMap.mapWidth;
        height = generatedMap.mapHeight;
        map = generatedMap.map;
        mesh = GetComponent<MeshFilter>().mesh;
        importMapFromTexture();

        generateMap();
    }

    // Update is called once per frame
    void importMapFromTexture()
    {
        width = heightMap.width;
        height = heightMap.height;
        Debug.Log(width);
        map = new float[width, height];
        for (int i = 0; i < heightMap.width; i++)
        {
            for(int j = 0; j < heightMap.height; j++)
            {
                float pixel = heightMap.GetPixel(i, j).grayscale;
                map[i, j] = pixel;
            }
        }
    }

    // remplir les triangles
    public void generateMap()
    {
        Vector3[] verts = new Vector3[width * height];
        int[] triangles = new int[(height - 1) * (width - 1) * 6];
        for(int j = 0; j < height; j++)
            for(int i = 0; i < width;i++)
            {
                verts[j * width + i] = new Vector3(i,map[i,j]*10,j); 
            }
        int f = 0;
        for(int i = 0;i < (width-1) * height; i++)
        {
            if ((i + 1) % height == 0 || (i + 1) % width == 0)
                continue;

            
            triangles[f] = i;
            f++;
            triangles[f] = width + i;
            f++;
            triangles[f] = width + i + 1;
            f++;
            triangles[f] = i;
            f++;
            triangles[f] = width + i + 1;
            f++;
            triangles[f] =  i + 1;
            f++;
        }

        mesh.vertices = verts;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }
}
