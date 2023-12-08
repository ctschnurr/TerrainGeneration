using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MeshGenerator : MonoBehaviour
{
    // Start is called before the first frame update

    Mesh mesh;
    Vector3[] vertices;
    int[] triangles;

    public int xRange = 10;
    public int zRange = 10;

    public float xPerl = 0.4f;
    public float zPerl = 0.6f;

    public float yClampMin;
    public float yClampMax;

    public float yMultiplierAmount;
    public float yMultiplierThreshold;

    public InputField xSize;
    public InputField zSize;

    public Slider xPerlSlider;
    public Slider zPerlSlider;

    public Slider yClampMinSlider;
    public Slider yClampMaxSlider;

    public Slider yMult;
    public Slider yMultThreshold;

    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        CreateGrid();
        CreateTriangles();
        ApplyNoise();

        CreateMesh();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
    }
    public void UpdateMesh()
    {
        xPerl = xPerlSlider.value;
        zPerl = zPerlSlider.value;

        yClampMin = yClampMinSlider.value;
        yClampMax = yClampMaxSlider.value;

        yMultiplierAmount = yMult.value;

        mesh.Clear();

        ApplyNoise();

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
    }

    public void CreateGrid()
    {
        vertices = new Vector3[(xRange + 1) * (zRange + 1)];

        int count = 0;
        for (int z = 0; z <= zRange; z++)
        {
            for (int x = 0; x <= xRange; x++)
            {
                vertices[count] = new Vector3(x, 0, z);
                count++;
            }
        }
    }

    public void CreateTriangles()
    {
        triangles = new int[xRange * zRange * 6];
        int vertexIndex = 0;
        int triangleIndex = 0;

        for (int z = 0; z < zRange; z++)
        {
            for (int x = 0; x < xRange; x++)
            {
                triangles[triangleIndex + 0] = vertexIndex + 0;
                triangles[triangleIndex + 1] = vertexIndex + xRange + 1;
                triangles[triangleIndex + 2] = vertexIndex + 1;
                triangles[triangleIndex + 3] = vertexIndex + 1;
                triangles[triangleIndex + 4] = vertexIndex + xRange + 1;
                triangles[triangleIndex + 5] = vertexIndex + xRange + 2;

                vertexIndex++;
                triangleIndex += 6;
            }
            vertexIndex++;
        }
    }

    public void ApplyNoise()
    {
        int count = 0;

        for (int z = 0; z <= zRange; z++)
        {
            for (int x = 0; x <= xRange; x++)
            {
                float y = Mathf.PerlinNoise(x * xPerl, z * zPerl);

                y *= yMultiplierAmount;

                y = Mathf.Clamp(y, yClampMin, yClampMax);
                vertices[count].y = y;
                count++;
            }
        }
    }

    public void CreateMesh()
    {
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
    }

}
