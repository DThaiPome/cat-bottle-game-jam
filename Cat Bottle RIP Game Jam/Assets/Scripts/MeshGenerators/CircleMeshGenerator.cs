using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleMeshGenerator : AMeshMaskGenerator
{
    private float radius;

    public CircleMeshGenerator(Mesh mesh, int obstructionLayer, float radius) : base(mesh, obstructionLayer)
    {
        this.radius = radius;
    }

    protected override Mesh GenerateValidMesh(Vector2 origin, Vector2 offset, Vector2 direction, int resolution)
    {
        this.mesh = new Mesh();

        // Resolution is the vertex count here
        Vector3[] verticies = new Vector3[resolution + 1];
        Vector2[] uv = new Vector2[verticies.Length];

        // One triangle for each edge vertex
        int triangleCount = resolution;
        int[] triangles = new int[triangleCount * 3];

        // Put first vertex at center
        verticies[0] = offset;
        uv[0] = verticies[0];

        float angleStep = 360f / resolution;
        for(int i = 1; i < resolution + 1; i++)
        {
            float angle = angleStep * (i - 1);
            Vector2 iDirection = RotateVector(direction, angle);
            Vector2 vertex = GetPointAtObstruction(origin, iDirection, this.radius, obstructionLayer) + offset;
            uv[i] = vertex;
            verticies[i] = vertex;
        }

        for(int i = 0; i < triangleCount - 1; i++)
        {
            int rootIndex = i * 3;

            triangles[rootIndex] = 0;
            triangles[rootIndex + 1] = i + 1;
            triangles[rootIndex + 2] = i + 2;
        }
        triangles[(triangleCount - 1) * 3] = 0;
        triangles[((triangleCount - 1) * 3) + 1] = resolution;
        triangles[((triangleCount - 1) * 3) + 2] = 1;

        this.mesh.vertices = verticies;
        this.mesh.uv = uv;
        this.mesh.triangles = triangles;

        return this.mesh;
    }
}
