using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadMeshGenerator : AMeshMaskGenerator
{
    Vector2 dimensions;

    public QuadMeshGenerator(Mesh mesh, int obstructionLayer, float width, float height) : base(mesh, obstructionLayer)
    {
        this.dimensions = new Vector2(width, height);
    }

    protected override Mesh GenerateValidMesh(Vector2 origin, Vector2 offset, Vector2 direction, int resolution)
    {
        this.mesh = new Mesh();

        Vector3[] verticies = new Vector3[4];
        Vector2[] uv = new Vector2[4];
        int[] triangles = new int[6];

        float maxX = offset.x + this.dimensions.x / 2;
        float minX = offset.x - this.dimensions.x / 2;
        float maxY = offset.y + this.dimensions.y / 2;
        float minY = offset.y - this.dimensions.y / 2;

        Vector2 ulCorner = new Vector2(minX, maxY);
        Vector2 urCorner = new Vector2(maxX, maxY);
        Vector2 llCorner = new Vector2(minX, minY);
        Vector2 lrCorner = new Vector2(maxX, minY);

        verticies[0] = ulCorner;
        verticies[1] = urCorner;
        verticies[2] = llCorner;
        verticies[3] = lrCorner;

        for (int i = 0; i < 4; i++)
        {
            uv[i] = verticies[i];
        }

        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 3;
        triangles[3] = 0;
        triangles[4] = 2;
        triangles[5] = 3;

        this.mesh.vertices = verticies;
        this.mesh.uv = uv;
        this.mesh.triangles = triangles;

        return this.mesh;
    }
}
