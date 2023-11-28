using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshGenerator : MonoBehaviour
{
    // Start is called before the first frame update

    Mesh mesh;
    Vector3[] vertices;
    int[] triangles;

    public int xRange = 10;
    public int zRange = 10;

    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        CreateGrid();
        CreateTriangles();

        CreateMesh();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateGrid()
    {
        vertices = new Vector3[(xRange + 1) * (zRange + 1)];

        int count = 0;
        for (int z = 0; z <= zRange; z++)
        {
            for (int x = 0; x < xRange; x++)
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

        for (int x = 0; x < xRange; x++)
        {
            triangles[triangleIndex + 0] = vertexIndex + 0;
            triangles[triangleIndex + 1] = vertexIndex + xRange + 1;
            triangles[triangleIndex + 2] = vertexIndex + 1;
            triangles[triangleIndex + 3] = vertexIndex + 1;
            triangles[triangleIndex + 4] = vertexIndex + xRange + 1;
            triangles[triangleIndex + 5] = vertexIndex + xRange + 2;

            vertexIndex++;
            triangleIndex+=6;
        }
    }

    public void CreateVertex(int x, int z)
    {
        
    }

    public void MakeShape()
    {

    }

    public void CreateMesh()
    {
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
    }
}
